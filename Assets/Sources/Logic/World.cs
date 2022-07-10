using System;
using System.Collections.Generic;
using Sources.LogicComponents;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources
{
    public class World: MonoBehaviour
    {
        [SerializeField] private Actor playerActor;
        [SerializeField] private Pool<Bonus> healPotions;
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private Pool<Actor> enemyGoblin;
        [SerializeField, Range(2,15)] private float spawnDelay = 4;
        [SerializeField] private AudioClip sfxSpawn;
        private readonly List<Actor> _actors = new List<Actor>();
        private float _spawnDelay;
        private Vector3 _playerSpawnPoint;
        private HealthComponent _playerHealth;

        public event Action EventGameStarted;
        public event Action EventGameFinished;
        public event Action<Actor> EventSpawnActor;

        public bool IsPlaying { get; private set; }
        public Actor MainPlayer => playerActor;
        public IEnumerable<Actor> Actors => _actors;

        public void StartNewGame()
        {
            foreach (var actor in _actors)
            {
                actor.gameObject.SetActive(false);
            }
            _actors.Clear();
            
            playerActor.gameObject.SetActive(false);
            playerActor.transform.position = _playerSpawnPoint;
            playerActor.gameObject.SetActive(true);

            healPotions.Clear();
            IsPlaying = true;
            EventGameStarted?.Invoke();

            foreach (var point in spawnPoints)
            {
                _actors.Add(playerActor);
                Spawn(point.position);
            }

            _spawnDelay = spawnDelay;
        }

        public void DropHealPotion(Vector3 position)
        {
            var potion = healPotions.Get();
            potion.transform.position = position;
        }

        private void Awake()
        {
            _playerSpawnPoint = playerActor.transform.position;
            playerActor.World = this;
            playerActor.gameObject.SetActive(false);
            _playerHealth = playerActor.GetComponent<HealthComponent>();
        }

        private void Update()
        {
            if (IsPlaying == false) return;
            ClearDeathEnemy();
            SpawnAtRandomPoint();
        }

        private void SpawnAtRandomPoint()
        {
            if (_spawnDelay > 0)
            {
                _spawnDelay -= Time.deltaTime;
                return;
            }
            var point = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Spawn(point.position);
            _spawnDelay = spawnDelay;
        }

        private void Spawn(Vector3 point)
        {
            var enemy = enemyGoblin.Get();
            enemy.transform.position = point;
            enemy.World = this;
            _actors.Add(enemy);
            sfxSpawn.Play();
            EventSpawnActor?.Invoke(enemy);
        }

        private void ClearDeathEnemy()
        {
            for (int i = _actors.Count - 1; i >= 0; i--)
            {
                var actor = _actors[i];
                if (actor.IsDie && actor.TimeDeath + 5 < Time.time)
                {
                    _actors.RemoveAt(i);
                    actor.gameObject.SetActive(false);
                }

                if (actor == playerActor && actor.IsDie)
                {
                    IsPlaying = false;
                    EventGameFinished?.Invoke();
                }
            }
        }
    }
}
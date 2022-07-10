using System;
using UnityEngine;

namespace Sources.LogicComponents
{
    public class WeaponComponent: MonoBehaviour
    {
        [SerializeField] private int damageValue;
        [SerializeField] private AudioClip sfxDamage;

        public int BonusDamage { get; set; }
        public int DamageValue => damageValue + BonusDamage;

        private void OnEnable()
        {
            sfxDamage.Play();
        }
    }
}
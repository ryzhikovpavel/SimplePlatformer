using UnityEngine;

namespace Sources
{
    [RequireComponent(typeof(AudioSource))]
    public class Sound : MonoBehaviour
    {
        public AudioClip ButtonClip;
        
        private AudioSource _source;
        private void Awake()
        {
            _source = GetComponent<AudioSource>();
        }

        public void Play(AudioClip clip)
        {
            _source.PlayOneShot(clip);
        }
    }

    public static class SoundExtension
    {
        public static void Play(this AudioClip clip)
            => Game.Sound.Play(clip);
    }
}
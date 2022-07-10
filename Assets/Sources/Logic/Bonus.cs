using System.Collections;
using UnityEngine;

namespace Sources
{
    public class Bonus: MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private ParticleSystem vfxCollect;
        [SerializeField] private Collider2D trigger;
        [SerializeField] private SpriteRenderer image;
        [SerializeField] private Types type;
        [SerializeField] private int value;
        public enum Types
        {
            HealPotion
        }

        public Types Type => type;
        public int Value => value;

        private void OnEnable()
        {
            trigger.enabled = true;
            animator.enabled = true;
            image.enabled = true;
            vfxCollect.gameObject.SetActive(false);
        }

        public void Take()
        {
            trigger.enabled = false;
            animator.enabled = false;
            image.enabled = false;
            
            StartCoroutine(PlayEffect());
        }

        private IEnumerator PlayEffect()
        {
            vfxCollect.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.1f);
            gameObject.SetActive(false);
        }
    }
}
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sources
{
    public class ButtonSound : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            Game.Sound.Play(Game.Sound.ButtonClip);
        }
    }
}
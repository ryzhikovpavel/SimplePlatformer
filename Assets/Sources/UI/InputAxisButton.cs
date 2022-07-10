using UnityEngine;
using UnityEngine.EventSystems;

namespace Sources.UI
{
    public class InputAxisButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private int direction;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("Active: " + direction);
            if (direction == 1) 
                Game.MobileInput.RightActive = true;
            else
                Game.MobileInput.LeftActive = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("Deactive: " + direction);
            if (direction == 1) 
                Game.MobileInput.RightActive = false;
            else
                Game.MobileInput.LeftActive = false;
        }
    }
}
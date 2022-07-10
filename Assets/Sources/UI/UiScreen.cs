using UnityEngine;

namespace Sources.UI
{
    public class UiScreen : MonoBehaviour
    {
        protected void Activate()
        {
            if (Game.UI.ActiveScreen != null)
            {
                Game.UI.ActiveScreen.gameObject.SetActive(false);
            }
            Game.UI.ActiveScreen = this;
            gameObject.SetActive(true);
        }
    }
}
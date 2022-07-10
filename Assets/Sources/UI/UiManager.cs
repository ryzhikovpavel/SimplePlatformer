using UnityEngine;

namespace Sources.UI
{
    public class UiManager: MonoBehaviour
    {
        public UiScreen ActiveScreen { get; set; }

        public T Find<T>()
        {
            return gameObject.GetComponentInChildren<T>(true);
        }
    }
}
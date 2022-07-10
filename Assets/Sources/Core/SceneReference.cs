using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sources
{
    public static class SceneReference<T> where T : Component
    {
        private static T _instance;
        
        public static T Get()
        {
            if (_instance is null)
            {
                _instance = Object.FindObjectOfType<T>();
                if (_instance is null)
                    Debug.LogError($"{typeof(T).Name} not found in scene");
                
                SceneManager.sceneUnloaded -= Clear;
                SceneManager.sceneUnloaded += Clear;
            }
            
            return _instance;
        }

        private static void Clear(Scene scene)
        {
            _instance = null;
        }
    }
}
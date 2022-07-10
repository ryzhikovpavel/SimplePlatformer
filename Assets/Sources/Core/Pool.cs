using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sources
{
    [Serializable]
    public class Pool<T> where T: Component
    {
        [SerializeField] private T prefab;
        [SerializeField] private Transform root;
        private List<T> _items = new List<T>();

        public T Get()
        {
            foreach (var item in _items)
            {
                if (item is not null && item.gameObject.activeSelf == false)
                {
                    item.gameObject.SetActive(true);
                    return item;
                } 
            }

            var newItem = Object.Instantiate(prefab, root);
            _items.Add(newItem);
            return newItem;
        }

        public void Clear()
        {
            foreach (var item in _items)
            {
                if (item is not null) item.gameObject.SetActive(false);
            }
        }
    }
}
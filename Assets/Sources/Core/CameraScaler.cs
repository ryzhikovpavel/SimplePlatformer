using System;
using UnityEngine;

namespace Sources
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    public class CameraScaler : MonoBehaviour
    {
        [SerializeField] private float targetAspect;
        [SerializeField] private float targetSize;
        private Camera _camera;
        private int _width;
        private int _height;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
        }
        
        private void Update()
        {
            Fit();
        }

        private void Fit()
        {
            var width = Screen.width;
            var height = Screen.height;
            if (width == _width && height == _height) return;
            if (height <= 0 || height > 10000 || width <= 0 || width > 10000) return;
            _width = width;
            _height = height;
            var aspect = _width / (float)_height;
            aspect = targetSize * targetAspect / aspect;
            if (aspect <= 0) return;
            _camera.orthographicSize = aspect;
        }
    }
}
using System;
using UnityEngine;

namespace Sources.LogicComponents
{
    [RequireComponent(typeof(Collider2D))]
    public class GroundComponent: MonoBehaviour
    {
        [SerializeField] private LayerMask groundLayerMask;
        private Collider2D _collider;
        private float _distance;
        
        public bool IsGrounded { get; private set; }

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }

        private void Start()
        {
            _distance = _collider.bounds.size.y / 2f + 0.02f;
        }

        private void FixedUpdate()
        {
            IsGrounded = Physics2D.Raycast(_collider.bounds.center, Vector2.down, _distance,groundLayerMask).collider is not null;
        }
    }
}
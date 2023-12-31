using System;
using UnityEngine;

namespace UIDrama
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private float maxValue = 14f;
        [SerializeField] private float minValue = 0.5f;

        private SpriteRenderer _look;
        private BoxCollider2D _collider;
        private ILoadable _loadable;
        
        private float _fullPosition = 0f;
        private float _emptyPosition;

        private Rigidbody2D _body;
        private Vector3 _defaultPosition;
        private float _mass;

        private bool _isReady;
       

        private void Awake()
        {
            _look = GetComponent<SpriteRenderer>();
            _collider = GetComponent<BoxCollider2D>();
            _body = GetComponentInParent<Rigidbody2D>();

            _defaultPosition = transform.localPosition;
            _emptyPosition = _defaultPosition.x;

            _isReady = true;
        }


        public void SetProgress(int percent)
        {
            if (_isReady)
            {
                var t = Convert.ToSingle(percent) * 0.01f;
                var width = Mathf.Lerp(minValue, maxValue, t);
                var xOffset = Mathf.Lerp(_emptyPosition, _fullPosition, t);

                transform.localPosition = new Vector3(xOffset, _defaultPosition.y, _defaultPosition.z);

                _look.size = new Vector2(width, _look.size.y);
                _collider.size = new Vector2(_look.size.x, _collider.size.y);
            }
        }

    }
}

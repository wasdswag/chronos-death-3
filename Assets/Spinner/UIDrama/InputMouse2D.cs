using System;
using System.Collections.Generic;
using UnityEngine;

namespace UIDrama
{
    public interface IMouseInteractable
    {
        void OnEnter();
        void OnOver();
        void OnDown();
        void OnDrag();
        void OnUp();
        void OnExit();
        Vector3 Position { get; }
    }
    public class InputMouse2D : MonoBehaviour
    {
        public Vector3 CursorPosition { get; private set; }
        
        [SerializeField] private LayerMask interactables;
        [SerializeField] private Camera gameCamera;
        private IMouseInteractable _current, _previous;

        private enum _conditions
        {   
            Enter,
            Down,
            Up,
            Drag
        }

        private Dictionary<_conditions, bool> _states = new Dictionary<_conditions, bool>();


        private void Awake()
        {
            _states.Add(_conditions.Enter, _previous!=_current);
            _states.Add(_conditions.Down, Input.GetMouseButtonDown(0));
            _states.Add(_conditions.Drag, Input.GetMouseButton(0));
            _states.Add(_conditions.Up, Input.GetMouseButtonUp(0));
        }

        private void Update()
        {
            var mousePosition = Input.mousePosition;
            var hit = Physics2D.Raycast(GetCursorPosition(transform.position, mousePosition), Vector2.up, interactables);
            
            if (hit && hit.collider.TryGetComponent(out IMouseInteractable interactable))
            {
                CursorPosition = GetCursorPosition(interactable.Position, mousePosition);
                
                _previous = _current;
                _current = interactable;
                
                
                
                if (_current != _previous)
                {
                    _current.OnEnter();
                    OnHitExit(ref _previous);
                }
                
                _current.OnOver();

                if (Input.GetMouseButtonDown(0))
                    _current.OnDown();

                if (Input.GetMouseButton(0))
                    _current.OnDrag();

                if (Input.GetMouseButtonUp(0))
                    _current.OnUp();
            }
            
            else
            {
                OnHitExit(ref _current);
                OnHitExit(ref _previous);
            }
        }
        
        void OnHitExit(ref IMouseInteractable interactable)
        {
            if (interactable != null)
            {
                interactable.OnExit();
                interactable = null;
            }
        }
        
        
        public Vector3 GetCursorPosition(Vector3 current, Vector3 mousePos)
        {        
            var distanceToScreen = gameCamera.WorldToScreenPoint(current).z;
            mousePos.z = distanceToScreen;
            var cursorPosition = gameCamera.ScreenToWorldPoint(mousePos);
            return cursorPosition;
        }
        



    }
}
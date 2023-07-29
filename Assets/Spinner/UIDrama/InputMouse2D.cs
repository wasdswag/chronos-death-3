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
        Vector3 CursorPosition { get; set; }
        
    }
    public class InputMouse2D : MonoBehaviour
    {
        public Vector3 CursorPosition { get; private set; }
        
        [SerializeField] private LayerMask interactables;
        [SerializeField] private Camera gameCamera;
        private IMouseInteractable _current, _previous, _dragged;


        private void Update()
        {
            var mousePosition = Input.mousePosition;
            var hit = Physics2D.Raycast(GetCursorPosition(transform.position, mousePosition), Vector2.up, interactables);
            
            if (hit && hit.collider.TryGetComponent(out IMouseInteractable interactable) && _dragged == null)
            {
                 interactable.CursorPosition = GetCursorPosition(interactable.Position, mousePosition);
                 _previous = _current;
                 _current = interactable;
                
                if (_current != _previous)
                {
                    _current.OnEnter();
                    OnHitExit(ref _previous);
                }
                
                _current.OnOver();

                if (Input.GetMouseButtonDown(0)) _current.OnDown();
                if (Input.GetMouseButton(0)) _current.OnDrag();
                if (Input.GetMouseButtonUp(0))  _current.OnUp();
            }
            
            else if (Input.GetMouseButton(0))
            {
                SwapCashed(ref _current);
                OnHitExit(ref _previous);

                if (_dragged != null)
                {
                    _dragged.OnDrag();
                    _dragged.CursorPosition = GetCursorPosition(_dragged.Position, mousePosition);
                }
            }
            else
            {
                OnHitExit(ref _current);
                OnHitExit(ref _previous);
                _dragged = null;
            }
        }

        private void SwapCashed(ref IMouseInteractable interactable)
        {
            if (interactable != null)
            {
                _dragged = interactable;
                
                interactable = null;
            }
        }
        
        private void OnHitExit(ref IMouseInteractable interactable)
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
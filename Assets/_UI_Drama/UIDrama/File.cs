using System;
using UnityEngine;

namespace UIDrama
{
    public class File : Executable
    {
        private Folder _folder;
        public void SetRootFolder(Folder root) => _folder = root;
        private bool _isInside;
        private bool _isDragged;
        
        
        protected override void OnMouseDown()
        {
            _isInside = _folder.IsFileInside(this);
            _isDragged = true;
            
            if (_isInside)
            {
                SelfCollider.isTrigger = true;
            }

            base.OnMouseDown();
        }

        protected override void OnMouseUp()
        {
            _isDragged = false;
            SelfCollider.isTrigger = false;
            var state = _folder.IsFileInside(this);
            if (_isInside != state)
            { 
                _folder.PlayMoveSound();
                _isInside = state;
            }

            base.OnMouseUp();
            Body.isKinematic = !_isInside;
        }
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (_folder.IsFileInside(this) || !_isDragged) return;
            
            var collider = col.collider;
            SelfCollider.isTrigger = collider.TryGetComponent(out FolderBoundaries bound) && _folder.HasThis(bound);
        }
    }
}
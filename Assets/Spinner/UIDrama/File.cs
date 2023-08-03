using System;
using UnityEngine;

namespace UIDrama
{
    public class File : Executable
    {
        private Folder _folder;
        public void SetRootFolder(Folder root) => _folder = root;
        private bool _isInside;
        
        
        protected override void OnMouseDown()
        {
            _isInside = _folder.IsFileInside(this);
            
            if (_isInside)
            {
                SelfCollider.isTrigger = true;
            }

            base.OnMouseDown();
        }

        protected override void OnMouseUp()
        {
            var state = _folder.IsFileInside(this);
            if (_isInside != state)
            { 
                Debug.Log("Playing move sound");
                _isInside = state;
            }
            
            SelfCollider.isTrigger = false;
            base.OnMouseUp();
        }
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (_folder.IsFileInside(this)) return;
            
            var collider = col.collider;
            SelfCollider.isTrigger = collider.TryGetComponent(out FolderBoundaries bound) && _folder.HasThis(bound);
        }
    }
}
using System;
using UnityEngine;

namespace UIDrama
{
    public abstract class ProgressReactable : MonoBehaviour
    {
        [SerializeField] private GameObject loader;
        private ILoadable _loadable;
        
        
        private void OnEnable()
        {
            if (loader == null)
                throw new Exception("there is no reference to loader game object");
            
            _loadable = loader.GetComponent<ILoadable>();
            if(_loadable != null) _loadable.OnProgressChange += SetProgress;
        }

        private void OnDisable()
        {
            if(_loadable != null) _loadable.OnProgressChange -= SetProgress;
        }

        protected abstract void SetProgress(int percent);
     
    }
}
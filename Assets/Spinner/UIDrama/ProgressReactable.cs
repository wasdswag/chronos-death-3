using System;
using UnityEngine;
using NaughtyAttributes;

namespace UIDrama
{
    public abstract class ProgressReactable : MonoBehaviour
    {
        protected int Progress { get; private set; }
        [SerializeField] private bool trackProgress;
        [ShowIf(nameof(trackProgress)), SerializeField] private GameObject loader;
        private ILoadable _loadable;
        
        private void OnEnable()
        {
           if (!trackProgress) return;
            
            if (loader == null)
                throw new Exception("there is no reference to loader game object");
            
            _loadable = loader.GetComponent<ILoadable>();
            if(_loadable != null) _loadable.OnProgressChange += SetProgress;
        }

        private void OnDisable()
        {
            if (!trackProgress) return;
            if(_loadable != null) _loadable.OnProgressChange -= SetProgress;
        }

        protected virtual void SetProgress(int percent) => Progress = percent;
     
    }
}
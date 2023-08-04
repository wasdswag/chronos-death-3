using UnityEngine;

namespace UIDrama
{
    public abstract class Program : ProgressReactable, IProgram
    {
        
        [field: SerializeField] public GameObject[] UIDramaElements { get; set; }
        public bool IsRunning { get; set; }

        public virtual void Run()
        {
            IsRunning = true;
            foreach (var uiObj in UIDramaElements)
                uiObj.SetActive(true);
        }

        public virtual void Stop()
        {
            IsRunning = false;
            foreach (var uiObj in UIDramaElements)
                uiObj.SetActive(false);
        }
    }
}
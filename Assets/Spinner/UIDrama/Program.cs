using UnityEngine;

namespace UIDrama
{
    public abstract class Program : ProgressReactable, IProgram
    {
        
        [field: SerializeField] public GameObject[] UIDramaElements { get; set; }
        public bool IsRunning { get; set; }
      
        
        public virtual void Run()
        {
            foreach (var uiObj in UIDramaElements)
                uiObj.SetActive(true);
        }

        public virtual void Stop()
        {
            foreach (var uiObj in UIDramaElements)
                uiObj.SetActive(false);
        }
    }
}
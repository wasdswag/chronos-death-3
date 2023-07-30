using UnityEngine;

namespace UIDrama
{
    public class UnzipProgram : ProgressReactable, IProgram
    {
        [field:SerializeField] public GameObject[] UIDramaElements { get; set; }
        public bool IsRunning { get; set; }
        [SerializeField] private GameObject expandedFolder;
        [SerializeField] private Spinner spinner;
        
        
        public void Run()
        {
            foreach (var uiObj in UIDramaElements)
                uiObj.SetActive(true);
            
            spinner.StartAuto();
            IsRunning = true;
        }

        public void Stop()
        {
            expandedFolder.SetActive(true);
            foreach (var uiObj in UIDramaElements)
                uiObj.SetActive(false);
        }


        protected override void SetProgress(int percent)
        {
            if(percent >= 100) Stop();
        }
    }
}
using UnityEngine;

namespace UIDrama
{
    public class UnzipProgram : ProgressReactable, IProgram
    {
        public bool IsRunning { get; set; }
        [SerializeField] private GameObject dialogue;
        [SerializeField] private GameObject expandedFolder;
        [SerializeField] private Spinner spinner;
        [SerializeField] private GameObject spinnerShadow;

        public void Run()
        {
            dialogue.SetActive(true);
            spinner.gameObject.SetActive(true);
            spinnerShadow.SetActive(true);
            spinner.StartAuto();
            IsRunning = true;
        }
        
        public void Stop()
        {
            expandedFolder.SetActive(true);
            dialogue.SetActive(false);
        }

        protected override void SetProgress(int percent)
        {
            if(percent >= 100) Stop();
        }
    }
}
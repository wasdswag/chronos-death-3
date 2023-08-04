using UnityEngine;
using UnityEngine.UI;

namespace UIDrama
{
    public class UnzipProgram : Program
    {
        [SerializeField] private Button cancel;
        [SerializeField] private ProgressBar progressBar;
        
        [SerializeField] private GameObject expandedFolder;
        [SerializeField] private Spinner spinner;
        
        

        public override void Run()
        {
            base.Run();
            spinner.StartAuto();
            IsRunning = true;
        }

        public override void Stop()
        {
            if (IsRunning)
            {
                expandedFolder.SetActive(true);
                base.Stop();
                IsRunning = false;
            }
        }
        
        protected override void SetProgress(int percent)
        {
            progressBar.SetProgress(percent);
            if(percent >= 100) Stop();
        }
    }
}
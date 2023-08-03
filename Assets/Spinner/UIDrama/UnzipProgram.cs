using UnityEngine;

namespace UIDrama
{
    public class UnzipProgram : Program
    {
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
            if(percent >= 100) Stop();
        }
    }
}
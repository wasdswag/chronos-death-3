using UnityEngine;
using UnityEngine.UI;

namespace UIDrama
{
    public class Installer : Program
    {
        [SerializeField] private Button playButton;
        [SerializeField] private ProgressBar progressBar;
        [SerializeField] private ProgressText progressText;
        
        [SerializeField] private Folder folder;
        [SerializeField] private Error error;

        [SerializeField] private InstallerHeader header;
       
        
        
        private bool playButtonIsPressed;
        private void Awake() => playButton.onClick.AddListener(IsDone);

        
        protected override void SetProgress(int percent)
        {
            progressBar.SetProgress(percent);
            progressText.SetProgress(percent);
            playButton.interactable = percent >= 100;
        }

        public override void Stop()
        {
            base.Stop();
            header.OnInstallationSuccess();
        }


        private void IsDone()
        {
            if(!playButton.interactable && Progress < 100) return;
            var missing = folder.GetMissingFiles();
            
            if (missing != null)
            {
                var message =
                    $"No such file or directory: ~/ChronosDeath3/{missing} make sure file \"{missing}\" is placed in root directory";
                error.SetMessage(message);
                if(!error.IsRunning) error.Run();
            }
            else
            {
                if(error.IsRunning) error.Stop();
                Stop();
                IsRunning = false;
                playButtonIsPressed = true;
            }
        }

    }
}
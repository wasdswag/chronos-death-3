using System;
using UnityEngine;

namespace UIDrama
{
    public class ChronOSInstaller : Installer
    {
       
        [SerializeField] private Folder folder;
        [SerializeField] private Error error;
        [SerializeField] private InstallerHeader header;


        private void Start()
        {
            SetProgress(0);
            playButton.interactable = false;

        }


        public override void Stop()
        {
            base.Stop();
            header.OnInstallationSuccess();
        }

        protected override void IsDone()
        {
            if(!playButton.interactable && Progress < 100) 
                return;
            
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
            }
        }

    }
}
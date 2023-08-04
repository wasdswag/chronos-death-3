using UnityEngine;
using UnityEngine.UI;

namespace UIDrama
{
    public class Installer : Program
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Folder folder;
        [SerializeField] private Error error;
        
        private bool playButtonIsPressed;
        private int _progress;
        private void Awake() => playButton.onClick.AddListener(IsDone);
        
        
        protected override void SetProgress(int percent)
        {
            _progress = percent;
        }
        
        private void IsDone()
        {
            if(!playButton.interactable && _progress < 100) return;
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
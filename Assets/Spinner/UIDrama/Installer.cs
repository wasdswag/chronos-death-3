using UnityEngine;
using UnityEngine.UI;

namespace UIDrama
{
    public class Installer : Program
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Folder folder;
        
        private bool playButtonIsPressed;
        private void Awake() => playButton.onClick.AddListener(IsDone);
        
        
        protected override void SetProgress(int percent)
        {
            if (percent >= 100)
            {
                if (playButtonIsPressed && folder.CheckEveryFileExist())
                    Stop();
            }
        }

        private void IsDone() => playButtonIsPressed = true;
        
    }
}
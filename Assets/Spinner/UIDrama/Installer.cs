using UnityEngine;
using UnityEngine.UI;

namespace UIDrama
{
    public class Installer : Program
    {
        [SerializeField] private Button playButton;
        private bool playButtonIsPressed;
        private void Awake() => playButton.onClick.AddListener(IsDone);
        
        protected override void SetProgress(int percent)
        {
            if (percent >= 100)
            {
                if (playButtonIsPressed)
                    Stop();
            }
        }

        private void IsDone() => playButtonIsPressed = true;
        
    }
}
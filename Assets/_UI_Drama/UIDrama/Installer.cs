using UnityEngine;
using UnityEngine.UI;

namespace UIDrama
{
    public abstract class Installer : Program
    {
        [SerializeField] protected Button playButton;
        [SerializeField] private ProgressBar progressBar;
        [SerializeField] private ProgressText progressText;
       
        private void Awake() => playButton.onClick.AddListener(IsDone);

        
        protected override void SetProgress(int percent)
        {
            progressBar.SetProgress(percent);
            progressText.SetProgress(percent);
            playButton.interactable = percent >= 100;
        }


        protected abstract void IsDone();
       

    }
        
    
}
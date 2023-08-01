using System;
using UnityEngine;
using UnityEngine.UI;

namespace UIDrama
{
    public class Installer : ProgressReactable, IProgram
    {
        [field: SerializeField] public GameObject[] UIDramaElements { get; set; }
        public bool IsRunning { get; set; }

        [SerializeField] private Button playButton;
        private bool playButtonIsPressed;
        private void Awake() => playButton.onClick.AddListener(IsDone);


        public void Run()
        {
            foreach (var uiObj in UIDramaElements)
                uiObj.SetActive(true);
        }

        public void Stop()
        {
            Debug.Log("Install Complete");
            foreach (var uiObj in UIDramaElements)
                uiObj.SetActive(false);
        }

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
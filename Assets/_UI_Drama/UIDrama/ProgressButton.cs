using UnityEngine;
using UnityEngine.UI;

namespace UIDrama
{
    public class ProgressButton : ProgressReactable
    {
        [SerializeField] private int interactableRequiredPercent = 100;
        [SerializeField] private Button button;
        protected override void SetProgress(int percent) => button.interactable = percent >= interactableRequiredPercent;
    }
}
using UnityEngine;
using UnityEngine.UI;

namespace UIDrama
{
    public class Readme : Program
    {
        [SerializeField] private Button closeButton;
        private void Awake() => closeButton.onClick.AddListener(Stop);
    }
}
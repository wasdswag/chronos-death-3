using UnityEngine;
using UnityEngine.UI;

namespace UIDrama
{
    public class PopupWindow : Program
    {
        [SerializeField] private Button closeButton;
        private void Awake() => closeButton.onClick.AddListener(Stop);
    }
}
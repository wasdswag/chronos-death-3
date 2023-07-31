using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

namespace UIDrama
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    [ExecuteAlways]
    public class ProgressText : ProgressReactable
    {
        [System.Serializable]
        private struct Message
        {
            public int StartPercent { get; set; }
            public int EndPercent;
            [Header("Progress message:")]
            [FormerlySerializedAs("Header")] public string progressHeader;
            [FormerlySerializedAs("Status")][TextArea] public string progressStatus;
            [Header("Regress message:")]
            public string regressHeader;
            [TextArea] public string regressStatus;
            
        }

        [SerializeField] Message[] _messages;
        private TextMeshProUGUI _status;
        private int _lastPercent;


        private void Start()
        {
            _status = GetComponent<TextMeshProUGUI>();
            for (int i = 0; i < _messages.Length - 1; i++)
                _messages[i + 1].StartPercent = _messages[i].EndPercent;
            
            SetProgress(0);

        }

        protected override void SetProgress(int percent)
        {
            foreach (var message in _messages)
            {
                if (percent >= message.StartPercent && percent < message.EndPercent)
                {
                    var status = percent > _lastPercent
                        ? $"{message.progressHeader}\n<size=85%>{message.progressStatus}"
                        : $"{message.regressHeader}\n<size=85%>{message.regressStatus}";
                    
                    _status.text = status;
                }
            }

            _lastPercent = percent;

        }
    }
}
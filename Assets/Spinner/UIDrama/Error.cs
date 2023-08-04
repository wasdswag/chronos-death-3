using UnityEngine;
using TMPro;

namespace UIDrama
{
    public class Error : Program
    {
        [SerializeField] private TextMeshProUGUI message;
        
        public void SetMessage(string e)
        {
            message.SetText(e);
        }
        
    }
}
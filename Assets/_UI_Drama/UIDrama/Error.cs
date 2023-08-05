using UnityEngine;
using TMPro;

namespace UIDrama
{
    public class Error : PopupWindow
    {
        [SerializeField] private TextMeshProUGUI message;
        
        public void SetMessage(string e)
        {
            message.SetText(e);
        }
        
    }
}
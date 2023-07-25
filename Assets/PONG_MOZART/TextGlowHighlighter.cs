using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextGlowHighlighter : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{

        private TextMeshProUGUI _text;
        private Material _textBaseMaterial;
        private Material _textHighlightMaterial;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _textBaseMaterial = _text.fontSharedMaterial;

            _textHighlightMaterial = new Material(_textBaseMaterial);
            _textHighlightMaterial.SetFloat(ShaderUtilities.ID_GlowPower, 1.0f);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _text.fontSharedMaterial = _textHighlightMaterial;
            _text.UpdateMeshPadding();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.pointerCurrentRaycast.gameObject == gameObject)
            {
                _text.fontSharedMaterial = _textBaseMaterial;
                _text.UpdateMeshPadding();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _text.fontSharedMaterial = _textHighlightMaterial;
            _text.UpdateMeshPadding();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _text.fontSharedMaterial = _textBaseMaterial;
            _text.UpdateMeshPadding();
        }
    }



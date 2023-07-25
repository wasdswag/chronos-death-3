using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Thought : MonoBehaviour
{
    private float _lifeTime, _currentTime;
    private TextMeshProUGUI _textLine;
    private AnimationCurve _transparency;

    private void Awake()
    {
        _textLine = GetComponent<TextMeshProUGUI>();
      
    }


    public void Set(string text, float time, AnimationCurve curve)
    {
        _lifeTime = time;
    
        _textLine.text = text;

        _transparency = curve;

        _textLine.color = Color.clear;
        _textLine.characterSpacing = -40f;
        _textLine.lineSpacing = -40f;

        StartCoroutine(Life());

    }


 

    private IEnumerator Life()
    {

        _currentTime = _lifeTime;
        //s.color = Color.white;
        //yield return new WaitForSeconds(1.5f);

        while (_currentTime > 0)
        {
            float t = Time.deltaTime * Narratory.LifeSpeed;
            _textLine.characterSpacing += t * 10f;
            _textLine.lineSpacing += t * 10f;
            _currentTime -= t;
            _textLine.color = Color.Lerp(Color.clear, Color.white, 1f - _transparency.Evaluate(_currentTime / _lifeTime));
            yield return null;
        }

        Debug.Log("Completed");
        Destroy(gameObject);
       
    }


}

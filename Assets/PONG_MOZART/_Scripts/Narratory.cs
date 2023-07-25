using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;


public class Narratory : MonoBehaviour, IEventsListener
{
    [SerializeField] private GameObject [] linePrefab;
    private int _lineprefabCounter;
    [SerializeField] private Transform canvas;
    [SerializeField] private AnimationCurve transparency;
    [SerializeField] private TextMeshProUGUI story;
    [SerializeField] [Range(0.5f, 1f)] private float offset = 0.5f;
    [SerializeField] private float lifeTime = 30f;
    [SerializeField] private int loopIntervals = 2;
    private float _currentTime;
    public static float LifeSpeed = 1f;

    private string[] _poemsSource;
    private List<string> _poems = new List<string>();

    private Vector2 _min, _max;

    private string[] _fonts = { "Bitter-Regular SDF",
                                "Caveat-Medium SDF",
                                "CormorantInfant-Medium SDF",
                                "EBGaramond-Bold SDF",
                                "EBGaramond-Regular SDF",
                                "PressStart2P-Regular SDF",
                                "RobotoMono-Regular SDF",
                                "RubikMonoOne-Regular SDF",
                                "SourceSerif4-Italic SDF",
                                "SourceSerif4-Regular SDF",
                                "StalinistOne-Regular SDF",
                                "Vollkorn-Italic SDF",
                                "Vollkorn-Regular SDF",
                                "YujiSyuku-Regular SDF" };

    private string[] _colorHex =
    {
        "><mark=#726100>",
        "><mark=#000000>",
        "><mark=#2F2752>",
        "><mark=#462929>",
        "><mark=#142729>"

    };


    private string _poem;
    [System.Serializable] private struct LineAlignment
    {
        [FormerlySerializedAs("AlignmentOption")] public TextAlignmentOptions alignmentOption;
        [FormerlySerializedAs("Margins")] public Vector4 margins;
    }

    [SerializeField] private LineAlignment[] lineAlignments;
    private List<LineAlignment> _lineAlignmentsBuffer = new List<LineAlignment>();



   private LineAlignment GetLineOption()
   {
        if(_lineAlignmentsBuffer.Count == 0)
           _lineAlignmentsBuffer = new List<LineAlignment>(lineAlignments);

        int rnd = Random.Range(0, _lineAlignmentsBuffer.Count);
        LineAlignment result = _lineAlignmentsBuffer[rnd];
        _lineAlignmentsBuffer.Remove(result);
        return result;
    }


   void Awake()
   {
    
       var source = Resources.Load("poem") as TextAsset;
       _poem = source.ToString();
   }

    private void Start()
    {
        SubscribeEvents();
        _poemsSource = _poem.Split('\n');
        _poems = new List<string>(_poemsSource);

        var size = story.GetComponent<RectTransform>().sizeDelta;
        _min = new Vector2(0 + size.x * offset, 0 + size.y * offset);
        _max = new Vector2(Screen.width - size.x * offset, Screen.height - size.y * offset);

       // story.transform.position = min;
    }


    private void DisplayLine()
    {
        if (Events.loopsCounter % loopIntervals == 0)
        {
            LifeSpeed = 1f;
            var newLine = Instantiate(linePrefab[_lineprefabCounter], canvas).GetComponent<Thought>();

            newLine.Set("<font=" + _fonts[Random.Range(0, _fonts.Length)] + _colorHex[Random.Range(0, _colorHex.Length)] + GetRandomLine() + "</mark>",  //"<font=" + _fonts[Random.Range(0, _fonts.Length)] + ">" + GetRandomLine(), 
                        lifeTime,  transparency);

            if (_lineprefabCounter < linePrefab.Length - 1) _lineprefabCounter++;
            else _lineprefabCounter = 0;

            //story = Instantiate(linePrefab, canvas).GetComponent<TextMeshProUGUI>();
            //var textPosition = GetLineOption();

            //story.margin = textPosition.Margins;
            //story.alignment = textPosition.AlignmentOption;


            //// story.transform.position = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
            //story.text = "<font=" + _fonts[Random.Range(0, _fonts.Length)]+">" + GetRandomLine() ; //"<font=" + _fonts[Random.Range(0, _fonts.Length)] + _colorHex[Random.Range(0, _colorHex.Length)] + GetRandomLine() + "</mark>";
            //_lifeSpeed = 1f;
            //story.color = Color.clear;
            //story.characterSpacing = -40f;
            //story.lineSpacing = -40f;
            //StartCoroutine(Life(story));
        }
    }




    private IEnumerator SlowMotion()
    {
        //ehjkhkddsadskjhsss    SSS=/5


        yield return null;



    }

    private IEnumerator Life(TextMeshProUGUI s)
    {
        _currentTime = lifeTime;
        //s.color = Color.white;
        //yield return new WaitForSeconds(1.5f);

        while (_currentTime > 0)
        {
            float t = Time.deltaTime * LifeSpeed;
            s.characterSpacing += t * 10f;
            s.lineSpacing += t * 10f;
            _currentTime -= t;
            s.color = Color.Lerp(Color.clear, Color.white, transparency.Evaluate(_currentTime / lifeTime));
            yield return null;
        }

        s.text = "";
        s.characterSpacing = 0;
        s.lineSpacing = 0;
    }


    private void ResetTimer() => LifeSpeed = 8f;

    private string GetRandomLine()
    {
        if(_poems.Count == 0)
           _poems = new List<string>(_poemsSource);

        int rnd = Random.Range(0, _poems.Count);
        string result = _poems[rnd];
        _poems.Remove(result);
        return result;
    }


    private void OnDestroy()
    {
        UnSubscribeEvents();
    }


    public void SubscribeEvents()
    {
        Events.OnCompleteLoop += DisplayLine;
        Events.OnFail += ResetTimer;
    }

    public void UnSubscribeEvents()
    {
        Events.OnCompleteLoop -= DisplayLine;
        Events.OnFail -= ResetTimer;
    }
}

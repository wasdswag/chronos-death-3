//using System.Threading;
//using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{

    public Transform positionHint;
    public bool isPassFromHuman;

    [SerializeField] private FieldSettings settings;
    [SerializeField] private float speed = 3f;
    [SerializeField] private Color[] highColors;
    [SerializeField] private Text passDisplay;

    private Rigidbody2D _body;
    private SpriteRenderer _show;
    private TrailRenderer _trail;
    private DiceMusic _diceMusic;

    private float _shortestDist = 12.6f;
    private float _clipDuration = 1f; //1.86f;

    private Vector2 _force;
    private int _reflectionsRemain = 50;

    private float _distance;
    private float _currentDistance;

    

   // private CancellationTokenSource _cancellationToken;




    void Start()
    {

       // Time.timeScale = 0.25f;
        Events.Reset();
        _clipDuration = settings.diceLevel == FieldSettings.Level.Mozart ? 1f : 1.86f;
        _body = GetComponent<Rigidbody2D>();
        _show = GetComponent<SpriteRenderer>();
        _trail = GetComponent<TrailRenderer>();
        _diceMusic = FindObjectOfType<DiceMusic>();

        StartGame();
        InvokeRepeating(nameof(GetFPS),0f,0.5f);
  


    }


    public void StartGame()
    {
        if (Events.beatCounter > 0)
        {
            Debug.LogWarning("On FAIL Reset");
            Events.OnFail?.Invoke();

        }
        _distance = _shortestDist;
        speed = _shortestDist / _clipDuration;
        Invoke(nameof(Pass), 2f);
    }

    private void Pass()
    {

        _body.velocity = Vector2.zero;
        _body.Sleep();
        transform.position = Vector3.zero;
        _force = new Vector2(1, UnityEngine.Random.Range(-0.3f, 0.3f));
        _body.WakeUp();
        speed = _shortestDist / _clipDuration;
        _body.velocity = _force * speed;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) StartGame();
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            _reflectionsRemain = 0;
            StopAllCoroutines();
            SceneManager.LoadScene(0);
        }
    
    }

    private void GetFPS()
    {
        var fps = (int)(1f / Time.unscaledDeltaTime);
        passDisplay.text = $"fps: {fps}";
    }
    


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Paddle paddle))
        {
            if (paddle.IsMyTurn || paddle.IsHuman)
            {
                isPassFromHuman = paddle.IsHuman;
                if (_distance > 18f && isPassFromHuman)
                    Events.OnApplyBonus?.Invoke();

                _body.velocity = Vector2.zero;
                _force = (transform.position - paddle.transform.position).normalized;

                //PassBall(paddle);
                StartCoroutine(PassBallCoroutine(paddle));
            }
        }
    }


    private IEnumerator PassBallCoroutine(Paddle paddle)
    {
        _currentDistance = 0;
        _distance = 0;
        _reflectionsRemain = 8;
        
        
        Vector3 position = transform.position;
        Vector3 direction = _force;
        
        while (_reflectionsRemain > 0)
        {
            if (position.x > settings.bounds.leftSide || position.x < settings.bounds.rightSide)
            {
                DrawReflectionPattern(ref _reflectionsRemain, ref position, ref direction);
                _reflectionsRemain--;
            }
            else
            {
                StartGame();
                break;
            }

            yield return null;
        }
        
        
        Color color = highColors[paddle.HeightIndex];
        _trail.startColor = color;
        Events.OnPassBall?.Invoke(paddle, color);
        speed = _distance / _clipDuration;
        _body.velocity = _force * speed;
        yield return null;

    }
    

    // private async void PassBall(Paddle paddle)
    // {
    //     _currentDistance = 0;
    //     _distance = 0;
    //     _reflectionsRemain = 50;
    //   
    //     await FindDistance();
    //
    //     Color color = highColors[paddle.HeightIndex];
    //     _trail.startColor = color;
    //     Events.OnPassBall?.Invoke(paddle, color);
    //     speed = _distance / _clipDuration;
    //     _body.velocity = _force * speed;
    //
    // }
    //
    //
    // private async Task FindDistance()
    // {
    //
    //     Vector3 position = transform.position;
    //     Vector3 direction = _force;
    //
    //
    //         while (_reflectionsRemain > 0)
    //         {
    //             if (position.x > settings.bounds.leftSide || position.x < settings.bounds.rightSide)
    //             {
    //                  DrawReflectionPattern(ref _reflectionsRemain, ref position, ref direction);
    //                 _reflectionsRemain--;
    //             }
    //             else
    //             {
    //                 StartGame();
    //                 break;
    //             }
    //
    //         await Task.Yield();
    //     }
    // } 



    private void DrawReflectionPattern(ref int reflections, ref Vector3 position, ref Vector3 direction)
    {

        if(_reflectionsRemain == 0)
        {
            Debug.Log("no rays");
            StartGame();
            return;
        }

        RaycastHit2D hit = Physics2D.Raycast(position, direction, 50f);
        _currentDistance += Vector2.Distance(position, hit.point);
        //Debug.DrawLine(position, hit.point, Color.green, 1f);

        if (hit.collider != null) {

            if(hit.collider.TryGetComponent(out CollisionTrigger collision))
            {
                _distance = _currentDistance;

                if (positionHint.gameObject.activeSelf && isPassFromHuman == false)
                    positionHint.position = new Vector3(positionHint.position.x, settings.bounds.ClampByHeight(hit.point.y), 0f);// Vector2.Lerp(position, hit.point, 0.92f);

                reflections = 0;
                return;
            }

            direction = Vector2.Reflect(direction, hit.normal);
            position = Vector2.Lerp(position, hit.point, 0.98f);
            return;

        }

    }

}

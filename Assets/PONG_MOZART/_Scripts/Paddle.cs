using UnityEngine;


[RequireComponent (typeof (PaddleDisplay))]
public class Paddle : MonoBehaviour, IEventsListener
{

    public PlayerSettings player;

    [HideInInspector] public bool IsHuman;
     public bool IsMyTurn;

    [SerializeField] private FieldSettings field;
    [SerializeField] private Transform ball;
    [SerializeField] private float misTargetOffset = 1.1f;

    private PaddleDisplay paddleDisplay;
   
   
    private int _heightIndex;
    public int HeightIndex { get => _heightIndex; private set => SetHeight(value); }

    private float _yPosition;
    private float _randomOffset;

    private Vector3 _paddlePosition;
    private Vector3 _startingPosition;

    private float _fieldHeight;
    private float _fieldHeightNegativeOffset;



    void Start()
    {
        _paddlePosition = transform.position;
        _fieldHeight = field.bounds.GetHeightLenght(); // Bounds.GetHeightLenght();
        _fieldHeightNegativeOffset = Mathf.Abs(field.bounds.downSide);
        IsHuman = player.Preferences.Mode == PlayerSettings.Mode.Human;
        paddleDisplay = GetComponent<PaddleDisplay>();

      //  SubscribeEvents();
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void Update()
    {
        Move();
        if(IsMyTurn) UpdateDiceByPosition();
    }

    private void Pass (Paddle paddle, Color color)
    {
        IsMyTurn = paddle != this;
        paddleDisplay.SetDiceColor(IsMyTurn, color);

        if (IsHuman == false) 
            _randomOffset = Random.Range(-misTargetOffset, misTargetOffset);
    }

    private void Move()
    {

        if (IsHuman)
        {
            _yPosition += player.Preferences.GetInput() * Time.deltaTime;
            _yPosition = field.bounds.ClampByHeight(_yPosition);
            transform.position = new Vector3(_paddlePosition.x, _yPosition, _paddlePosition.z);
        }

        else if (IsHuman == false && IsMyTurn) 
        {
            float ballYPosition = ball.position.y + _randomOffset;
            ballYPosition = field.bounds.ClampByHeight(ballYPosition);
            Vector3 target = new Vector3(transform.position.x, ballYPosition, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, target, player.Preferences.InputReactionSpeed * Time.deltaTime);
        }
    }


    private void SetHeight(int value)
    {
        if (value != _heightIndex && IsMyTurn)
        {
            _heightIndex = value;
            paddleDisplay.DisplayDice(HeightIndex + 2);
        }
    }

    private void UpdateDiceByPosition()
    {
        var part = Mathf.Lerp(0f, 10f, (transform.position.y + _fieldHeightNegativeOffset) / _fieldHeight);
        HeightIndex = (int)part;
    }

    private void ResetPaddles() => IsMyTurn = IsHuman == false;
 


    private void OnDestroy() => UnSubscribeEvents();

    public void SubscribeEvents()  { Events.OnPassBall += Pass; Events.OnFail += ResetPaddles; }
    public void UnSubscribeEvents() { Events.OnPassBall -= Pass; Events.OnFail -= ResetPaddles; }

}

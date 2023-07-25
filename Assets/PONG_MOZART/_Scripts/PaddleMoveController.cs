//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PaddleMoveController : Paddle
//{
//    public PlayerSettings player;

//    [SerializeField] private FieldSettings field;
//    [SerializeField] private Transform ball;
//    [SerializeField] private float misTargetOffset = 1.1f;


//    private float _yPosition;
//    private float _randomOffset;

//    private Vector3 _paddlePosition;
//    private Vector3 _startingPosition;

//    private float _fieldHeight;
//    private float _fieldHeightNegativeOffset;



//    private void Start()
//    {
//         IsHuman = player.Preferences.Mode == PlayerSettings.Mode.Human;
//        _paddlePosition = transform.position;
//        _fieldHeight = field.Bounds.GetHeightLenght();
//        _fieldHeightNegativeOffset = Mathf.Abs(field.Bounds.DownSide);

//    }


//    public override void OnGettingPass()
//    {
//        if (IsHuman == false)
//            _randomOffset = Random.Range(-misTargetOffset, misTargetOffset);
//    }


//    private void Update()
//    {
//        Move();
//        if(IsMyTurn) GetHeightPosition();
//    }

//    private void GetHeightPosition()
//    {
//        var part = Mathf.Lerp(0f, 10f, (transform.position.y + _fieldHeightNegativeOffset) / _fieldHeight);
//        HeightIndex = (int)part;
//    }

//    private void Move()
//    {
//        if (IsHuman)
//        {
//            _yPosition += player.Preferences.GetInput() * Time.deltaTime;
//            _yPosition = field.Bounds.ClampByHeight(_yPosition);
//            transform.position = new Vector3(_paddlePosition.x, _yPosition, _paddlePosition.z);
//        }

//        else if (IsHuman == false && IsMyTurn)
//        {
//            float ballYPosition = ball.position.y + _randomOffset;
//            ballYPosition = field.Bounds.ClampByHeight(ballYPosition);
//            Vector3 target = new Vector3(transform.position.x, ballYPosition, transform.position.z);
//            transform.position = Vector3.MoveTowards(transform.position, target, player.Preferences.InputReactionSpeed * Time.deltaTime);
//        }
//    }

//}

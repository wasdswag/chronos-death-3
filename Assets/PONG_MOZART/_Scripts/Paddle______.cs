//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;

//public abstract class Paddle : MonoBehaviour, IEventsListener
//{

//    public bool IsHuman;

//    public bool IsMyTurn;
//    protected Action<int> OnHeightChange;


//    [HideInInspector] protected bool IsComputer;
//    public Color InActiveColor;


//    private int _heightIndex;
//    public int HeightIndex { get => _heightIndex; set => SetHeight(value); }




//    private void Awake()
//    {
//        SubscribeEvents();
//    }

//    private void SetHeight(int value)
//    {
//        if (value != _heightIndex && IsMyTurn)
//        {
//             OnHeightChange?.Invoke(value);
//            _heightIndex = value;
//        }
//    }

//    public void Pass(Paddle paddle, Color color)
//    {
//        IsMyTurn = paddle != this;
//        InActiveColor = color;
//        OnGettingPass();
//    }

//    public abstract void OnGettingPass();

//    public virtual void UpdateDiceDisplay(int summ) => summ += 2;

//    private void OnDestroy()
//    {
//        UnSubscribeEvents();
//    }


//    public void SubscribeEvents()
//    {
//        Events.OnPassBall += Pass;
//        OnHeightChange += UpdateDiceDisplay;
//    }

//    public void UnSubscribeEvents()
//    {
//        Events.OnPassBall -= Pass;
//        OnHeightChange -= UpdateDiceDisplay;

//    }
//}

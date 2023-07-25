using System;
using UnityEngine;
using System.Collections.Generic;

public class Events : MonoBehaviour 
{
    public static Action<Paddle, Color> OnPassBall;
    public static Action OnCompleteLoop;
    public static Action OnApplyBonus;
    public static Action OnFail;


    public static int loopsCounter { get; private set; }
    public static int beatCounter  { get; private set; }


    public static Action OnPaddleHeightChange;




    private void Awake()
    {
        OnFail += Reset;
    }

    private void OnDestroy()
    {
        OnFail -= Reset;
    }


    public static void SetBeat(int maxValue)
    {
        if (beatCounter < maxValue) 
        {
            beatCounter++; 
        }
        else
        {
            loopsCounter++;
            ResetBeat();
            OnCompleteLoop?.Invoke();
        }
    }

    public static void Reset()
    {
        loopsCounter = 0;
        beatCounter = 0;
    }

    private static void ResetBeat() => beatCounter = 0;




} 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Serialization;

public class LoadingPercentage : MonoBehaviour
{
    [SerializeField] private Text displayPercentage;
    private int _perc;
    private bool _count;

    public enum Types {  UnityWebgl, Hourglass, Mac }
    public Types type;

    float _angle;


    [FormerlySerializedAs("Bar")] [SerializeField] private Transform bar;

    private void Start()
    {
        Action doStuff = null;
        if (type.Equals(Types.Mac))
            doStuff = MacSpinner;
        else if (type.Equals(Types.UnityWebgl))
            doStuff = UnityUnLoaderStart;
        else
            return;
    }


   private IEnumerator Calculate(Action type)
    {
        while (true)
        {
            type();
            yield return null;
        }
    }


    public void UnityUnLoaderStart()
    {
        StartCoroutine(UnityUnload());
    }


    private IEnumerator UnityUnload()
    {
        while (bar.localScale.x > 0f)
        {
            yield return null;
            bar.localScale += Vector3.left * Time.deltaTime;
        }
        yield return null;
        bar.localScale = new Vector3(0,1,1);
        StartCoroutine(UnityLoad());


    }

    IEnumerator UnityLoad()
    {
        displayPercentage.enabled = true;
        while (true)
        {
            if(bar.localScale.x > 0f)
            bar.localScale += Vector3.left * 0.005f;

            if (Input.anyKey && bar.localScale.x < 1f)
                bar.localScale += Vector3.right * 0.05f;
            if (bar.localScale.x >= 1f)
                LoadNextLevel.Go();

            yield return null;
        }

    }


    public void SandCounter(int range)
    {
        _perc += range;
        displayPercentage.text = _perc + "%";
        if (bar != null)
        {
            bar.localScale += Vector3.right * (range * 0.01f);
        }

    }


    public void MacSpinner()
    {
            if (Mathf.Abs(_angle) < 360)
            {
                _angle += WinSpinner.DeltaAngle;
            }
            else
            {
                _perc += (_angle > 0 ? -1 : 1);
                _angle = 0;
            }


        displayPercentage.text = _perc + "%";

        if (_perc >= 100)
            LoadNextLevel.Go();

    }



}

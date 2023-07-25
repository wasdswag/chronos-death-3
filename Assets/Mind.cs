using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Mind : MonoBehaviour
{
    [SerializeField] private Transform projection;
    private bool _firstCollected;
    public static Action<float> OnCollectedFlower;


    private void Start()
    {
        OnCollectedFlower += SetProjectionSize;
    }


    public void SetProjectionSize(float ammount)
    {
        if(_firstCollected == false)
        {
            projection.localScale = Vector3.one;
            _firstCollected = true;
        }
        var previousScale = projection.localScale;
        projection.localScale = new Vector3(previousScale.x + ammount, previousScale.y + ammount, 1f);
    }

    public float GetProjectionSize()
    {
        return projection.localScale.y;
    }



    private void OnDestroy()
    {
        OnCollectedFlower -= SetProjectionSize;
    }




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandCounter : MonoBehaviour
{

    [SerializeField] private LoadingPercentage loadingProgress;
    [SerializeField] private SandCounter otherBowl;
    private bool _isDown;
    private Camera _cam;
    public float dot;
    public bool activeBowl;
    private int _counter; 


    private void Start()
    {
        _cam = Camera.main;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        dot = Vector3.Dot(_cam.transform.up, transform.up);

        if(dot > 0 && collision.TryGetComponent(out Grain grain))
        {
            if(activeBowl)
            {
                grain.isCounted = true;
                loadingProgress.SandCounter(3);

                if (_counter < HourGlassManager.MaxSandsPerLap)
                    _counter++;
                else
                    NextTurn();
            }
            else
            {
                loadingProgress.SandCounter(-3);
            }
        }

     

    }


    private void NextTurn()
    {
        activeBowl = false;
        otherBowl.activeBowl = !otherBowl.activeBowl;
        _counter = 0;

        foreach (Grain s in HourGlassManager.Grains)
            s.isCounted = false;


    }


}

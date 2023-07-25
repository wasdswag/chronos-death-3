using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gate : MonoBehaviour
{

    [SerializeField] private Text counter;
    private int _score;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Ball ball))
        {
            //score ++;
            //counter.text = score.ToString();
            Debug.LogWarning("RESTART!");
            ball.StartGame();
        }

    }


}

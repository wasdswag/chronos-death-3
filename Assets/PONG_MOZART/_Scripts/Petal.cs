using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Petal : MonoBehaviour
{
    [SerializeField] private Text variation;
    [SerializeField] private SpriteRenderer background;

    private void Start()
    {

    }



    public void Activate(int number, Color color)
    {
        variation.text = number.ToString();
        //variation.color = color;
        background.color = color;
    }

}

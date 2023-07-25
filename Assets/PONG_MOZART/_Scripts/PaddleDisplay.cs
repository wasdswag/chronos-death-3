using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleDisplay : MonoBehaviour
{

    [SerializeField] private Graphics graphics;
    [SerializeField] private SpriteRenderer upDice, downDice;


    public void SetDiceColor(bool isYourTurn, Color inActive)
    {
        upDice.color = isYourTurn ? Color.white : inActive;
        downDice.color = upDice.color;
    }


    public void DisplayDice(int summ)
    {
        int up = 0;
        int down = 0;
        int dif = 0;

        if (summ <= 6)
        {
            dif = summ - 1;
            up = dif - Random.Range(0, dif);
            down = summ - up;
        }

        else
        {
            dif = summ - 6;
            up = Random.Range(dif, 7);
            down = summ - up;
        }

        upDice.sprite = graphics.dice[up - 1]; //Emoji[Random.Range(0, graphics.Emoji.Length-1)];
        downDice.sprite = graphics.dice[down - 1];
    }

}

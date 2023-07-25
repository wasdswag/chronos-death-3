using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour, IModifier
{

    public Bonus (GameObject obj, Vector3 pos)
    {
        Instantiate(obj, pos, Quaternion.identity);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out Ball ball))
        {
            if (ball.isPassFromHuman)
            {
                if (ball.positionHint.gameObject.activeSelf == false)
                    ball.positionHint.gameObject.SetActive(true);
                else
                    ball.positionHint.GetComponent<Abilitity>().@continue = true;

                Destroy(gameObject);
            }
    
        }
    }


}

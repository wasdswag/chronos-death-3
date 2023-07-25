using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{


    private SpriteRenderer _look;




    private void Awake()
    {
        _look = GetComponent<SpriteRenderer>();
    }


    public void ChangeColor(Color color)
    {
        _look.color = color;
    }



    public void LookAt(Vector3 target)
    {
        target = new Vector3(target.x, transform.position.y, target.z);

        Vector3 relativePos = target - transform.position;
        transform.rotation = Quaternion.LookRotation(relativePos, Vector3.up);

    }








}

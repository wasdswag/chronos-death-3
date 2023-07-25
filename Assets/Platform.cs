using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private Collider _collision;
    private Transform _heroFeet;

    private void Start()
    {
        _collision = GetComponent<Collider>();
        _heroFeet = FindObjectOfType<Body>().feet;
    }

   


}

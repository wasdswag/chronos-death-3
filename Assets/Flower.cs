using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{

    [SerializeField] private float power = 1.2f;


    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Mind mind))
        {
            Mind.OnCollectedFlower?.Invoke(power);
            Destroy(gameObject);

        }

    }


}

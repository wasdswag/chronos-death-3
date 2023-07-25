using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bagage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoDisplay;
    public int ammo { get; private set; }

    private void Start()
    {
        Mind.OnCollectedFlower += SetCollectedFlowers;
        ammo = 10;
    }


    private void SetCollectedFlowers(float a)
    {
        ammo += (int)Mathf.Ceil(a);
        ammoDisplay.text = "x " + ammo.ToString();
    }

    public void RemoveFlower()
    {
        ammo--;
                ammoDisplay.text = "x " + ammo.ToString();


    }


    private void OnDestroy()
    {
        Mind.OnCollectedFlower -= SetCollectedFlowers;
    }
}

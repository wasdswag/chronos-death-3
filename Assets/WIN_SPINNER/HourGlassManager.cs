using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HourGlassManager : MonoBehaviour
{

    public static int MaxSandsPerLap = 10;
    [SerializeField] private GameObject grainPrefab;
    [SerializeField] private Grain zeroGrain;
    public static List<Grain> Grains = new List<Grain>();
    public int col, row;
    int _counter;
  


    void Start()
    {
        Grains.Add(zeroGrain);

        for (int i = 0; i < col; i++)
        {
            for (int j = 0; j < row; j++)
            {
                Vector2 spawnPos = new Vector2(i * 0.53f, j * 0.53f);
                GameObject grain = Instantiate(grainPrefab, transform);
                grain.name += " " + _counter.ToString();
                grain.transform.localPosition = spawnPos;
                grain.transform.localRotation = Quaternion.Euler(0, 0, -135);
                Grains.Add(grain.GetComponent<Grain>());
                _counter++;
            }
            row++;
        }


    }

  
}

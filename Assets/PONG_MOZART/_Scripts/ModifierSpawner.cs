using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierSpawner : MonoBehaviour
{
    [SerializeField] FieldSettings settings;
    [SerializeField] private Vector2 fieldAreaOffset = new Vector2(3.5f, 2f);

    [SerializeField] private GameObject positionHelper;


    private void Start()
    {
        Events.OnApplyBonus += SpawnPositionHelper;
    }


    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(settings.bounds.leftSide + fieldAreaOffset.x, settings.bounds.rightSide - fieldAreaOffset.x);
        float y = Random.Range(settings.bounds.downSide + fieldAreaOffset.y, settings.bounds.upSide - fieldAreaOffset.y);
        return new Vector3(x, y, 0);
    }

    void SomeMethod<T>() where T : new()
    {

    }

    public void Spawn<T>() where T : IModifier
    {

        

    }




    public void SpawnPositionHelper()
    {
        Instantiate(positionHelper, GetRandomPosition(), Quaternion.identity);
    }


    private void OnDisable()
    {
        Events.OnApplyBonus -= SpawnPositionHelper;
    }

    private void OnDestroy()
    {
        Events.OnApplyBonus -= SpawnPositionHelper;
    }

}

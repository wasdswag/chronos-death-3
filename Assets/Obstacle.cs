using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Obstacle : MonoBehaviour
{

    [FormerlySerializedAs("Collider")] [HideInInspector] public Collider collider;
    [FormerlySerializedAs("IsStartOrEnd")] public bool isStartOrEnd;


    private const string DefaultLayer = "Default";
    private const string Ground = "Ground";

    [SerializeField] private Transform root;
    private Obstacle [] _neigbours;
    [SerializeField] private List<Obstacle> edgeBlocks = new List<Obstacle>();


    private void Awake()
    {
        root = transform.parent;
        _neigbours = root.GetComponentsInChildren<Obstacle>();

        foreach(var block in _neigbours) 
                if (block.isStartOrEnd) 
                edgeBlocks.Add(block);


        collider = GetComponent<Collider>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Projection projection))
        {
            //Debug.Log("Enter");
            if (isStartOrEnd) SetWalkable();
            else if (IsConnectedToEdge()) SetWalkable();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (IsConnectedToEdge() == false)
            SetUnWalkable();
        else if(collider.isTrigger) SetWalkable();
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Projection projection))
            SetUnWalkable();
    }




    private void SetWalkable()
    {
        collider.isTrigger = false;
        gameObject.layer = LayerMask.NameToLayer(Ground);
    }


    private void SetUnWalkable()
    {
        gameObject.layer = LayerMask.NameToLayer(DefaultLayer);
        collider.isTrigger = true;

    }


    private bool IsConnectedToEdge()
    {
        foreach(var edge in edgeBlocks)
            if (edge.collider.isTrigger == false) return true;

        return false;
    }



  



}

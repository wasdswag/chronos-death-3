using System.Collections;
using System.Collections.Generic;
using UIDrama;
using UnityEngine;

public class UItester : MonoBehaviour, IMouseInteractable
{
    
    public void OnEnter()
    {
        Debug.Log("Enter");
    }

    public void OnOver()
    {
        Debug.Log("Over");
    }

    public void OnDown()
    {
        Debug.Log("Down");
    }

    public void OnDrag()
    {
        Debug.Log("Drag");
    }

    public void OnUp()
    {
        Debug.Log("Up");
    }

    public void OnExit()
    {
        Debug.Log("Exit");
    }

    public Vector3 Position => transform.position;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : ActTask
{

    private Renderer _show;

    private void Start()
    {
        _show = GetComponent<Renderer>();
    }

    public void OnMouseDown()
    {
        _show.material.color = Color.green;
        isComplete = true;
        progress.CheckAllTasksCompleted();
    }

}

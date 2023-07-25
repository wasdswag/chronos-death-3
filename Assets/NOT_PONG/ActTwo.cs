using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActTwo : ActTask
{

    public void OnClick()
    {
        isComplete = true;
        progress.CheckAllTasksCompleted();

    }


}

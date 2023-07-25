using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActProgress : MonoBehaviour
{

    [SerializeField] private int nextActIndex;
    private ActTask [] _tasks;


    private void Start()
    {
        _tasks = FindObjectsOfType<ActTask>();
    }


    public bool CheckAllTasksCompleted()
    {
        foreach (var task in _tasks)
        {
            if (task.isComplete == false)
                return false;
        }


        LoadNextAct();
        return true;

    }


    private void LoadNextAct()
    {

        Debug.Log("ACT IS COMPLETED");
        SceneManager.LoadScene(nextActIndex);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class ActTask : MonoBehaviour, ITask
{
    private bool _isComplete;
    public bool isComplete { get => _isComplete; set => _isComplete = value; }

    [FormerlySerializedAs("Progress")] public ActProgress progress;

    private void Awake()
    {
        progress = FindObjectOfType<ActProgress>();
    }


}

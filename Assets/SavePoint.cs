using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    private PlayerProgress _progress;

    private void Start()
    {
        _progress = GetComponentInParent<PlayerProgress>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Body player))
        {
            _progress.SetLastSavePoint(this);
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgress : MonoBehaviour
{

    private SavePoint _latest;

    public Vector3 SetLastSavePoint(SavePoint point)
    {
        _latest = point;
        return _latest.transform.position;
    }


    public Vector3 GetLastSavePoint()
    {
        return _latest.transform.position;
    }

}

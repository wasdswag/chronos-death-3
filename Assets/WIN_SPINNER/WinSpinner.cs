using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinSpinner : MonoBehaviour
{
    public static float DeltaAngle;
    [SerializeField] private bool isWinHourGlass;
    [SerializeField] private Camera camera;
    [SerializeField] LoadingPercentage loadingPercentage;

    private float _previousAngle, _currentAngle, _angle;
   

    private int _percent;
    private bool _count;


    private void OnMouseDown()
    {
        _previousAngle = GetAngle();
        transform.rotation = Quaternion.Euler(Vector3.forward * _currentAngle);

    }


    private void OnMouseDrag()
    {
        _currentAngle = GetAngle();
        DeltaAngle = Mathf.DeltaAngle(_currentAngle, _previousAngle);
        _previousAngle = _currentAngle;
        //transform.Rotate(Vector3.back, deltaAngle);

        transform.rotation = Quaternion.Euler(Vector3.back * _currentAngle);

     

    }


    float GetAngle()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = camera.transform.position.z - transform.position.z;

        Vector3 fixPosition = transform.position - camera.ScreenToWorldPoint(mousePos);
        float a = Mathf.Atan2(fixPosition.x, fixPosition.y) * Mathf.Rad2Deg;
        return a;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastSelector : MonoBehaviour
{
    [SerializeField] private Camera gameCamera;
    private Ray _ray;
    private RaycastHit _hit;
    private Forrest _forrest;

    private void Start()
    {
        _forrest = FindObjectOfType<Forrest>();
    }


    private void Update()
    {
        _ray = gameCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));

        if(Physics.Raycast(_ray, out _hit, 100f))
        {
            if(_hit.collider.TryGetComponent(out Tree tree))
            {

                if (Input.GetMouseButtonDown(0))
                    _forrest.RemoveTree(tree);

            }

         
        }

  

    }




   
}

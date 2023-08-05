using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torque : MonoBehaviour
{
    private Rigidbody2D _body;
    [SerializeField] private float angularChangeInDegrees;
    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            var impulse = (angularChangeInDegrees * Mathf.Deg2Rad) * _body.inertia;
            _body.AddTorque(impulse, ForceMode2D.Impulse);
        }
    }
}

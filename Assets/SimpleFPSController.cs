using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof (CharacterController))]
public class SimpleFPSController : MonoBehaviour
{


    [SerializeField] [Range(0.2f, 10f)] private float walkSpeed = 1f;
    [SerializeField] [Range(1f, 20f)] private float runSpeed = 10f;

    [SerializeField] [Range(1f, 50f)] private float mouseSensetivityX;
    [SerializeField] [Range(1f, 50f)] private float mouseSensetivityY;

    [SerializeField] private float headRotationMinAngle;
    [SerializeField] private float headRotationMaxAngle;

    [SerializeField] private Transform head;
    [SerializeField] private Transform feet;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float gravityScale = 9.8f;
    [SerializeField] private float jumpHeight = 10f;
    [SerializeField] private LayerMask groundSurface;

    private const string Side = "Horizontal";
    private const string Forward = "Vertical";
    private const string MouseX = "Mouse X";
    private const string MouseY = "Mouse Y";
    private const string Jum = "Jump";


    private CharacterController _controller;
    private Vector3 _moveDirection;
    private Vector3 _gravitation;
    private float _headRotation;



    private void Start()
    {
        _controller = GetComponent<CharacterController>();
       // Cursor.lockState = CursorLockMode.Locked;
       // Cursor.visible = false;

    }


    private void Update()
    {

        isGrounded = Physics.CheckSphere(feet.position, 0.5f, groundSurface);
        if (isGrounded && _gravitation.y < 0) _gravitation.y = -1f; 
      

        Jump();
        MouseLook();
        Move();
          
    }


    private void Jump()
    {
        if (isGrounded)
        {
            if (Input.GetButtonDown(Jum))
                _gravitation.y = Mathf.Sqrt(jumpHeight * gravityScale);
        }
        else
        {
            _gravitation.y -= gravityScale * Time.deltaTime;
           
        }

        _controller.Move(_gravitation * Time.deltaTime);
    }


    private void MouseLook()
    {
        float bodyRotation = Input.GetAxis(MouseX);
        float rotationAxe = Input.GetAxis(MouseY);

        transform.Rotate(Vector3.up * bodyRotation * mouseSensetivityX * Time.deltaTime);
        _headRotation += rotationAxe * mouseSensetivityY * Time.deltaTime;

        _headRotation = Mathf.Clamp(_headRotation, headRotationMinAngle, headRotationMaxAngle);
        head.localRotation = Quaternion.Euler(Vector3.left * _headRotation);

    }


    private void Move()
    {
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        float side = Input.GetAxis(Side);
        float forward = Input.GetAxis(Forward);

        _moveDirection = transform.right * side + transform.forward * forward;
        _controller.Move(_moveDirection * currentSpeed * Time.deltaTime);
    }


}

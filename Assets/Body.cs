using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

[RequireComponent (typeof (CharacterController))]
public class Body : MonoBehaviour
{


    [SerializeField] [Range(0.2f, 10f)] private float walkSpeed = 1f;
    [SerializeField] [Range(1f, 20f)] private float runSpeed = 10f;
    [SerializeField] [Range(0.0001f, 0.5f)] private float feetCheckerRadius;


    [FormerlySerializedAs("Feet")] public Transform feet;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float gravityScale = 9.8f;
    [SerializeField] private float jumpHeight = 10f;
    [SerializeField] private LayerMask groundSurface;

    private const string SIDE = "Horizontal";
    private const string FORWARD = "Vertical";
    private const string MOUSE_X = "Mouse X";
    private const string MOUSE_Y = "Mouse Y";
    private const string JUMP = "Jump";


    private CharacterController _controller;
    private Vector3 _moveDirection;
    [SerializeField] private Vector3 gravitation;

    private int _currentSceneIndex;

    private PlayerProgress _progress;



    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        _progress = FindObjectOfType<PlayerProgress>();

    }

    private void Restart()
    {
        _controller.enabled = false;
        transform.position = _progress.GetLastSavePoint();
        _controller.enabled = true;

    }


    private void Update()
    {
        
        
        
        isGrounded = Physics.CheckSphere(feet.position, feetCheckerRadius, groundSurface);
        if (isGrounded && gravitation.y < 0) gravitation.y = 0f;



        if (_controller.enabled)
        {
            Jump();
            Move();
        }
        if (transform.position.y < -5) Restart();


    }


    private void Jump()
    {
        if (isGrounded)
        {
            if (Input.GetButtonDown(JUMP) || Input.GetKeyDown(KeyCode.UpArrow))
                gravitation.y = Mathf.Sqrt(jumpHeight * gravityScale);
        }
        else
        {
            gravitation.y -= gravityScale * Time.deltaTime;

        }

        _controller.Move(gravitation * Time.deltaTime);
    

    }


 

    private void Move()
    {
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        float side = Input.GetAxisRaw(SIDE);
        _moveDirection = transform.right * side;
        _controller.Move(_moveDirection * currentSpeed * Time.deltaTime);
    }




}


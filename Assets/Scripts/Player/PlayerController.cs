using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;


public class PlayerController : MonoBehaviour
{






    private Rigidbody rb;
    private PlayerInputs inputs;

    private InputAction move;
    private InputAction jump;
    private InputAction shoot;
    private InputAction cam;

    public InputAction CameraAction => cam;


    public Vector3 PlayerPosition { get; private set; }


    public static PlayerController Instance { get; private set; }


    private void Awake()
    {
        if (Instance == null) Instance = this;
        inputs = new PlayerInputs();

    }

    private void OnDestroy()
    {
        Instance = null;
    }

    private void OnEnable()
    {



        move = inputs.Player.Movement;
        jump = inputs.Player.Jump;
        shoot = inputs.Player.Shoot;
        cam = inputs.Player.Camera;

        move.Enable();
        jump.Enable();
        shoot.Enable();
        cam.Enable();
    }



    public void DisableExternal()
    {
        OnDisable();
    }

    private void OnDisable()
    {

        move.Disable();
        jump.Disable();
        shoot.Disable();
        cam.Disable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();




        grounded = true;
        jump.performed += Jump;

        shoot.started += (c) =>
        {
            if (c.canceled) return;
            OnPlayerShootingStart?.Invoke();
        };
        shoot.canceled += (c) => OnPlayerShootingEnd?.Invoke();




    }



    public event Action OnPlayerShootingStart, OnPlayerShootingEnd;


    private Vector2 moveDir;

    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float groundingDistance;


    private void Update()
    {
        moveDir = move.ReadValue<Vector2>();
        //camTurn += cam.ReadValue<Vector2>();
    }



    private void FixedUpdate()
    {
        PlayerPosition = rb.position;

        Debug.DrawRay(transform.position, Vector3.down*groundingDistance, Color.magenta);
        if (Physics.Raycast(transform.position, Vector3.down, groundingDistance, groundMask))
        {
            Debug.Log("grounded");
            grounded = true;
        }





        var rbY = rb.velocity.y;

        rb.velocity =
        rbY * Vector3.up +
        moveSpeed * Time.fixedDeltaTime * ((transform.forward * moveDir.y) + (transform.right * moveDir.x));










    }


    private bool grounded;
    private void Jump(InputAction.CallbackContext context)
    {
        if (!grounded) return;


        grounded = false;

        
        rb.AddForce(jumpSpeed * Time.fixedDeltaTime * Vector3.up, ForceMode.Impulse);



    }





}

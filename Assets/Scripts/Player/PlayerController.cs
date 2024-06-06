using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.Assertions;


/// <summary>
/// Manages events from the PlayerInput asset class, handles movement and jumping.
/// </summary>
public class PlayerController : MonoBehaviour
{




    private Vector2 moveDir;

    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float groundingDistance;

    private Rigidbody rb;
    private PlayerInputs inputs;

    private InputAction move;
    private InputAction jump;
    private InputAction shoot;
    private InputAction cam;
    private bool grounded;
    public InputAction CameraAction => cam;
    public Vector3 PlayerPosition { get; private set; }


    public static PlayerController Instance { get; private set; }

    public event Action OnPlayerShootingStart, OnPlayerShootingEnd;



    public void DisableExternal()
    {
        OnDisable();
    }

    private void Awake()
    {
        Assert.IsTrue(moveSpeed > 0 && jumpSpeed > 0 && groundingDistance >0);



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



    


    private void Update()
    {
        moveDir = move.ReadValue<Vector2>();
    }



    private void FixedUpdate()
    {
        PlayerPosition = rb.position;

       

        grounded = Physics.Raycast(transform.position, Vector3.down, groundingDistance, groundMask);

        

        var rbY = rb.velocity.y;

        rb.velocity =
        rbY * Vector3.up +
        moveSpeed * Time.fixedDeltaTime * ((transform.forward * moveDir.y) + (transform.right * moveDir.x));










    }



    private void Jump(InputAction.CallbackContext context)
    {
        if (!grounded) return;


        grounded = false;

        
        rb.AddForce(jumpSpeed * Time.fixedDeltaTime * Vector3.up, ForceMode.Impulse);



    }





}

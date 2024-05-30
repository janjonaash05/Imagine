using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using UnityEngine.UIElements;
using System;


public class PlayerController : MonoBehaviour
{






    private Rigidbody rb;
    private PlayerInputs inputs;

    private InputAction move;
    private InputAction jump;
    private InputAction shoot;
    private InputAction cam;

    public Vector3 PlayerPosition { get; private set; }


    public static PlayerController Instance { get; private set; }


    private void Awake()
    {
        inputs = new PlayerInputs();

    }

    private void OnDestroy()
    {
        Instance = null;
    }

    private void OnEnable()
    {
        if (Instance == null) Instance = this;
        

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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;



        grounded = true;
        jump.performed += Jump;

        shoot.performed += (_) => OnPlayerAttemptShot?.Invoke();



    }



    public event Action OnPlayerAttemptShot;


    private Vector2 moveDir;
    private Vector2 camTurn;
    [SerializeField] private LayerMask mask;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;


    void Update()
    {
        moveDir = move.ReadValue<Vector2>();
        camTurn += cam.ReadValue<Vector2>();
    }



    private void FixedUpdate()
    {
        PlayerPosition = rb.position;

        // Debug.DrawRay(rb.position, -transform.up * 5, Color.green);
        if (Physics.Raycast(rb.position, -transform.up, 1f, mask))
        {
            grounded = true;
        }

      

        rb.velocity = moveSpeed * Time.fixedDeltaTime * ((transform.forward * moveDir.y) + (transform.right * moveDir.x));




        rb.rotation = Quaternion.Euler(0, camTurn.x, 0); ;




       
    }


    private bool grounded;
    private void Jump(InputAction.CallbackContext context)
    {
        if (grounded)
        {
           
            grounded = false;

            rb.AddForce(jumpSpeed * Vector3.up, ForceMode.Impulse);


        }
    }





}

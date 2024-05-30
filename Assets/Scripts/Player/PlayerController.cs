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


public class PlayerController : MonoBehaviour
{




    

        private Rigidbody rb;
        private PlayerInputs inputs;

        private InputAction move;
        private InputAction jump;
        private InputAction shoot;
        private InputAction cam;



        private void Awake()
        {
            inputs = new PlayerInputs();
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

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;



            grounded = true;
            jump.performed += Jump;
        }

        private Vector2 moveDir;
        private Vector2 camTurn;
        [SerializeField] LayerMask mask;
        [SerializeField] float moveSpeed;
        [SerializeField] float jumpSpeed;


        void Update()
        {
            moveDir = move.ReadValue<Vector2>();
            camTurn += cam.ReadValue<Vector2>();
        }

        

        private void FixedUpdate()
        {
            

           // Debug.DrawRay(rb.position, -transform.up * 5, Color.green);
            if (Physics.Raycast(rb.position, -transform.up, 1f, mask))
            {
                grounded = true;
            }

        Debug.Log(grounded);

            rb.velocity = moveSpeed * Time.fixedDeltaTime * ((transform.forward * moveDir.y) + (transform.right * moveDir.x));
            
            


            rb.rotation = Quaternion.Euler(0, camTurn.x, 0); ;




            Debug.Log(moveDir);
        }


        private bool grounded;
        private void Jump(InputAction.CallbackContext context)
        {
        if (grounded)
        {
            Debug.LogError("JUMP");
            grounded = false;

            rb.AddForce(jumpSpeed * Vector3.up,ForceMode.Impulse);
                

            }
        }





    }

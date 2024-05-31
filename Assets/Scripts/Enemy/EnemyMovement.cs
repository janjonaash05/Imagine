using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update



    [SerializeField] protected float speed;
    protected Rigidbody rb;

    private PlayerController player;


    protected float startY;


    private delegate Vector3 MovementAdjust();




    private void Start()
    {

        player = PlayerController.Instance;
    }


    protected void Awake()
    {
        Assert.IsTrue(speed > 0);



        rb = GetComponent<Rigidbody>();
        startY = rb.position.y;


    }

    
    private void FixedUpdate()
    {






        var direction = new Vector3(player.PlayerPosition.x, rb.position.y, player.PlayerPosition.z) - rb.position;


        rb.MovePosition(rb.position + (speed * Time.fixedDeltaTime * direction));
        rb.position = AfterMoveAdjust();


    }


    protected virtual Vector3 AfterMoveAdjust()
    {
        return new(rb.position.x, startY, rb.position.z);
    }










}

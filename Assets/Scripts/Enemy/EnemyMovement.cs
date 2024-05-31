using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update



    [SerializeField] protected float speed;
    protected Rigidbody rb;

    private PlayerController player;


    protected float startY;

    [SerializeField] private float stopDistance;
    [SerializeField] private LayerMask stopMask;

    private delegate Vector3 MovementAdjust();

    private RadiusDetection radiusDetection;


    /*
    public enum MovementType {REGULAR, SIN }


    private Dictionary<MovementType, MovementAdjust> typeAdjustDict;

    [SerializeField] private MovementType type;
    private MovementAdjust typeAdjust;
    */


    private void Start()
    {

        player = PlayerController.Instance;
    }


    private void Awake()
    {
        
        


        rb = GetComponent<Rigidbody>();
        startY = rb.position.y;


    }

    
    private void FixedUpdate()
    {

        
         

     
            
        var direction = new Vector3(player.PlayerPosition.x, rb.position.y, player.PlayerPosition.z) - rb.position;


        if (Physics.OverlapSphereNonAlloc(transform.position, stopDistance, null, stopMask, QueryTriggerInteraction.Collide) == 0)
        {
            rb.MovePosition(rb.position + (speed * Time.fixedDeltaTime * direction));
        }


        rb.position = AfterMoveAdjust();


    }


    protected virtual Vector3 AfterMoveAdjust() 
    {
        return new(rb.position.x, startY, rb.position.z);
    }


    


   




}

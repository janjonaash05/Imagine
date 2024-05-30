using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update



    [SerializeField] private float speed;
    private Rigidbody rb;

    private PlayerController player;


    private float startY;



   private delegate Vector3 MovementAdjust(Rigidbody r);


    public enum MovementType {REGULAR, SIN }


    private Dictionary<MovementType, MovementAdjust> typeAdjustDict;

    [SerializeField] private MovementType type;
    private MovementAdjust typeAdjust;

    private void Start()
    {

       

        typeAdjustDict = new()
        {
            { MovementType.REGULAR, (x) => x.position },
            { MovementType.SIN, (x) => new(rb.position.x, (Mathf.Sin(Time.fixedTime) * speed * 10 * Time.fixedDeltaTime) + startY, rb.position.z)  }
        };


        typeAdjust = typeAdjustDict[type];

    }


    private void Awake()
    {

        rb = GetComponent<Rigidbody>();
        startY = rb.position.y;


    }
    private void FixedUpdate()
    {
        player = PlayerController.Instance;
        var direction = new Vector3(player.PlayerPosition.x, rb.position.y, player.PlayerPosition.z) - rb.position;



        rb.MovePosition(rb.position + (speed * Time.fixedDeltaTime * direction));

        //rb.position = new(rb.position.x, (Mathf.Sin(Time.fixedTime) * speed * 10 * Time.fixedDeltaTime) + startY, rb.position.z);

        rb.position = typeAdjust(rb);


    }






}

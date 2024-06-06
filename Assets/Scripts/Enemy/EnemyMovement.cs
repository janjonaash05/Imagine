using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


/// <summary>
/// Manages enemy's movement via its RigidBody.
/// </summary>
public class EnemyMovement : MonoBehaviour
{

    [SerializeField] protected float speed;
    protected Rigidbody rb;
    protected float startY;
    private PlayerController player;
    private delegate Vector3 MovementAdjust();


    protected virtual Vector3 AfterMoveAdjust()
    {
        return new(rb.position.x, startY, rb.position.z);
    }

    protected void Awake()
    {
        Assert.IsTrue(speed > 0);
        rb = GetComponent<Rigidbody>();
        startY = rb.position.y;


    }

    private void Start()
    {

        player = PlayerController.Instance;
    }


    private void FixedUpdate()
    {

        var direction = (new Vector3(player.PlayerPosition.x, rb.position.y, player.PlayerPosition.z) - rb.position).normalized;

        rb.MovePosition(rb.position + (speed * Time.fixedDeltaTime * direction));
        rb.position = AfterMoveAdjust();

    }





}

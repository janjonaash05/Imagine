using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class CameraHolderRotation : MonoBehaviour
{
    private Vector2 camTurn;
    [SerializeField] private Vector2 camClamp;
    private void Start()
    {
        controller = PlayerController.Instance;
    }


    private PlayerController controller;
    
    


    private void Update()
    {
        camTurn += controller.CameraAction.ReadValue<Vector2>();
        Debug.Log(camTurn);
        camTurn.y =  Mathf.Clamp(camTurn.y, camClamp.x, camClamp.y);
        transform.localRotation = Quaternion.Euler(-camTurn.y, 0, 0);

        


    }
}

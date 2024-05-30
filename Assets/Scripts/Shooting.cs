using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] RadiusDetection radiusDetection;
    [SerializeField] GameObject projectilePrefab;
    void Start()
    {
        radiusDetection.OnObjectCaught +=()    
    }




    void ShootAt(GameObject g) 
    {
        
    
    
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

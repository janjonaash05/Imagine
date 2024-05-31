using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorChange : MonoBehaviour
{
    [SerializeField] private float delayTime;
   private void Start()
    {
        
    }

    IEnumerator ColorChange()
    {
        var delay = new WaitForSeconds(delayTime);


        
    };

}

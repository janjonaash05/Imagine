using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    

    public override void AfterDeathAction()
    {

        SceneManager.LoadScene(0);
       
    }

    void Start()
    {
        PlayerHUD.Instance.SetupHPLabel(baseHealth);
    }

    public override void MidDamageAction()
    {
       
        PlayerHUD.Instance.UpdateHPLabel(health);
    }

}

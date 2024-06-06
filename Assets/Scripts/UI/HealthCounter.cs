using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthCounter : MonoBehaviour
{
    private TextMeshProUGUI healthLbl;

    public void Awake()
    {

        healthLbl = GetComponent<TextMeshProUGUI>();

        
    }

    public void UpdateHPLabel(int health, int baseHealth)
    {
        healthLbl.text = health + "/" + baseHealth;

    }

    private void Start()
    {
        PlayerHealth.Instance.OnHealthUpdated += UpdateHPLabel;
    }
}

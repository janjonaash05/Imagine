using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using static EnemyID;
public class PlayerHUD : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI killedLabel;
    [SerializeField] private TextMeshProUGUI hpLabel;


   private Dictionary<EnemyType, int> typeKilledDict;



   private EnemyType[] enemyTypes;

    [SerializeField] private float killedLabelEntryOffset;


    public static PlayerHUD Instance { get; private set; }


    public void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;    
    }

    private void Start()
    {
        typeKilledDict = new();
        enemyTypes = Enum.GetValues(typeof(EnemyType)).Cast<EnemyType>().ToArray();
        

        foreach (var item in enemyTypes) 
        {
            typeKilledDict.Add( item, 0);    
        
        }



        ChangeKilledLabel();

    }


    private (string killedString, int offsetMultiplier) GetKilledStringAndOffsetMultiplier()
    {
        int offset = 0;
        var builder = new StringBuilder();
        builder.AppendLine("Killed");
        foreach (var item in enemyTypes)
        {
            if (typeKilledDict[item] == 0) continue;

            builder.AppendLine(item.ToString() + ": " + typeKilledDict[item]);
            offset++;

        }

        return (builder.ToString(), offset);

    }


    public void AddKill(EnemyType type)
    {
        typeKilledDict[type]++;
        ChangeKilledLabel();
    }

    private void ChangeKilledLabel() 
    {
        var (killedString, offsetMultiplier) = GetKilledStringAndOffsetMultiplier();
        killedLabel.text = killedString;
        killedLabel.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, killedLabelEntryOffset * offsetMultiplier);

    }


    private int baseHealth;
    public void SetupHPLabel(int baseHealth)
    {
        this.baseHealth = baseHealth;
        hpLabel.text = baseHealth + "/" + baseHealth;
    }

    public void UpdateHPLabel(int health)
    {
        hpLabel.text = health + "/" + baseHealth;
    }



}

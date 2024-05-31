using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using static EnemyID;
public class PlayerHUD : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI killedLabel;
    [SerializeField] private TextMeshProUGUI hpLabel;


    private Dictionary<EnemyType, int> typeKilledDict;

    private Vector2 killedLabelStartPos;
    private RectTransform killedLabelRectTrans;

    private List<EnemyType> enemyTypes;

    [SerializeField] private float killedLabelNewEntryOffset;


    public static PlayerHUD Instance { get; private set; }











    public void Awake()
    {
        Assert.IsNotNull(killedLabel);
        Assert.IsNotNull(hpLabel);
        Assert.IsTrue(resetDelay > 0);
        Assert.IsTrue(killedLabelNewEntryOffset > 0);

        if (Instance == null) Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    private void Start()
    {
        typeKilledDict = new();
        enemyTypes = Enum.GetValues(typeof(EnemyType)).Cast<EnemyType>().ToList();
        killedLabelRectTrans = killedLabel.transform.GetComponent<RectTransform>();
        killedLabelStartPos = killedLabelRectTrans.anchoredPosition;



        foreach (var item in enemyTypes)
        {
            typeKilledDict.Add(item, 0);

        }



        ChangeKilledLabel();
        StartCoroutine(ResetTimer());

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
        killedLabelRectTrans.anchoredPosition = killedLabelStartPos + new Vector2(0, -killedLabelNewEntryOffset * offsetMultiplier);

        updated = true;
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



    private bool updated;
    [SerializeField] private float resetDelay;
    private IEnumerator ResetTimer()
    {

        var delay = new WaitForSeconds(resetDelay);

        while (true)
        {
            updated = false;

            yield return delay;
            if (!updated)
            {
                ResetDict(typeKilledDict);
                ChangeKilledLabel();
            }
            
        
        
        
        }


    }



    private void ResetDict(Dictionary<EnemyType, int> dict)
    {

        foreach (var item in enemyTypes)
        {
            dict[item] = 0;
        }


    }


    




    


}

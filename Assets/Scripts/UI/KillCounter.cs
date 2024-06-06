using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using static EnemyID;

public class KillCounter : MonoBehaviour
{
    private TextMeshProUGUI killedLabel;


    private Dictionary<string, int> nameKilledDict;

    [SerializeField] private float resetDelay;

    private bool updated;

    [SerializeField] private float killedLabelNewEntryOffset;
    [SerializeField] private KillsManagerScriptableObject killsManager;

    private Vector2 killedLabelStartPos;
    private RectTransform killedLabelRectTrans;



    private void Awake()
    {
        Assert.IsNotNull(killsManager, "kills manager is not null");
        Assert.IsTrue(resetDelay >= 0, "Reset delay must be non-negative");
    }

    private void Start()
    {
        killedLabel = GetComponent<TextMeshProUGUI>();

        nameKilledDict = new();



        killedLabelRectTrans = killedLabel.transform.GetComponent<RectTransform>();
        killedLabelStartPos = killedLabelRectTrans.anchoredPosition;



        ChangeKilledLabel();
        StartCoroutine(ResetTimer());







        killsManager.OnNamedEnemyKilled += AddKill;
    }




    private void OnDestroy()
    {
        killsManager.OnNamedEnemyKilled -= AddKill;
    }

    private void AddKill(string name)
    {
        updated = true;
        if (!nameKilledDict.ContainsKey(name))
        {
            nameKilledDict.Add(name, 1);
            return;
        }

        nameKilledDict[name]++;


    }

    private void ChangeKilledLabel()
    {
        var (killedString, offsetMultiplier) = GetKilledStringAndOffsetMultiplier();
        killedLabel.text = killedString;
        killedLabelRectTrans.anchoredPosition = killedLabelStartPos + new Vector2(0, -killedLabelNewEntryOffset * offsetMultiplier);


    }



    private (string killedString, int offsetMultiplier) GetKilledStringAndOffsetMultiplier()
    {
        int offset = 0;
        var builder = new StringBuilder();
        builder.AppendLine("Killed");
        foreach (var item in nameKilledDict.Keys)
        {
            if (nameKilledDict[item] == 0) continue;

            builder.AppendLine(item.ToString() + ": " + nameKilledDict[item]);
            offset++;

        }

        return (builder.ToString(), offset);

    }
    private IEnumerator ResetTimer()
    {

        var delay = new WaitForSeconds(resetDelay);

        while (true)
        {
            updated = false;

            yield return delay;
            if (!updated)
            {
                nameKilledDict = new();
            }


            ChangeKilledLabel();

        }


    }


}

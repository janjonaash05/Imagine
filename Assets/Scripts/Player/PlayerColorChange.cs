using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


/// <summary>
/// Gradually changes the player's base and emission material color over time, resembling a rainbow.
/// </summary>
public class PlayerColorChange : MonoBehaviour
{
    [SerializeField] private float delayTime;
    private Renderer rend;


    private void Awake()
    {
        Assert.IsTrue(delayTime >=0);
    }

    private void Start()
    {
        rend = GetComponent<Renderer>();
        StartCoroutine(ColorChange());
    }

    private enum State
    {
        RedToGreen, RedToNone, GreenToBlue, GreenToNone, BlueToRed, BlueToNone,
    }

    private IEnumerator ColorChange()
    {
        var delay = new WaitForSeconds(delayTime);

        var color = new Vector3Int(255, 0, 0);


        var stateDict =
            new Dictionary<State, (Vector3Int colorChange, State nextState, Predicate<Vector3Int> changeCondition)>()
            {
                { State.RedToGreen, (new (0,1,0), State.RedToNone, (v) => v.y == 255)  },
                { State.RedToNone, (new (-1,0,0), State.GreenToBlue, (v) => v.x == 0)  },
                { State.GreenToBlue, (new (0,0,1), State.GreenToNone, (v) => v.z == 255)  },
                { State.GreenToNone, (new (0,-1,0), State.BlueToRed, (v) => v.y == 0)  },
                { State.BlueToRed, (new (1,0,0), State.BlueToNone, (v) => v.x == 255)  },
                { State.BlueToNone, (new (0,0,-1), State.RedToGreen, (v) => v.z == 0)  },

            };


        var currentState = State.RedToGreen;

        while (true)
        {

            if (rend == null) yield break;

            var (colorChange, nextState, changeCondition) = stateDict[currentState];
            color += colorChange;

            


            rend.material.color = new Color(color.x / 255f, color.y / 255f, color.z / 255f);
            rend.material.SetColor("_EmissionColor", new Color(color.x / 255f, color.y / 255f, color.z / 255f));

            if (changeCondition(color)) currentState = nextState;


            yield return delay;


        }

    }

}

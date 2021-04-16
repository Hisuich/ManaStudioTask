using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    [SerializeField]
    private SummonUnit summonKnight;

    [SerializeField]
    private SummonUnit summonAxeman;

    [SerializeField]
    private MeteorFall meteorFall;


    private Command activeCommand;

    private void Start()
    {
        activeCommand = summonKnight;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            activeCommand.Activate();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            activeCommand = summonKnight;
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            activeCommand = summonAxeman;
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            activeCommand = meteorFall;
        }
    }

}

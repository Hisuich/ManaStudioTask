using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SummonUnit : Command
{
    [SerializeField]
    private GameObject unit;
    public override void Activate()
    {
        Debug.Log("Activate Summon");
        Debug.Log("Can Activate?" + CanActivate());
        if (CanActivate())
        {
            Vector3 pos = GetMousePos();
            GameObject newUnit = GameObject.Instantiate(unit);
            newUnit.transform.position = pos;
        }
    }
}

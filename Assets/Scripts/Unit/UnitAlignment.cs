using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Alignment
{ 
    Knight,
    Axeman
}

public class UnitAlignment : MonoBehaviour
{
    [SerializeField]
    private Alignment alignment;

    public bool IsUnitEnemy(Unit unit)
    {
        return unit.alignment.GetAlignment() != alignment;
    }

    public Alignment GetAlignment()
    {
        return alignment;
    }
}

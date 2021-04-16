using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
abstract public class Command
{
    [SerializeField]
    protected float cooldownTime;

    protected float prevUseTime = 0;

    protected bool targetFloor = false;
    virtual public bool CanActivate()
    {
        GetMousePos();

        if (!targetFloor)
        {
            return false;
        }
        if (Time.time - prevUseTime < cooldownTime)
        {
            return false;
        }
        else if (Time.time - prevUseTime >= cooldownTime)
        {
            prevUseTime = Time.time;
            return true;
        }
        return false;
    }
    abstract public void Activate();

    public Vector3 GetMousePos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, LayerMask.GetMask("Floor")))
        {
            targetFloor = true;
            return hit.point;
        }

        targetFloor = false;
        return new Vector3(-1000, -1000, -1000);
    }
}

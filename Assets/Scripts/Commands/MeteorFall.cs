using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MeteorFall : Command
{
    [SerializeField]
    private GameObject meteor;



    [SerializeField]
    private float speed;
    public override void Activate()
    {
        if (CanActivate())
        {
            Vector3 mousePos = GetMousePos();
            mousePos.y += Camera.main.transform.position.y;
            float rand = UnityEngine.Random.value-0.5f;
            mousePos.x += (10 * rand);
            mousePos.z += (10 * (1-rand));
            GameObject newMeteor = GameObject.Instantiate(meteor);
            newMeteor.transform.position = mousePos;
            newMeteor.transform.LookAt(GetMousePos());
            newMeteor.GetComponent<Meteor>().SetSpeed(speed);
        }
    }

}

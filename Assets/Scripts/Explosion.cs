using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 3);
    }
    public void Explode()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, 3);

        for (int i = 0; i < collider.Length; i++)
        {
            Debug.Log("unit + " + collider[i].gameObject.name);
            if (collider[i].gameObject.tag == "Unit")
            {
                Debug.Log("Kill unit" + collider[i].gameObject.name);
                collider[i].GetComponentInParent<Unit>().ReceiveDamage(1000);
            }
        }
    }
}

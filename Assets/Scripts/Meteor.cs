using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particle;
    [SerializeField]
    private GameObject explode;

    private float speed = 0;
    public void SetSpeed(float speed)
    {
        this.speed = speed;
        particle.startSpeed = speed;
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.forward);
        transform.Translate(new Vector3(0,0,speed*Time.deltaTime));
    }   

    private void OnCollisionEnter(Collision collision)
    {
            Destroy(gameObject);
            GameObject newExplode = Instantiate(explode);
            newExplode.transform.position = transform.position;
            newExplode.GetComponent<Explosion>().Explode();
    }
}

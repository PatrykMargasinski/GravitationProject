using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    const float G = 666.4f;
    public static List<Gravity> gravities=new List<Gravity>();
    public Rigidbody rb;
    public SphereCollider col;
    public bool gravitationEnabled=true;
    public bool collided=false;
    public bool collisionOff=false;
    public float collisionOffTime=0f;

    void FixedUpdate()
    {
        gravitationEnabled=rb.detectCollisions;
        foreach(Gravity gravity in gravities)
        {
            if (gravity != this && gravitationEnabled==true)
            {
                Attract(gravity);
            }
        }
    }

    void OnEnable()
    {
        gravities.Add(this);
        collided=false;
    }

    void OnDisable()
    {
        gravities.Remove(this);
    }

    private void OnCollisionEnter(Collision other) {
        if(collided==false && gravitationEnabled==true)
        {
        collided=true;
        Bindings.bindings.Enqueue(new Gravity[]{this,other.gameObject.GetComponent<Gravity>()});
        }
    }

    void Attract(Gravity objToAttract)
    {
        Rigidbody rbToAttract = objToAttract.rb;

        Vector3 direction = rb.position - rbToAttract.position;
        float distance = direction.sqrMagnitude;

        if (distance < col.radius*2)
        {
            return;
        }

        float forceMagnitude = G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forceMagnitude;
        if(Controller.limitAchieved!=true) rbToAttract.AddForce(force);
        else rbToAttract.AddForce(-force);
    }

    public void SetNoCollisionTime()
    {
        collisionOff=true;
        collisionOffTime=0.5f;
    }
}

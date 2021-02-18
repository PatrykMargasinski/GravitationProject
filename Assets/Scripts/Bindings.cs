using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bindings : MonoBehaviour
{
    static public Queue<Gravity[]> bindings = new Queue<Gravity[]>();
    public BallPool pool;
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Gravity[] binding;
        while (bindings.Count != 0)
        {
            binding = bindings.Dequeue();
            bool firstIs = Gravity.gravities.Contains(binding[0]);
            bool secondIs = Gravity.gravities.Contains(binding[1]);

            if (firstIs && secondIs)
            {
                float firstMass = binding[0].rb.mass;
                float secondMass = binding[1].rb.mass;
                float newMass = firstMass + secondMass;
                Vector3 newPosition = (binding[0].transform.position * firstMass / newMass + binding[1].transform.position * secondMass / newMass);
                pool.ReturnBall(binding[0].gameObject);
                pool.ReturnBall(binding[1].gameObject);
                if (newMass < 50)
                {
                    Merging(binding[0].gameObject,binding[1].gameObject,newPosition,newMass);
                }
                else
                {
                    Burst(binding[0].gameObject,binding[1].gameObject,newPosition, newMass);
                }
            }
            else
            {
                if (firstIs) binding[0].collided = false;
                if (secondIs) binding[1].collided = false;
            }
        }
    }

    void Merging(GameObject g1, GameObject g2, Vector3 pos, float mass)
    {
        pool.GetBall(pos, mass);
    }

    void Burst(GameObject g1, GameObject g2, Vector3 pos, float mass)
    {
        pool.ReturnBall(g1);
        pool.ReturnBall(g2);
        List<Rigidbody> gravities=new List<Rigidbody>();
        for(int i=0;i<mass;i++)
        {
            gravities.Add(pool.GetBall(pos,1f).GetComponent<Rigidbody>());
            gravities[i].detectCollisions=false;
            gravities[i].velocity=new Vector3(Random.Range(-30f,30f),Random.Range(-30f,30f),Random.Range(-30f,30f));
        }
        StartCoroutine(Masakra(gravities.ToArray()));
    }

    IEnumerator Masakra(Rigidbody[] gravities)
    {
        yield return new WaitForSeconds(0.5f);
        foreach(Rigidbody rb in gravities) rb.detectCollisions=true;
    }
}

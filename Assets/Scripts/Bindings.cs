using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bindings : MonoBehaviour
{
    static public Queue<Gravity[]> bindings=new Queue<Gravity[]>();
    public BallPool pool;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        Gravity[] binding;
        while(bindings.Count!=0)
        {
            binding=bindings.Dequeue();
            bool firstIs=Gravity.gravities.Contains(binding[0]);
            bool secondIs=Gravity.gravities.Contains(binding[1]);

            if(firstIs && secondIs)
            {
                float firstMass=binding[0].rb.mass;
                float secondMass=binding[1].rb.mass;
                float newMass=firstMass+secondMass;

                Vector3 newPosition=(binding[0].transform.position*firstMass/newMass+binding[1].transform.position*secondMass/newMass);

                pool.ReturnBall(binding[0].gameObject);
                pool.ReturnBall(binding[1].gameObject);
                //if(newMass>=50) Burst(newPosition,newMass);
                pool.GetBall(newPosition, newMass); 
            }
            else
            {
                if(firstIs) binding[0].collided=false;
                if(secondIs) binding[1].collided=false;
            }
        }
    }
}

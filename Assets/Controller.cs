using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public static bool limitAchieved=false;
    public BallPool pool;
    private int amount=0;
    public int limit=250;
    public int maxLength=0;
    static public Queue<Gravity[]> bindings=new Queue<Gravity[]>();
    float time=0f;

    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.Escape))Application.Quit();
        time+=Time.deltaTime;
        while(time>0.25f && amount<limit)
        {
            time -= 0.25f; amount++;
            pool.GetBall(new Vector3(Random.Range(-20,20),Random.Range(-20,20),Random.Range(-20,20)),1f);
        }
        if(amount==limit) limitAchieved=true;

        Gravity[] binding;
        while(bindings.Count!=0)
        {
            if(bindings.Count>maxLength)maxLength=bindings.Count;
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

    void Burst(Vector3 position,float mass)
    {
        for(int i=0;i<mass;i++)
        {
            var ball=pool.GetBall(position,1);
            ball.GetComponent<Rigidbody>().mass=0;
            ball.GetComponent<SphereCollider>().enabled=false;
            ball.GetComponent<Rigidbody>().velocity=new Vector3(Random.Range(1f,5f),Random.Range(1f,5f),Random.Range(1f,5f));
            ball.GetComponent<Gravity>().enabled=false;
        }
    }
}

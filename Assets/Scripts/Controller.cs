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

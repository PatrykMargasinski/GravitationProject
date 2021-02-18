using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public static bool limitAchieved=false;
    public BallPool pool;
    public static int amount=0;
    public int limit=250;
    public int maxLength=0;
    float time=0f;

    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.Escape))Application.Quit();
        time+=Time.fixedDeltaTime;
        while(time>0.25f && amount<limit)
        {
            time -= 0.25f; amount++;
            pool.GetBall(new Vector3(Random.Range(-20,20),Random.Range(-20,20),Random.Range(-20,20)),1f);
        }
        if(amount==limit) limitAchieved=true;
    }
}

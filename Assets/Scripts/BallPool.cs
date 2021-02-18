using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPool : MonoBehaviour
{
    public static int ballNumber=0;
    public GameObject ballPrefab;
    public int amount;
    Queue<GameObject> balls;
    void Start()
    {
        balls=new Queue<GameObject>();
        for(int i=0;i<amount;i++)
        {
            var ball=Instantiate(ballPrefab,Vector3.zero,Quaternion.identity);
            ball.SetActive(false);
            balls.Enqueue(ball);
        }
    }

    public GameObject GetBall(Vector3 position, float mass, float scale)
    {
        var ball=balls.Dequeue();
        ball.SetActive(true);
        ball.transform.position=position;
        ball.transform.localScale=Vector3.one*scale;//Vector3.one*Mathf.Sqrt(mass);
        ball.GetComponent<Rigidbody>().mass=mass;
        ballNumber++;
        return ball;
    }
    public GameObject GetBall(Vector3 position, float mass)
    {
        return GetBall(position, mass, Mathf.Sqrt(mass));
    }

    public void ReturnBall(GameObject ball)
    {
        ballNumber--;
        Rigidbody rigidbody=ball.GetComponent<Rigidbody>();
        rigidbody.mass=1f;
        rigidbody.velocity=Vector3.zero;
        rigidbody.angularVelocity=Vector3.zero;
        ball.SetActive(false);
        balls.Enqueue(ball);
    }
}

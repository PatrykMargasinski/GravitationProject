using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Pool przechujący wszystkie kule. Przy uruchomieniu aplikacji tworzone jest domyślnie 250 kul, które będą aktywowane i dezaktywowane
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

    //metoda zwracająca jedną z kul z Poola
    public GameObject GetBall(Vector3 position, float mass, float scale)
    {
        var ball=balls.Dequeue();
        ball.SetActive(true);
        ball.transform.position=position;
        ball.transform.localScale=Vector3.one*scale;
        ball.GetComponent<Rigidbody>().mass=mass;
        ballNumber++;
        return ball;
    }
    public GameObject GetBall(Vector3 position, float mass)
    {
        return GetBall(position, mass, Mathf.Sqrt(mass));
    }

    //metoda zwracająca jedną z kul do Poola
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

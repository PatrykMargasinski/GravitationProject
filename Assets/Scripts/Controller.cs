using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//klasa odpowiedzialna za tworzenie kul co 0.25 sekundy 
public class Controller : MonoBehaviour
{
    public static bool limitAchieved=false; //czy jest {limit} kul, domyślnie 250. Jak tak, wykonywana jest odwrotna grawitacja
    public BallPool pool;
    public static int amount=0;//ilość stworzonych podstawowych kul
    public int limit=250;
    float time=0f;

    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.Escape))Application.Quit();//wylaczenie gry przy wcisnięciu ESC
        time+=Time.fixedDeltaTime;//do zmiennej time dodawany jest czas trwania klatki
        while(time>0.25f && amount<limit)//jeżeli "czas" osiągnął 0.25 sekundy, dodawana jest nowa kula
        {
            time -= 0.25f; amount++;
            pool.GetBall(new Vector3(Random.Range(-20,20),Random.Range(-20,20),Random.Range(-20,20)),1f);
        }
        if(amount==limit) limitAchieved=true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    const float G = 666.4f;//stala grawitacyjna - ustalona jest taka wartość, by kule przyciągały się w przyzwoitym czasie
    public static List<Gravity> gravities=new List<Gravity>();//lista wszystkich obiektów podlegających grawitacji innych obiektów
    public Rigidbody rb;
    public SphereCollider col;
    public bool gravitationEnabled=true;//czy obiekt ma podlegać grawitacji
    public bool collided=false;
    public bool collisionOff=false;
    public float collisionOffTime=0f;

    void FixedUpdate()
    {
        gravitationEnabled=rb.detectCollisions;//jeżeli obiekt nie ma detekcji kolizji, to grawitacja też jest wyłaczana
        foreach(Gravity gravity in gravities)//sprawdzanie oddziaływania grawitacyjnego z każdym obiektem
        {
            if (gravity != this && gravitationEnabled==true)
            {
                Attract(gravity);
            }
        }
    }

    void OnEnable()//aktywowany obiekt z komponentem Gravity jest dodawany do listy gravities
    {
        gravities.Add(this);
        collided=false;
    }

    void OnDisable()//dezatywowany obiekt z komponentem Gravity jest usuwany z listy gravities
    {
        gravities.Remove(this);
    }

    private void OnCollisionEnter(Collision other) {
        if(collided==false && gravitationEnabled==true)
        {
        collided=true;
        Bindings.bindings.Enqueue(new Gravity[]{this,other.gameObject.GetComponent<Gravity>()});
        //wszystkie pary, które się ze sobą stykły są dodawane do listy "bindings", gdzie sprawdzane jest, czy para stworzy nową kule, czy rozprysk
        }
    }

    void Attract(Gravity toAttract)
    {
        Rigidbody rbToAttract = toAttract.rb;

        Vector3 direction = rb.position - rbToAttract.position;
        float distance = direction.sqrMagnitude;

        //jeżeli obiekty są zbyt blisko siebie, efekt grawitacyjny nie jest wykonywany, wartości mogłyby być wtedy zbyt duże
        if (distance < (col.radius+toAttract.col.radius)*1.2f)
        {
            return;
        }

        float forceValue = G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forceValue;

        //jeżeli został osiągnięty limit kul, kule się odpychają, zamiast przyciagać
        if(Controller.limitAchieved!=true) rbToAttract.AddForce(force);
        else rbToAttract.AddForce(-force);
    }
}

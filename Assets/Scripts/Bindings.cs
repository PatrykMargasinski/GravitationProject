using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//klasa w której zdefiniowane są metody realizujące połączenie dwóch kul i rozprysk, 
//gdy masa nowej kuli miałaby być większa od 50
public class Bindings : MonoBehaviour
{
    static public Queue<Gravity[]> bindings = new Queue<Gravity[]>();//para kól, która ze sobą kolidowała jest dodawana do kolejki
    public BallPool pool;

    // Update is called once per frame
    void FixedUpdate()
    {

        Gravity[] binding;
        while (bindings.Count != 0)
        {
            binding = bindings.Dequeue();
            bool firstExist = Gravity.gravities.Contains(binding[0]);
            bool secondExist = Gravity.gravities.Contains(binding[1]);

            if (firstExist && secondExist)
            {
                //jeżeli dwie kule wciąż są na scenie aktywne, pobierane są od nich takie dane jak masa, pozycja
                //na ich podstawie są określane parametry nowej kuli lub rozprysku
                float firstMass = binding[0].GetComponent<Rigidbody>().mass;
                float secondMass = binding[1].GetComponent<Rigidbody>().mass;
                float newMass = firstMass + secondMass;
                //przy określaniu nowej pozycji wykorzystywana jest "średnia ważona" - nowa kula będzie bliżej pozycji starej kuli o większej masie
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
                if (firstExist) binding[0].collided = false;
                if (secondExist) binding[1].collided = false;
            }
        }
    }

    //łączenie dwóch kul w jedną, większą
    void Merging(GameObject g1, GameObject g2, Vector3 pos, float mass)
    {
        pool.GetBall(pos, mass);
    }

    //metoda implementująca rozprysk - tworzone jest ponad 50 kul (w zależności od masy), 
    //które lecą w losowym kierunku z wyłączoną kolizją i grawitacją na 0.5 sekundy
    void Burst(GameObject g1, GameObject g2, Vector3 pos, float mass)
    {
        pool.ReturnBall(g1);
        pool.ReturnBall(g2);
        List<Rigidbody> gravities=new List<Rigidbody>();
        //tworzenie nowych kul, usuwanie kolizji i ich grawitacji na czas 0.5 sekundy
        for(int i=0;i<mass;i++)
        {
            gravities.Add(pool.GetBall(pos,1f).GetComponent<Rigidbody>());
            gravities[i].detectCollisions=false;
            gravities[i].velocity=new Vector3(Random.Range(-30f,30f),Random.Range(-30f,30f),Random.Range(-30f,30f));
        }
        StartCoroutine(SetRigidboty(gravities.ToArray()));
    }

    //po 0.5 sekundy przywracana jest kolizja i grawitacja
    IEnumerator SetRigidboty(Rigidbody[] gravities)
    {
        yield return new WaitForSeconds(0.5f);
        foreach(Rigidbody rb in gravities) rb.detectCollisions=true;
    }
}

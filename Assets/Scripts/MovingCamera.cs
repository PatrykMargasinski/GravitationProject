using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//klasa odpowiedzialna za ruch kamery
public class MovingCamera : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Vector3 target;
    [SerializeField] private float distanceToTarget = 10;

    private Vector3 previousPosition;
    private void Start()
    {
        target=new Vector3(0,0,0); //kamera zawsze patrzy na punkt 0,0,0
    }

    private void FixedUpdate()
    {
        //na podstawie momentu dokładnie przed i po wciśnięciu prawego przycisku myszy określany jest ruch kamery
        if (Input.GetMouseButtonDown(1))
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(1))
        {
            Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            Vector3 direction = previousPosition - newPosition;

            float rotationAroundYAxis = -direction.x * 180;
            float rotationAroundXAxis = direction.y * 180;
            cam.transform.position = target;
            var angles=cam.transform.rotation.eulerAngles;
            cam.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
            cam.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World);
            
            cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));

            previousPosition = newPosition;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public Text text;
    void Update() {
        text.text="Number of spheres:"+BallPool.ballNumber;
    }
}

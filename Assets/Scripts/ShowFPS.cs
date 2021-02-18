 using UnityEngine;
 using System.Collections;
 using UnityEngine.UI;
 
 public class ShowFPS : MonoBehaviour 
 {
     private Text fpsText;
     private float deltaTime;

     void Start()
     {
         fpsText=GetComponent<Text>();
     }
 
     void Update () 
     {
         deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
         float fps = 1.0f / deltaTime;
         fpsText.text = "FPS: "+Mathf.Ceil (fps).ToString();
     }
 }
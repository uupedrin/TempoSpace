using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour
{
    void Start(){
        transform.position = new Vector3(transform.position.x,transform.position.y,120);
    }

    void Update()
    {
        Movement();
    }

    void Movement(){
        if(GameController.controller.isPaused) return;

        transform.position += Vector3.back * Time.deltaTime * GameController.controller.sceneSpeed / 1.5f;
    }
}

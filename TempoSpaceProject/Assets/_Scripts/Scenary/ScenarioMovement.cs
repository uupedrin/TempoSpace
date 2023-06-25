using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioMovement : MonoBehaviour
{   
    public bool halfSpeed = false;
    public bool destroyMe = true;

    void Update()
    {
        if(GameController.controller.isPaused) return;

        float speed = halfSpeed ? GameController.controller.sceneSpeed / 2 : GameController.controller.sceneSpeed;

        transform.position += Vector3.back * Time.deltaTime * speed;

        if (transform.position.z <= -500 && destroyMe)
        {
            Destroy(gameObject);
        }
    }
}

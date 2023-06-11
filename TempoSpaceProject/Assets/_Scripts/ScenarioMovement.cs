using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioMovement : MonoBehaviour
{   
    void Update()
    {
        if(GameController.controller.isPaused) return;

        transform.position += Vector3.back * Time.deltaTime * GameController.controller.sceneSpeed;

        if (transform.position.z <= -500)
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            Destroy(this.gameObject);
            GameController.controller.AddHealth();
        }
    }
}

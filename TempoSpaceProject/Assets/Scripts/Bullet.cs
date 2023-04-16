using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Enemy" && this.tag != "EBullet"){
            GameController.controller.AddScore();
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        if(other.tag == "Player" && this.tag != "PBullet"){
            GameController.controller.ReduceHealth();
            Destroy(this.gameObject);
        }
    }
}

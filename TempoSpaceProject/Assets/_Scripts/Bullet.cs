using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Enemy") && this.tag != "EBullet"){
            other.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
        if(other.tag == "Player" && this.tag != "PBullet"){
            GameController.controller.ReduceHealth();
            Destroy(this.gameObject);
        }
    }
}

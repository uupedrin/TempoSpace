using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float bullet_speed, bullet_remove_time = 2;
    public GameObject bullet;

    private void Start() {
        InvokeRepeating("EnemyShoot",1f, 1.5f);
    }
    private void EnemyShoot(){
        GameObject temp_bullet = Instantiate(bullet, transform.position, transform.rotation);
        Rigidbody temp_rb = temp_bullet.GetComponent<Rigidbody>();
        temp_rb.AddForce(transform.forward * bullet_speed * -1, ForceMode.VelocityChange);
        Destroy(temp_bullet,bullet_remove_time);
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            Destroy(this.gameObject);
            GameController.controller.ReduceHealth();
        }
    }
}

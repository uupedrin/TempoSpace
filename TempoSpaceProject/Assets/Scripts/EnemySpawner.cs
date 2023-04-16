using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy, powerup;
    public float move_speed;

    private void Start() {
        InvokeRepeating("SpanwEnemy", 1f, 2f);
    }

    private void SpanwEnemy(){
        float posx = Random.Range(-85,85);
        float posz = Random.Range(65, 70);

        float which_spawn = Random.Range(0,100);

        if(which_spawn >= 90){//spawn powerup
            GameObject temp_powerup = Instantiate(powerup, transform.position, transform.rotation);
            temp_powerup.transform.position = new Vector3(posx, 5, posz);
            Rigidbody temp_rb = temp_powerup.GetComponent<Rigidbody>();
            temp_rb.AddForce(transform.forward * move_speed * -1, ForceMode.VelocityChange);
        }else{
            GameObject temp_enemy = Instantiate(enemy, transform.position, transform.rotation);
            temp_enemy.transform.position = new Vector3(posx, 5, posz);
            Rigidbody temp_rb = temp_enemy.GetComponent<Rigidbody>();
            temp_rb.AddForce(transform.forward * move_speed * -1, ForceMode.VelocityChange);
        } 
    }
}

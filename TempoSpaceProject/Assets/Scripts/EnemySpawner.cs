using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemy; 
    public float move_speed;

    private void Start() {
        InvokeRepeating("SpanwEnemy", 1f, 2f);
    }

    private void SpanwEnemy(){
        float posx = Random.Range(-50,50);
        float posz = Random.Range(65, 70);
        int Randomizer = Random.Range(0, GameController.controller.PowerUps.Length);
        int EnemyRand = Random.Range(0, enemy.Length);

        float which_spawn = Random.Range(0,100);

        if(which_spawn >= 90){//spawn powerup
            GameObject temp_powerup = Instantiate(GameController.controller.PowerUps[Randomizer], transform.position, transform.rotation);
            temp_powerup.transform.position = new Vector3(posx, 5, posz);
            Rigidbody temp_rb = temp_powerup.GetComponent<Rigidbody>();
            temp_rb.AddForce(transform.forward * move_speed * -1, ForceMode.VelocityChange);
        }else{
            
            //Pegar o movement do Inimigo e inserir ele aqui
            GameObject temp_enemy = Instantiate(enemy[EnemyRand], transform.position, transform.rotation);
            temp_enemy.transform.position = new Vector3(posx, 5, posz);
            Rigidbody temp_rb = temp_enemy.GetComponent<Rigidbody>();
            temp_rb.AddForce(transform.forward * move_speed * -1, ForceMode.VelocityChange);
        } 
    }
}

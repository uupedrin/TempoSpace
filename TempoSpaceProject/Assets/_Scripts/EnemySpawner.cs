using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemy; 
    public float move_speed;
    public GameObject mothership;
    [SerializeField] float spawnRate;


    public Player playerRef;

    private void Start() {
        InvokeRepeating("SpanwEnemy", 1f, spawnRate);
        Invoke("EndLevel",60);
    }

    private void SpanwEnemy(){
        float posx = Random.Range(-26,26);
        float posz = Random.Range(65, 70);
        //int Randomizer = Random.Range(0, GameController.controller.PowerUps.Length);
        int EnemyRand = Random.Range(0, enemy.Length);

        float which_spawn = Random.Range(0,100);

        GameObject temp_enemy = Instantiate(enemy[EnemyRand], transform.position, transform.rotation);
        temp_enemy.transform.position = new Vector3(posx, 5, posz);
        Rigidbody temp_rb = temp_enemy.GetComponent<Rigidbody>();
        temp_rb.AddForce(transform.forward * move_speed * -1, ForceMode.VelocityChange);
    }

    public void EndLevel(){
        Collider[] playerColliders = playerRef.GetComponents<Collider>();
        Player playerScript = playerRef.GetComponent<Player>();
        for (int i = 0; i < playerColliders.Length; i++)
        {
            playerColliders[i].enabled = false;
        }
        playerScript.enabled = false;
        CallMothership();
    }

    public void CallMothership(){
        GameObject temp_ship = Instantiate(mothership, new Vector3(0,-5,70), Quaternion.Euler(0,180,0));
        
        Rigidbody temp_rb = temp_ship.GetComponent<Rigidbody>();
        temp_rb.AddForce(Vector3.back * 30, ForceMode.VelocityChange);

        Invoke("EndGame",5);
    }

    void EndGame(){
        GameController.controller.Win();
    }

}

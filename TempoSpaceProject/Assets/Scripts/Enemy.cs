using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float bullet_speed, bullet_remove_time = 2;
    public GameObject bullet, Target;
    private float powerupchance = 10,powerupspeed = 1, speed = 1; // Colocar o número da velocidade mais apropriado em ambos
    public Transform[] WayPoints; // Para fazer o inimigo ir de um lado para o outro.
    private float worldtime, time; // Para que os inimigos não fiquem spawnando um encima do outro
    public int EnemyType; // Colocar o tipo de comportamento que o inimigo terá dentro da unity, basta trocar os números já que estou utilizando Switch Case.

    private void Start() {
        InvokeRepeating("EnemyShoot",1f, 1.5f);
    }
    private void EnemyShoot(){
        GameObject temp_bullet = Instantiate(bullet, transform.position, transform.rotation);
        Rigidbody temp_rb = temp_bullet.GetComponent<Rigidbody>();
        temp_rb.AddForce(transform.forward * bullet_speed * -1, ForceMode.VelocityChange);
        Destroy(temp_bullet,bullet_remove_time);
    }

    public void EnemyMove()
    {
        switch (EnemyType)
        {
            default:
                break;
            case 1: // Por algum motivo o inimigo da garra não quer mais sair do spawn. Corrigir depois.
                worldtime += Time.deltaTime;
                time = Mathf.PingPong(worldtime * speed, 1);
                transform.position = Vector3.Lerp(WayPoints[0].position, WayPoints[1].position, time);
                break;

            case 2: //Inimigo não morre quando é atingido pela bala, provavelmente o objeto está sendo spawnado abaixo da box collider da Bullet, o que faz a bala errar.
                transform.position += (transform.forward * speed * Time.deltaTime);
                Destroy(gameObject, 3.5f);
                break;

            case 3: // Inimigo não fica olhando para o player (arrumar).
                Target = GameObject.FindWithTag("Player");
                transform.position += (transform.forward * speed * Time.deltaTime); // Comportamento provisório para o inimigo dos raios
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                GameController.controller.AddScore();
                GameController.controller.ReduceHealth();
                Destroy(this.gameObject);
                break;

            case "PBullet":
                GameController.controller.AddScore();
                Destroy(this.gameObject);
                PowerUpSpawn();
                break;
            
            default:
                break;
        }
    }

    private void PowerUpSpawn()
    {
        int chance = Random.Range(1, 100);
        if (chance <= powerupchance)
        {
            int options = Random.Range(0, 3); // Aqui você coloca o número de power ups que tem!
            Instantiate(GameController.controller.PowerUps[options], transform.position, Quaternion.identity); // O "Options" será o número do PowerUp no Array do GameController
            GameController.controller.PowerUps[options].transform.position += (transform.forward * powerupspeed * Time.deltaTime);
        }
    }
}

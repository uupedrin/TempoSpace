using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float bulletSpeed, bulletRemoveTime = 2;
    [SerializeField] int health = 3;
    public GameObject bullet, Target;

    DamageFlash damageFlash;

    private float powerupchance = 10,powerupspeed = 1, speed = 1; // Colocar o n�mero da velocidade mais apropriado em ambos
    public Transform[] WayPoints; // Para fazer o inimigo ir de um lado para o outro.
    private float worldtime, time; // Para que os inimigos n�o fiquem spawnando um encima do outro
    public int EnemyType; // Colocar o tipo de comportamento que o inimigo ter� dentro da unity, basta trocar os n�meros j� que estou utilizando Switch Case.

    private void Start() {
        InvokeRepeating("EnemyShoot",1f, 1.5f);
        damageFlash = GetComponent<DamageFlash>();
    }
    private void EnemyShoot(){
        GameObject temp_bullet = Instantiate(bullet, transform.position, transform.rotation);
        Rigidbody temp_rb = temp_bullet.GetComponent<Rigidbody>();
        temp_rb.AddForce(transform.forward * bulletSpeed * -1, ForceMode.VelocityChange);
        Destroy(temp_bullet,bulletRemoveTime);
    }

    public void EnemyMove()
    {
        /* switch (EnemyType)
        {
            default:
                break;
            case 1: // Por algum motivo o inimigo da garra n�o quer mais sair do spawn. Corrigir depois.
                worldtime += Time.deltaTime;
                time = Mathf.PingPong(worldtime * speed, 1);
                transform.position = Vector3.Lerp(WayPoints[0].position, WayPoints[1].position, time);
                break;

            case 2: //Inimigo n�o morre quando � atingido pela bala, provavelmente o objeto est� sendo spawnado abaixo da box collider da Bullet, o que faz a bala errar.
                transform.position += (transform.forward * speed * Time.deltaTime);
                Destroy(gameObject, 3.5f);
                break;

            case 3: // Inimigo n�o fica olhando para o player (arrumar).
                Target = GameObject.FindWithTag("Player");
                transform.position += (transform.forward * speed * Time.deltaTime); // Comportamento provis�rio para o inimigo dos raios
                break;
        } */
    }

    private void OnTriggerEnter(Collider other)
    {
        /* switch (other.tag)
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
        } */
    }

    private void PowerUpSpawn()
    {
        int chance = Random.Range(1, 100);
        if (chance <= powerupchance)
        {
            int options = Random.Range(0, 3); // Aqui voc� coloca o n�mero de power ups que tem!
            Instantiate(GameController.controller.PowerUps[options], transform.position, Quaternion.identity); // O "Options" ser� o n�mero do PowerUp no Array do GameController
            GameController.controller.PowerUps[options].transform.position += (transform.forward * powerupspeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int amountOfDamage){
        damageFlash.UseDoDamageFlash();
        health -= amountOfDamage;
        if(health <= 0){
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        
    }
}
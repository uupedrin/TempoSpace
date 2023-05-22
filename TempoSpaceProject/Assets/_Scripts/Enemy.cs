using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float bulletSpeed, bulletRemoveTime = 2;
    [SerializeField] int health = 3;
    [SerializeField] float fireRate = 1.5f;
    [SerializeField] Transform[] guns;
    public GameObject bullet, target;
    DamageFlash damageFlash;

    [SerializeField] float powerupchance = 25;
    [SerializeField] float powerupSpeed = 30;
    [SerializeField] float speed = 1;
    private float worldtime, time; // Para que os inimigos n�o fiquem spawnando um encima do outro
    public int EnemyType; // Colocar o tipo de comportamento que o inimigo ter� dentro da unity, basta trocar os n�meros j� que estou utilizando Switch Case.

    private void Start() {
        InvokeRepeating("EnemyShoot",1f, fireRate);
        damageFlash = GetComponent<DamageFlash>();
    }
    private void EnemyShoot(){
        for (int i = 0; i < guns.Length; i++)
        {
            GameObject temp_bullet = Instantiate(bullet, guns[i].transform.position, Quaternion.Euler(0,180,0));
            temp_bullet.tag = "EBullet";
            Rigidbody temp_rb = temp_bullet.GetComponent<Rigidbody>();
            temp_rb.AddForce(transform.forward * bulletSpeed * -1, ForceMode.VelocityChange);
            Destroy(temp_bullet,bulletRemoveTime);
        }
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
                target = GameObject.FindWithTag("Player");
                transform.position += (transform.forward * speed * Time.deltaTime); // Comportamento provis�rio para o inimigo dos raios
                break;
        } */
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                other.GetComponent<Player>().TakeDamage();
                Destroy(this.gameObject);
                break;
            case "PBullet":
                TakeDamage();
                Destroy(other.gameObject);
            break;
        }
    }

    private void PowerUpSpawn()
    {
        int chance = Random.Range(0, 100);
        if (chance <= powerupchance)
        {
            GameObject temp_pwp = Instantiate(GameController.controller.PowerUpObject, transform.position, Quaternion.Euler(-90,0,0));
            Rigidbody temp_rb = temp_pwp.GetComponent<Rigidbody>();
            temp_rb.AddForce(Vector3.back * powerupSpeed , ForceMode.VelocityChange);
        }
    }

    public void TakeDamage(int amountOfDamage = 1){
        damageFlash.UseDoDamageFlash();
        health -= amountOfDamage;
        if(health <= 0){
            PowerUpSpawn();
            GameController.controller.AddScore();
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        
    }
}

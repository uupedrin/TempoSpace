using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Stats")]
    [SerializeField] int health = 3;
    [SerializeField] float fireRate = 1.5f;
    public bool canBeKilled = false, hasSpawned = false, isDead = false;
    

    [Header("Utils")]

    [SerializeField] float powerupDropChance = 25;
    [SerializeField] float activationZ;
    [SerializeField] GameObject token;


    [Header("Effects")]
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject smoke;
    [SerializeField] Transform smokeStartTransform;
    DamageFlash damageFlash;


    [Header("Movement")]
    [SerializeField] float moveSpeed = 1;



    [Header("Shooting")]
    [SerializeField] Transform[] guns;
    [SerializeField] GameObject bullet;




    private void Start() {
        damageFlash = GetComponent<DamageFlash>();
    }

    private void Update(){
        CheckPosition();
        Movement();
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
                Bullet pBullet = other.GetComponent<Bullet>();
                if(pBullet.projectileType == 1){
                    TakeDamage();
                    Destroy(other.gameObject);
                }
            break;
        }
    }
    
    private void OnEnable()
    {
        hasSpawned = true;
    }


    
    private void EnemyShoot(){
        if(bullet == null) return;
        for (int i = 0; i < guns.Length; i++)
        {
            Instantiate(bullet, guns[i].transform.position, Quaternion.Euler(0,180,0));
        }
    }

    void CheckPosition(){
        if(canBeKilled) return;

        if(transform.position.z <= activationZ){
            canBeKilled = true;
            
            //Start Enemy Methods
            InvokeRepeating("EnemyShoot",Random.Range(0, 1.5f), fireRate);
        }
    }

    void Movement(){
        if(GameController.controller.isPaused) return;
        if(!hasSpawned) return;

        if(isDead){
            transform.Rotate(Vector3.forward * 30 * Time.deltaTime);
        }else{
            transform.position += Vector3.back * Time.deltaTime * moveSpeed;
        }
    }
    
    private void PowerUpSpawn()
    {
        int chance = Random.Range(0, 100);
        if (chance <= powerupDropChance)
        {
            Instantiate(GameController.controller.PowerUpObject, transform.position, Quaternion.identity);
        }
    }

    public void TakeDamage(int amountOfDamage = 1){
        if(!canBeKilled) return;

        damageFlash.UseDoDamageFlash();
        health -= amountOfDamage;
        if(health <= 0){
            //PowerUpSpawn();
            GameController.controller.AddScore();
            DeathAnimation();
        }
    }

    void Die()
    {   
        GameController.controller.enemiesInScene.Remove(this);
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void DeathAnimation(){
        PowerUpSpawn();
        CheckTokenAndSpawn();
        int deathAnimation = Random.Range(0,100);
        deathAnimation = deathAnimation < 75 ? 0 : 1;
        switch (deathAnimation)
        {               
            case 0:
                Die();
                break;
            case 1: //Falling
                CancelInvoke();
                FallAnimation();
                Invoke("Die", 4f);
                break;
        }
    }

    void FallAnimation(){
        DisableThrusters();
        DisableCollisions();

        GameObject smokeInstance = Instantiate(smoke,smokeStartTransform.position, Quaternion.Euler(Vector3.left * 15f));

        smokeInstance.transform.parent = smokeStartTransform;

        transform.Rotate(Vector3.right * 30f);

        isDead = true;

        GetComponent<Rigidbody>().useGravity = true;
    }

    void DisableThrusters(){
        ParticleSystem[] thrusters;
        thrusters = GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < thrusters.Length; i++)
        {
            thrusters[i].Stop();
        }
    }

    void DisableCollisions(){
        Collider[] collisions = GetComponentsInChildren<Collider>();
        for (int i = 0; i < collisions.Length; i++)
        {
            collisions[i].enabled = false;
        }
    }

    void CheckTokenAndSpawn(){
        GameController.controller.EnemyKillIncrease();
        bool spawnToken = GameController.controller.CheckTokens();
        if(spawnToken){
            //Instantiate Token
            Instantiate(token, transform.position, Quaternion.identity);
        }
    }
}

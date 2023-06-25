using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpyBlinder : MonoBehaviour
{
    //Stats
    public float xSpeed = 5f;
    public int health = 8;

    private bool canAttack = true, playerRange = false, canBeKilled = false, hasSpawned = true;

    public GameObject explosionEffect;
    RigDamageFlash damageFlash;

    //Animation
    private Animation selfAnimator;
    public float zOffset = 30f, xOffset = .5f;
    private AudioSource source;
    public float flashDelay = .45f;

        //ANIMATIONS
        string anim0 = "SpyEnemy|SpyEnemyIdle";
        string anim1 = "SpyEnemy|SpyEnemyAttack";


    //Movement
    public Transform player;

    void AttackPlayer(){
        selfAnimator.Play(anim1);
        selfAnimator.PlayQueued(anim0);
        Invoke("HarmPlayer", flashDelay);
    }

    void HarmPlayer(){ //Function to be invoked during animation
        source.Play();
        Invoke("BlindPop",.4f);
    }

    void BlindPop(){
        GameController.controller.ui_controller.BlindPlayer();
    }


    void OnEnable(){
        hasSpawned = true;
        canBeKilled = true;

        InvokeRepeating("AttackPlayer", 8f,8f);
    }

    void Movement(){
        if(GameController.controller.isPaused) return;
        if(!hasSpawned) return;

        Vector3 playerPos = Vector3.right * player.position.x;
        Vector3 desiredPosition = playerPos + (Vector3.forward * zOffset) + (Vector3.up * 8.5f);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, xSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

    }

    public void TakeDamage(int damage = 1){
        if(!canBeKilled) return;

        health-=damage;
        damageFlash.UseDoDamageFlash();
        if(health <= 0){
            Die();
        }
    }

    void Die(){
        GameController.controller.AddScore(800);
        Instantiate(explosionEffect,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }





    void Start()
    {
        damageFlash = GetComponent<RigDamageFlash>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        selfAnimator = GetComponentInChildren<Animation>();
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        Movement();
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag){
            case "PBullet":
                Bullet pBullet = other.GetComponent<Bullet>();
                if(pBullet.projectileType == 1){
                    TakeDamage();
                    Destroy(other.gameObject);
                }
                break;
        }
    }
}

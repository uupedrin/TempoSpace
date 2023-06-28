using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HawkBoss : MonoBehaviour
{
    int currentWeapon = 0; //0 - Shotgun | 1 - RayGun
    int currentAction = 0; //0 - Movement | 1 - ChangeWeapon | 2 - RayCharge | 3 - Fire | 4 - Reload | 5 - SwordCharge | 6 - SwordAttack


    int shotgunBullets = 2;

    GameObject[] weapons;
    AudioSource[] sources;

    RigDamageFlash damageFlash;

    public GameObject[] bulletsPrefabs;

    public Transform bulletSpawnLocation;

    int health = 120; // < 20 - Stange 2

    void GetReferences(){
        weapons = GameObject.FindGameObjectsWithTag("Gun");
        sources = GetComponents<AudioSource>();
        damageFlash = GetComponent<RigDamageFlash>();
    }

    void DisableComponents(){
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }
    }

    public Animation[] animators;

    public float xSpeed = 4;

    //ANIMATIONS
        string idleAnim = "HawkShip|HawkShipIdle";
        string changeWPAnim = "HawkShip|HawkShipChangeWeapon";
        string rayChargeAnim = "HawkShip|HawkShipRayGunTremble";
        string fireAnim = "HawkShip|HawkShipShotgunFire";
        string shotgunReloadAnim = "HawkShip|HawkShipShotgunReload";

            string shotgunAnim = "Shotgun|ShotgunObjectReload";
            string rayGunAnim = "Blaster|RayGunFire";

    void Start()
    {
        GetReferences();
        DisableComponents();
        weapons[0].SetActive(true);
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        InvokeRepeating("CallWeaponChange",15f, 15f);
        InvokeRepeating("CallWeaponFire",7f, 7f);
    }

    void CallWeaponFire(){
        StartCoroutine(Fire());
    }

    void CallWeaponChange(){
        StartCoroutine(ChangeWeapon(currentWeapon == 0 ? 1 : 0));
    }

    void Update()
    {
        Movement();
    }


    public Transform playerTransform;

    void Movement(){
        if(GameController.controller.isPaused) return;
        if(currentAction != 0) return;

        Vector3 playerPos = Vector3.right * playerTransform.position.x;
        Vector3 desiredPosition = playerPos + (Vector3.forward * 10) + (Vector3.down * 7.5f);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, xSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }

    

    IEnumerator Reload(){
        currentAction = 4;
        StopCoroutine("Fire");
        animators[0].Play(shotgunReloadAnim);
        yield return new WaitForSeconds(.5f);
        sources[1].Play();
        animators[1].Play(shotgunAnim);
        shotgunBullets = 2;
        animators[0].PlayQueued(idleAnim);
        StartCoroutine(ReturnMoveAction());
    }

    IEnumerator Fire(){
        currentAction = 3;
        switch(currentWeapon){
            case 0:
                if(shotgunBullets <= 0){
                    StartCoroutine(Reload());
                }else{
                    shotgunBullets--;
                    yield return new WaitForSeconds(.5f);
                    sources[0].Play();
                    animators[0].Play(fireAnim);
                    animators[0].PlayQueued(idleAnim);
                    Instantiate(bulletsPrefabs[0], bulletSpawnLocation.position, Quaternion.Euler(0,180,0));
                    StartCoroutine(ReturnMoveAction());
                }
                break;
            case 1:
                currentAction = 2;
                animators[0].Play(rayChargeAnim);
                animators[2].Play(rayGunAnim);
                yield return new WaitForSeconds(1f);
                sources[2].Play();
                animators[0].Play(fireAnim);
                animators[0].PlayQueued(idleAnim);
                currentAction = 3;
                Instantiate(bulletsPrefabs[1], bulletSpawnLocation.position, Quaternion.Euler(0,180,0));
                StartCoroutine(ReturnMoveAction());
                break;
        }
        
    }

    IEnumerator ChangeWeapon(int newWeapon){
        currentAction = 1;
        animators[0].Play(changeWPAnim);
        yield return new WaitForSeconds(.2f);
        weapons[currentWeapon].SetActive(false);
        yield return new WaitForSeconds(.5f);
        weapons[newWeapon].SetActive(true);
        animators[0].PlayQueued(idleAnim);
        currentWeapon = newWeapon;
        StartCoroutine(ReturnMoveAction());
    }

    IEnumerator ReturnMoveAction(){
        yield return new WaitForSeconds(1.5f);
        currentAction = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        switch(other.tag){
            case "PBullet":
                TakeDamage();
                break;
        }
    }

    public void TakeDamage(int damage = 1){
        health -= damage;
        damageFlash.UseDoDamageFlash();
        if(health <= 0){
            Die();
        }
    }

    void Die(){
        GameController.controller.ui_controller.SceneChange("VictoryScene");
    }

}

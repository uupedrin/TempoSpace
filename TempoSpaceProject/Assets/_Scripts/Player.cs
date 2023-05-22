using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player player;
    float v,h;
    [Header("Movement")]
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float maximumRotation;


    [Header("Shooting")]
    [SerializeField] float bulletSpeed;
    public float fireRate;
    float OGFireRate;
    [SerializeField] float bulletRemoveTime;
    [SerializeField] float aimOffset;
    [SerializeField] Transform[] guns;
    public int currentWeaponUpgrade = 1;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform aim;
    bool canFire = true;
    DamageFlash damageFlash;

    public enum Powerups
    {
        NONE, SHIELD, FIRERATE, HEALTH 
    }

    [Header("Powerups")]
    [SerializeField] Shield shieldObject;
    public Powerups currentPWP;



    void Start(){
        damageFlash = GetComponent<DamageFlash>();
        shieldObject = GetComponentInChildren<Shield>();
        OGFireRate = fireRate;
        SetPowerup(1);
    }
    void Update()
    {
        MovePlayer();
        PlayerShoot();
        if(Input.GetKeyDown(KeyCode.Escape)){
            GameController.controller.ui_controller.QuitGame();
        }
        AimMovement();
    }

    void MovePlayer()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        Vector3 h_movement = Vector3.zero, v_movement = Vector3.zero;

        if ((h > 0 && transform.position.x < 27) || (h < 0 && transform.position.x > -27)) {
            h_movement = Vector3.right * h;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Vector3.back * maximumRotation * h), rotationSpeed * Time.deltaTime);

        }
        else
        {
            h_movement = Vector3.zero;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Vector3.zero), rotationSpeed * Time.deltaTime);

        }

        if ((v > 0 && transform.position.z < -27) || (v < 0 && transform.position.z > -45))
        {
            v_movement = Vector3.forward * v;
        }
        else
        {
            v_movement = Vector3.zero;
        }
        Vector3 movement = h_movement + v_movement;
        transform.position += movement.normalized * Time.deltaTime * moveSpeed;
    }

    void PlayerShoot(){
        if (Input.GetButton("Fire1") && canFire)
        {
            StartCoroutine("_Fire");
        }
    }

    IEnumerator _Fire(){
        canFire = false;
        switch (currentWeaponUpgrade)
        {
            case 1:
                GameObject temp_bullet = Instantiate(bullet, guns[0].position, transform.rotation);
                Rigidbody temp_rb = temp_bullet.GetComponent<Rigidbody>();
                temp_rb.AddForce(transform.forward * bulletSpeed, ForceMode.VelocityChange);
                Destroy(temp_bullet, bulletRemoveTime);
            break;

        }
        
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }

    void AimMovement(){
        Vector3 playerPos = Vector3.right * transform.position.x;
        aim.position = new Vector3(transform.position.x, transform.position.y, aimOffset);
    }

    public void TakeDamage(){
        damageFlash.UseDoDamageFlash();
        GameController.controller.ReduceHealth();
    }

    public void SetPowerup(int powerupIndex){
        switch ((Powerups)powerupIndex)
        {
            case Powerups.SHIELD:
                shieldObject.EnableShield();
                RemovePowerup();
            break;
            case Powerups.FIRERATE:
                fireRate /= 2;
                Invoke("RevertFireRate", Time.time * 2);
            break;
            case Powerups.HEALTH:

                RemovePowerup();
            break;
        }
    }

    public void RevertFireRate(){
        fireRate = OGFireRate;
        RemovePowerup();
    }

    public void RemovePowerup(){
        currentPWP = Powerups.NONE;
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "EBullet":
                Destroy(other.gameObject);
                TakeDamage();
            break;
        }
    }
}

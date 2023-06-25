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
    public float fireRate = 0.3f;
    float OGFireRate;
    float OGMoveSpeed;
    [SerializeField] float bulletRemoveTime;
    [SerializeField] float aimOffset;
    [SerializeField] Transform[] guns;
    [SerializeField] GameObject[] bullets;
    [SerializeField] Transform aim;
    bool canFire = true;
    bool isEnabled = false;
    DamageFlash damageFlash;

    public enum Powerups
    {
        NONE, SHIELD, FIRERATE, HEALTH 
    }

    [Header("Powerups")]
    [SerializeField] Shield shieldObject;
    public Powerups currentPWP;

    public bool haveBomb = false;


    void Start(){
        damageFlash = GetComponent<DamageFlash>();
        shieldObject = GetComponentInChildren<Shield>();
        OGFireRate = fireRate;
        OGMoveSpeed = moveSpeed;
    }

    void Update()
    {
        MovePlayer();
        PlayerShoot();
        AimMovement();
        UpgradeVariables();
        BombPWPHandle();
    }

    void UpgradeVariables(){
        fireRate = OGFireRate - (0.025f * GameController.controller.upgradesFireRate);
        moveSpeed = OGMoveSpeed + (0.025f * GameController.controller.upgradesSpeed);
    }

    void MovePlayer()
    {
        if(GameController.controller.isPaused) return;
        if(!isEnabled) return;


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
        if(GameController.controller.isPaused) return;
        if(!isEnabled) return;

        if (Input.GetButton("Fire1") && canFire)
        {
            StartCoroutine("_Fire");
        }
    }

    IEnumerator _Fire(){
        canFire = false;

        switch (GameController.controller.upgradesWeapons)
        {
            case 0:
                Instantiate(bullets[0], guns[0].position, Quaternion.identity);
                break;
            case 1:
                for (int i = 1; i <= 2; i++)
                {
                    Instantiate(bullets[0], guns[i].position, Quaternion.identity);
                }
                break;
            case 2:
                for (int i = 0; i <= 2; i++)
                {
                    Instantiate(bullets[0], guns[i].position, Quaternion.identity);
                }
                break;
            case 3:
                for (int i = 0; i <= 2; i++)
                {
                    int idx = i == 0 ? 1 : 0;
                    Instantiate(bullets[idx], guns[i].position, Quaternion.identity);
                }
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
        if(!isEnabled) return;

        damageFlash.UseDoDamageFlash();
        GameController.controller.ReduceHealth();
    }
    
    public void SetPowerup(int powerupIndex){
        switch ((Powerups)powerupIndex)
        {
            case Powerups.SHIELD:
                GameController.controller.ui_controller.SetPowerupText("SHIELD ACQUIRED");
                shieldObject.EnableShield();
                RemovePowerup();
            break;
            case Powerups.FIRERATE:
                GameController.controller.ui_controller.SetPowerupText("FIRERATE INCREASED");
                fireRate /= 2;
                Invoke("RevertFireRate", Time.time * 2);
            break;
            case Powerups.HEALTH:
                GameController.controller.AddHealth();
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
            case "PWPShield":
                shieldObject.EnableShield();
                GameController.controller.ui_controller.SetPowerupText("SHIELD AQUIRED!");
                Destroy(other.gameObject);
            break;
            case "PWPBomb":
                EnableBombPWP();
                Destroy(other.gameObject);
                break;
            case "Token":
                GameController.controller.EarnToken();
                Destroy(other.gameObject);
                break;
            case "Smoke":
                TakeDamage();
                other.GetComponent<Hatch>().HitTaken();
                break;
        }
    }
    private void BombPWPHandle(){
        if (Input.GetButtonDown("Fire2") && haveBomb)
        {
            DisableBombPWP();
            for (int i = 0; i < GameController.controller.enemiesInScene.Count; i++)
            {
                if(GameController.controller.enemiesInScene[i].canBeKilled){
                    GameController.controller.enemiesInScene[i].TakeDamage(10);
                }
            }
        }
        
    }

    private void EnableBombPWP(){
        GameController.controller.ui_controller.SetBombBarValue(100);
        GameController.controller.ui_controller.SetPowerupText("BOMB AQUIRED!\nPress K or C to use");
        haveBomb = true;
    }
    private void DisableBombPWP(){
        GameController.controller.ui_controller.SetPowerupText("KABOOM!");
        GameController.controller.ui_controller.SetBombBarValue(0);
        haveBomb = false;
    }

    public void EnableSelf(){
        isEnabled = true;
    }
}

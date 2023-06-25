using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int damage = 1;
    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletDestroyTime = 5f;
    public int projectileType;
    
    public bool isInverted = false;

    Vector3 vectorToUse;

    Collider missileArea;

    void Start()
    {
        CheckRotation();
        vectorToUse = isInverted ? Vector3.back : Vector3.forward;
        if(projectileType == 2){
            missileArea = GetComponents<Collider>()[1];
            missileArea.enabled = false;
        }
        Destroy(gameObject, bulletDestroyTime);
    }

    void Update(){
        Movement();
    }

    void CheckRotation(){
        isInverted = transform.rotation.y != 1 ? false : true;
    }
    
    void Movement(){
        if(GameController.controller.isPaused) return;

        transform.position += vectorToUse * Time.deltaTime * bulletSpeed;
    }

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Enemy")){
            if(missileArea != null && projectileType == 2){
                missileArea.enabled = true;
                projectileType = 1;
                other.GetComponent<Enemy>().TakeDamage();
                Destroy(gameObject,.5f);
            }
        }
    }
}

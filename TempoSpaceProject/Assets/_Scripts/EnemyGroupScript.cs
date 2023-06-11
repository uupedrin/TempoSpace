using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupScript : MonoBehaviour
{
    [SerializeField] float activationZ;
    public Enemy[] enemyGroup;

    private void DeactivateChildren(){
        enemyGroup = GetComponentsInChildren<Enemy>();

        for(int i = 0; i < enemyGroup.Length; i++){
            enemyGroup[i].gameObject.SetActive(false);
        }
    }

    private void AddEnemiesToGameController(){
        for(int i = 0; i < enemyGroup.Length; i++){
            GameController.controller.enemiesInScene.Add(enemyGroup[i]);
        }
    }

    void Start()
    {
        DeactivateChildren();
        AddEnemiesToGameController();
    }

    void Update()
    {
        Movement();  
        ZChecker();
    }


    void Movement(){
        if(GameController.controller.isPaused)return;
        
        transform.position += Vector3.back*Time.deltaTime*GameController.controller.sceneSpeed;

    }
    
    void ZChecker(){
        if(transform.position.z <= 80){
            for(int i = 0; i < enemyGroup.Length; i++){
                enemyGroup[i].gameObject.SetActive(true);
                enemyGroup[i].transform.parent = null;
            }
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupScript : MonoBehaviour
{
    [SerializeField] float activationZ;
    [SerializeField] float moveSpeed;

    public Enemy[] enemyGroup;

    private void DeactivateChildren(){
        enemyGroup = GetComponentsInChildren<Enemy>();

        for(int i = 0; i < enemyGroup.Length; i++){
            enemyGroup[i].gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DeactivateChildren();
    }
    
    void Movement(){
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

    // Update is called once per frame
    void Update()
    {
     Movement();  

     ZChecker();
    }
}

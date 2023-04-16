using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    float v,h;
    public float bullet_speed, move_speed, bullet_remove_time;
    public GameObject bullet;

    void Update()
    {
        MovePlayer();
        if(Input.GetKeyDown(KeyCode.X)){
            PlayerShoot();
        }
    }

    void MovePlayer(){
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        Vector3 h_movement = Vector3.zero, v_movement = Vector3.zero;

        if((h>0 && transform.position.x < 91) || (h<0 && transform.position.x > -91)){
            h_movement = Vector3.right * h;
        }else {
            h_movement = Vector3.zero;
        }

        if((v>0 && transform.position.z < 45) || (v<0 && transform.position.z > -52)){
            v_movement = Vector3.forward * v;
        }else {
            v_movement = Vector3.zero;
        }
        Vector3 movement = h_movement + v_movement;
        transform.position += movement.normalized * Time.deltaTime * move_speed;
    }

    void PlayerShoot(){
        GameObject temp_bullet = Instantiate(bullet, transform.position, transform.rotation);
        Rigidbody temp_rb = temp_bullet.GetComponent<Rigidbody>();
        temp_rb.AddForce(transform.forward * bullet_speed, ForceMode.VelocityChange);
        Destroy(temp_bullet, bullet_remove_time);
    }
}

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
    }

    void MovePlayer(){
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        Vector3 h_movement = Vector3.right * h;
        Vector3 v_movement = Vector3.forward * v;
        Vector3 movement = h_movement + v_movement;
        transform.position += movement.normalized * Time.deltaTime * move_speed;
    }

    void PlayerShoot(){
        GameObject temp_bullet = Instantiate(bullet, transform.position, transform.rotation);
        Rigidbody temp_rb = temp_bullet.GetComponent<Rigidbody>();
        temp_rb.AddForce(transform.forward, ForceMode.VelocityChange);
        Destroy(temp_bullet, bullet_remove_time);
    }
}

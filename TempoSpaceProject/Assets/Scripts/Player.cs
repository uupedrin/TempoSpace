using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player player;
    float v,h;
    public float bullet_speed, move_speed, bullet_remove_time, aimOffset;
    public GameObject bullet;
    public Transform aim;
    public int firerate = 1; //Colocar o valor da variável corretamente posteriormente;

    void Update()
    {
        MovePlayer();
        if(Input.GetKeyDown(KeyCode.X)){
            PlayerShoot();
        }

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
        }
        else
        {
            h_movement = Vector3.zero;
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
        transform.position += movement.normalized * Time.deltaTime * move_speed;
    }

    void PlayerShoot(){
        GameObject temp_bullet = Instantiate(bullet, transform.position, transform.rotation);
        Rigidbody temp_rb = temp_bullet.GetComponent<Rigidbody>();
        temp_rb.AddForce(transform.forward * bullet_speed, ForceMode.VelocityChange);
        Destroy(temp_bullet, bullet_remove_time);
    }

    void AimMovement(){
        Vector3 playerPos = Vector3.right * transform.position.x;
        aim.position = new Vector3(transform.position.x, transform.position.y, aimOffset);
    }
}

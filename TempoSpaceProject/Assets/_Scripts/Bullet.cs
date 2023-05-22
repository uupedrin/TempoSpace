using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] int damage = 1;

    public void SetDamage(int newDamage){
        damage = newDamage;
    }

    public int GetDamage(){
        return damage;
    }
}

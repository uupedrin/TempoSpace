using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate3DObject : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 30;
    void Update()
    {
        transform.eulerAngles += Vector3.down * rotationSpeed * Time.deltaTime;
    }
}

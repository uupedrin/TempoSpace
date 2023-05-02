using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Vector3 cameraOffset;
    public float cameraSpeed;
    public Transform player;

    private void Update()
    {
        Vector3 playerPos = Vector3.right * player.position.x;
        Vector3 desiredPosition = playerPos + cameraOffset;
        if (desiredPosition.x >= -19 && desiredPosition.x <= 19)
        {
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, cameraSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}

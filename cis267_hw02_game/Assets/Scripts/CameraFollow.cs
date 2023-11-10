using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    public float movementSpeed;
    private Vector3 cameraPosition;

    // Update is called once per frame
    void LateUpdate()
    {
        cameraPosition = new Vector3(player.position.x, player.position.y, -10);
        transform.position = Vector3.Slerp(transform.position, cameraPosition, movementSpeed * Time.deltaTime);
    }
}

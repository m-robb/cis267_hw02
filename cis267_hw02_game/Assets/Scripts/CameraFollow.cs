using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public static CameraFollow Instance;

    public float movementSpeed;
    private Vector3 cameraPosition;
    private void Start()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
    }
    // Update is called once per frame
    void LateUpdate()
    {
        cameraPosition = new Vector3(player.position.x, player.position.y, -10);
        transform.position = Vector3.Slerp(transform.position, cameraPosition, movementSpeed * Time.deltaTime);
    }
}

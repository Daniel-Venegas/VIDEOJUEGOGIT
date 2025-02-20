using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float offsetZ= 5f;
    public float smoothing = 2f;

    //Player Transform component
    Transform playerPos;
    // Start is called before the first frame update
    void Start()
    {

        playerPos = FindObjectOfType<PlayerScript>().transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        Vector3 targetPosition = new Vector3(playerPos.position.x, transform.position.y, playerPos.position.z - offsetZ);


        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //used this to help with camera follow: https://gamedevsolutions.com/smooth-camera-follow-in-unity3d-c/
    [SerializeField] GameObject player;
    [SerializeField] Vector3 offset;

    float followSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        //sets the follow offset
        offset = transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
        //follow player by an offset
        Vector3 playerPos = player.transform.position + offset;
        //follow the player
        Vector3 following = Vector3.Lerp(transform.position, playerPos, followSpeed);

        //make camera follow the player
        transform.position = following;
        transform.LookAt(player.transform);
    }
}

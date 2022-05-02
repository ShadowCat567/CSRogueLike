using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //used this to help with camera follow: https://gamedevsolutions.com/smooth-camera-follow-in-unity3d-c/
    [SerializeField] GameObject player;
    [SerializeField] Vector3 offset;

    float followSpeed = 0.1f;
    bool isFollowing = true;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
       // if (isFollowing == true || player.GetComponent<PlayerMovement>().followPlayer)
        //{
            Vector3 playerPos = player.transform.position + offset;
            Vector3 following = Vector3.Lerp(transform.position, playerPos, followSpeed);

            transform.position = following;
            transform.LookAt(player.transform);
       // }

        if(player.GetComponent<PlayerMovement>().followPlayer)
        {
            isFollowing = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CamBarrier")
        {
            //Debug.Log("Stopped following");
            isFollowing = false;
            player.GetComponent<PlayerMovement>().followPlayer = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "CamBarrier")
        {
            isFollowing = true;
        }
    }
}

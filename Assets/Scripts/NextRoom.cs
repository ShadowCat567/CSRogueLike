using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRoom : MonoBehaviour
{
    [SerializeField] GameObject manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("RoomManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            manager.GetComponent<RoomManager>().SelectRoom();
        }
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log("collided");
        if (collision.gameObject.tag == "Player")
        {
            manager.GetComponent<RoomManager>().SelectRoom();
        }
    }
    */
}

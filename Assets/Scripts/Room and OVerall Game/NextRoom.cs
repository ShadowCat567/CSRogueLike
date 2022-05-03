using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRoom : MonoBehaviour
{
    [SerializeField] GameObject manager;
    [SerializeField] GameObject currentRoom;
    [SerializeField] GameObject player;
    [SerializeField] bool isStart;

    BoxCollider bc;

    AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        bc = GetComponent<BoxCollider>();
        bc.isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStart == false)
        {
            if (currentRoom.GetComponent<PositionsInRoom>().canChangeRooms)
            {
                bc.isTrigger = true;
            }
        }

        else
        {
            bc.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            sound.PlayOneShot(sound.clip);
            manager.GetComponent<RoomManager>().SelectRoom();
            currentRoom.SetActive(false);
        }
    }
}

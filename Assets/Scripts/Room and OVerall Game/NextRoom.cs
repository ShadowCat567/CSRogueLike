using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRoom : MonoBehaviour
{
    //variables related to the room
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
            //if the room is not the starting room, only change the collider to a trigger when canChangeRooms is true
            if (currentRoom.GetComponent<PositionsInRoom>().canChangeRooms)
            {
                bc.isTrigger = true;
            }
        }

        //if the room is Start, set the collider to a trigger
        else
        {
            bc.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if the player runs into the exit object, move to the next room
        if(other.gameObject.tag == "Player")
        {
            sound.PlayOneShot(sound.clip);
            manager.GetComponent<RoomManager>().SelectRoom();
            currentRoom.SetActive(false);
        }
    }
}

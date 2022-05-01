using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRoom : MonoBehaviour
{
    [SerializeField] GameObject manager;
    [SerializeField] GameObject currentRoom;
    [SerializeField] GameObject player;
    [SerializeField] bool isStart;

    // Start is called before the first frame update
    void Start()
    {

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
            currentRoom.SetActive(false);
        }
    }
}

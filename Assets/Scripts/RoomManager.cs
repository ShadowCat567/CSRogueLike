using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] GameObject[] roomArr;
    [SerializeField] GameObject curRoom;
    [SerializeField] GameObject finalRoom;
    [SerializeField] GameObject player;

    List<GameObject> usedRooms = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectRoom()
    {
        int nextRoom = Random.Range(0, roomArr.Length - 1);

        roomArr[nextRoom].SetActive(true);
        usedRooms.Add(curRoom);
        curRoom = roomArr[nextRoom];
    }
}

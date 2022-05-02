using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] GameObject[] roomArr;
    [SerializeField] GameObject curRoom;
    [SerializeField] GameObject finalRoom;
    [SerializeField] GameObject player;

    List<GameObject> unusedRooms = new List<GameObject>();
    List<GameObject> usedRooms = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        unusedRooms.Add(curRoom);

        foreach(GameObject room in roomArr)
        {
            unusedRooms.Add(room);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectRoom()
    {
        if (unusedRooms.Count == 0)
        {
            finalRoom.SetActive(true);
            curRoom = finalRoom;
        }

        else
        {
            int nextRoom = Random.Range(0, unusedRooms.Count - 1);

            unusedRooms[nextRoom].SetActive(true);
            usedRooms.Add(curRoom);
            curRoom = unusedRooms[nextRoom];
            unusedRooms.Remove(curRoom);
        }
    }
}

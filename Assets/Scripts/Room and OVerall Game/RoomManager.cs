using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    //variables related to the specific rooms and the player
    [SerializeField] GameObject[] roomArr;
    [SerializeField] GameObject curRoom;
    [SerializeField] GameObject finalRoom;
    [SerializeField] GameObject player;

    //creates a pair of lists to keep track of which rooms have been visited or not
    List<GameObject> unusedRooms = new List<GameObject>();
    List<GameObject> usedRooms = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject room in roomArr)
        {
            //add rooms to the unusedRooms list
            unusedRooms.Add(room);
        }
    }

    public void SelectRoom()
    {
        //if there are no more rooms, go to the final room
        if (unusedRooms.Count == 0)
        {
            finalRoom.SetActive(true);
            curRoom = finalRoom;
        }

        //if there are still rooms, choose a random one from the remaining list and set it active, deactivate current room and move it to the usedRooms List
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] GameObject[] roomArr;
    [SerializeField] GameObject curRoom;
    [SerializeField] GameObject finalRoom;

    List<GameObject> usedRooms = new List<GameObject>();

    private void Awake()
    {
        foreach(GameObject room in roomArr)
        {
            Instantiate(room);
            room.SetActive(false);
        }

        Instantiate(finalRoom);
        finalRoom.SetActive(false);
    }

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
        StartCoroutine(RoomWait());
    }

    IEnumerator RoomWait()
    {
        yield return new WaitForSeconds(0.3f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionsInRoom : MonoBehaviour
{
    [SerializeField] GameObject room;
    [SerializeField] GameObject playerPos;
    GameObject player;
    bool wasActivated = false;

    public int enemyCounter = 0;
    public int enemiesKilled;
    public bool canChangeRooms = false;

    [SerializeField] GameObject healthbuff;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(healthbuff.GetComponent<HealthBuffs>().collectedBuff)
        {
            canChangeRooms = true;
        }

        if(enemyCounter == enemiesKilled)
        {
            healthbuff.SetActive(true);
        }

        if(room.activeSelf == true && wasActivated == false)
        {
            ChangePosition();
        }
    }

    void ChangePosition()
    {
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = playerPos.transform.position;
        player.GetComponent<CharacterController>().enabled = true;
        wasActivated = true;
    }
}

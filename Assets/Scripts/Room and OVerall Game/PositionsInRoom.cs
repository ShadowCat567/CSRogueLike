using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionsInRoom : MonoBehaviour
{
    //gets the room the player and the position the players spawns in in the room
    [SerializeField] GameObject room;
    [SerializeField] GameObject playerPos;
    GameObject player;
    //makes sure the player's position gets sets to the spawn position in the room once
    bool wasActivated = false;

    //variables to tell whether the room should change
    public int enemyCounter = 0; //this is set in the inspector and does not change
    public int enemiesKilled; //this does change
    public bool canChangeRooms = false;

    [SerializeField] GameObject healthbuff;

    //tell whether or not the healthBuff is active and whether or not we are in the final room
    bool isBuffActive = false;
    [SerializeField] bool isFinal;

    AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //if the healthBuff has been collected and we are not in the final room, allowed to change to next room
        if(healthbuff.GetComponent<HealthBuffs>().collectedBuff && isFinal == false)
        {
            canChangeRooms = true;
        }

        //if the buff is not active and enemiesKilled is greater than or equal to the enemyCounter and we're not in the final room, activate the healthBuff
        if(enemyCounter <= enemiesKilled && isBuffActive == false && isFinal == false)
        {
            ActivateBuff();
        }

        //set player's position in the room when they first get here
        if(room.activeSelf == true && wasActivated == false)
        {
            ChangePosition();
        }
    }

    void ChangePosition()
    {
        //turns of character controller in player temporarily so transform.position works
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = playerPos.transform.position;
        player.GetComponent<CharacterController>().enabled = true;
        wasActivated = true;
    }

    void ActivateBuff()
    {
        //activates the healthBuff
        sound.PlayOneShot(sound.clip);
        healthbuff.SetActive(true);
        isBuffActive = true;
    }
}

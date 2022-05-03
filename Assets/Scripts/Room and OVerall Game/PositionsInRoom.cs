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
        if(healthbuff.GetComponent<HealthBuffs>().collectedBuff && isFinal == false)
        {
            canChangeRooms = true;
        }

        if(enemyCounter <= enemiesKilled && isBuffActive == false && isFinal == false)
        {
            ActivateBuff();
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

    void ActivateBuff()
    {
        sound.PlayOneShot(sound.clip);
        healthbuff.SetActive(true);
        isBuffActive = true;
    }
}

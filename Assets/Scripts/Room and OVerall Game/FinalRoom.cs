using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalRoom : MonoBehaviour
{
    //variables related to stuff in the room
    [SerializeField] GameObject boss;
    [SerializeField] GameObject exit;
    AudioSource sound;
    [SerializeField] AudioClip successSound;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        exit.GetComponent<BoxCollider>().isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if the boss is dead, play the successSound and set the exit to a trigger so the player can leave
        if(boss.GetComponent<FinalBoss>().isAlive == false)
        {
            sound.PlayOneShot(successSound);
            exit.GetComponent<BoxCollider>().isTrigger = true;
        }
    }
}

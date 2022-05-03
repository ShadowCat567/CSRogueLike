using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotSpawner : MonoBehaviour
{
    //variables related to spawning
    [SerializeField] GameObject potion;
    [SerializeField] float timeBetwnSpawns = 12.0f;
    float spawnTimer;

    //variables related to object pool
    int potionPool = 3;
    List<GameObject> potionLst = new List<GameObject>();

    //what room am I in?
    [SerializeField] GameObject curRoom;

    AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();

        //populate the object pool
        for(int i = 0; i < potionPool; i ++)
        {
            GameObject newPotion = Instantiate(potion, transform.position, Quaternion.identity);
            newPotion.SetActive(false);
            potionLst.Add(newPotion);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //spawn health objects so long as the room it is in is active and the player cannot change rooms
        if (curRoom.activeSelf && !curRoom.GetComponent<PositionsInRoom>().canChangeRooms)
        {
            spawnTimer -= Time.deltaTime;

            while(spawnTimer < 0.0f)
            {
                spawnTimer += timeBetwnSpawns;

                foreach(GameObject pot in potionLst)
                {
                    if(pot.activeSelf == false)
                    {
                        sound.PlayOneShot(sound.clip);
                        pot.SetActive(true);
                        float xPos = Random.Range(-8, 8);
                        float zPos = Random.Range(-8, 8);
                        pot.transform.position = new Vector3(transform.position.x + xPos, transform.position.y, transform.position.z + zPos);
                        break;
                    }
                }
            }
        }
    }
}

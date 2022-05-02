using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotSpawner : MonoBehaviour
{
    [SerializeField] GameObject potion;
    [SerializeField] float timeBetwnSpawns = 12.0f;

    float spawnTimer;
    int potionPool = 3;
    List<GameObject> potionLst = new List<GameObject>();

    [SerializeField] GameObject curRoom;

    // Start is called before the first frame update
    void Start()
    {
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
        if (curRoom.activeSelf || !curRoom.GetComponent<PositionsInRoom>().canChangeRooms)
        {
            spawnTimer -= Time.deltaTime;

            while(spawnTimer < 0.0f)
            {
                spawnTimer += timeBetwnSpawns;

                foreach(GameObject pot in potionLst)
                {
                    if(pot.activeSelf == false)
                    {
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

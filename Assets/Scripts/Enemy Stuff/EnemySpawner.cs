using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //variables related to spawning the object
    [SerializeField] GameObject enemy;
    [SerializeField] float secBetwnSpawn = 8.0f;
    float spawnTimer;

    //variables related to the obejct pool
    int enemyPool = 3;
    List<GameObject> EnemyLst = new List<GameObject>();

    //gets the room the spawner is in
    [SerializeField] GameObject roomManager;

    //timer to update enemiesKilled variable
    [SerializeField] float enemiesKilledTimer = 3.0f;

    //sound for spawning
    AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();

        //puts enemies in the object pool
        for(int i = 0; i < enemyPool; i ++)
        {
            GameObject newEnemy = Instantiate(enemy, transform.position, Quaternion.identity);
            newEnemy.SetActive(false);
            EnemyLst.Add(newEnemy);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //spawns enemies when the room is active and the player cannot change rooms
        if (roomManager.activeSelf && !roomManager.GetComponent<PositionsInRoom>().canChangeRooms)
        {
            spawnTimer -= Time.deltaTime;

            while (spawnTimer < 0.0f)
            {
                spawnTimer += secBetwnSpawn;

                foreach (GameObject ene in EnemyLst)
                {
                    if (ene.activeSelf == false)
                    {
                        sound.PlayOneShot(sound.clip);
                        ene.SetActive(true);
                        ene.transform.position = transform.position;
                        StartCoroutine(AddToEnemiesKilled(enemiesKilledTimer));
                        break;
                    }
                }
            }
        }
    }

    IEnumerator AddToEnemiesKilled(float timer)
    {
        //adds to the enemiesKilled variable a set period of time after an enemy has been spawned
        yield return new WaitForSeconds(timer);
        roomManager.GetComponent<PositionsInRoom>().enemiesKilled += 1;
    }
}

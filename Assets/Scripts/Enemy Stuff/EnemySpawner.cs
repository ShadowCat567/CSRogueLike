using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] float secBetwnSpawn = 8.0f;

    float spawnTimer;
    int enemyPool = 3;
    List<GameObject> EnemyLst = new List<GameObject>();

    [SerializeField] GameObject roomManager;

    // Start is called before the first frame update
    void Start()
    {
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
        if (roomManager.activeSelf)
        {
            spawnTimer -= Time.deltaTime;

            while (spawnTimer < 0.0f)
            {
                spawnTimer += secBetwnSpawn;

                foreach (GameObject ene in EnemyLst)
                {
                    if (ene.activeSelf == false)
                    {
                        ene.SetActive(true);
                        ene.transform.position = transform.position;
                        break;
                    }
                }
            }
        }
    }
}

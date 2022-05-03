using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgZone : MonoBehaviour
{
    [SerializeField] GameObject boss;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(boss.transform.position.x, boss.transform.position.y - 1.0f, boss.transform.position.z);
    }
}

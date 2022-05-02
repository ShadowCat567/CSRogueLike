using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBuffs : MonoBehaviour
{
    public int buffValue = 5;
    [SerializeField] GameObject buffObject;
    [SerializeField] GameObject curRoom;
    public bool collectedBuff = false;

    // Start is called before the first frame update
    void Start()
    {
        buffObject.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            collectedBuff = true;
            buffObject.SetActive(false);
        }
    }
}

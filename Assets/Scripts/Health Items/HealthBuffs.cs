using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBuffs : MonoBehaviour
{
    public int buffValue = 5;
    [SerializeField] GameObject buffObject;
    public bool collectedBuff = false;

    // Start is called before the first frame update
    void Start()
    {
        buffObject.SetActive(false);   
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collectedBuff = true;
            buffObject.SetActive(false);
        }
    }
}

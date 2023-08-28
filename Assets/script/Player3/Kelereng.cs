using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kelereng : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            FindObjectOfType<AudioManager>().PlaySound("PickupMarbles");
            PlayerManager.numbOfKelereng += 1;
            PlayerPrefs.SetInt("Kelereng", PlayerManager.numbOfKelereng);
            Destroy(gameObject);
        }
    }
}

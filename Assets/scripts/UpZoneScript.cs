using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpZoneScript : MonoBehaviour
{
    public GameObject colZone;
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
        if (other.gameObject.tag == "sable")
        {
            if(colZone.GetComponent<ColZoneScript>().estado != -1)
            {
                colZone.GetComponent<ColZoneScript>().changeEstado();
            }

        }
    }
    private void OnCollisionEnter(Collision collision)
    {

    }
}

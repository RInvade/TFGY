using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drumManager : MonoBehaviour
{
    public Vector3 position;
    public Quaternion rotation;
    //public int element;
    public GameObject blueBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void  OnTriggerEnter(Collider other)
    {
        prepareBox(position);
    }
    private void prepareBox(Vector3 position)
    {
        Instantiate(blueBox, position, rotation);
        Debug.Log("Imprimiendo box");
    }
}

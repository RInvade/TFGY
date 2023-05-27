using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColZoneScript : MonoBehaviour
{
    public int estado;

    // Start is called before the first frame update
    void Start()
    {
        estado = 0;
    }

    // Update is called once per frame
    void Update()
    {
            
    }
    public void changeEstado()
    {
        estado = 1;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "sable" && estado != 0)
        {
            Debug.Log("CORRECT COLLISION");
            estado = 0;

        }
        else
        {
            Debug.Log("BAD COLLISION");
            estado = -1;
            //Estamos en colision con el sable pero es un error debido a que primero tiene que tocar la parte superior y cambiar el valor del estado para entrar

        }
        
        Destroy(transform.parent.gameObject);

    }
    private void OnCollisionEnter(Collision collision)
    {

    }

}

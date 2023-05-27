using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2 : MonoBehaviour
{
   // public GameObject itemRecorded;
  //  public GameObject itemReproducer;

    public List<Vector3> datospos;
    public List<Quaternion> datosrot;
    public GameObject[] boness;
    public GameObject[] reproducebones;


    public List<List<Vector3>> bonesdatospos;
    public List<List<Quaternion>> bonesdatosrot;


    public List<float> tiempo;

    public bool graba = false;
    public bool reproduce = false;
    public float time;
    public float t;
    public float auxtime;
    int i;
    int j;

    // Start is called before the first frame update
    void Start()
    {
        i = 0;
        j = 0;
        time = 0;
        auxtime = 0;

        boness = GameObject.FindGameObjectsWithTag("Character");
        reproducebones = GameObject.FindGameObjectsWithTag("Recordered");
    }
    void guarda(GameObject itemRecorded)
    {
        datospos.Add(itemRecorded.transform.position);
        datosrot.Add(itemRecorded.transform.rotation);
    }
    void reproducir(GameObject itemReproducer)
    {

        itemReproducer.transform.position = Vector3.Lerp(datospos[i], datospos[i + reproducebones.Length ], t);
        itemReproducer.transform.rotation = Quaternion.Lerp(datosrot[i], datosrot[i + reproducebones.Length  ], t);
        i++;
    }
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("GRABANDO test  2");
            graba = true;
        }


        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("REPRODUCIENDO test  2");
            reproduce = true;
        }




        if (graba == true)
        {
            //Para cada hueso grabar en diferentes listas
            time = time+Time.deltaTime;
            if (time < 10)
            {
                Array.ForEach(boness, guarda);
                tiempo.Add(time);
            }
            else
            {
                graba = false;
            }
        }
        if(reproduce == true)
        {
            auxtime = auxtime + Time.deltaTime;
            if (auxtime < 10)
            {
                t = auxtime / tiempo[j];
                Array.ForEach(reproducebones, reproducir);
                j++;
            }
        }
    }


    
}

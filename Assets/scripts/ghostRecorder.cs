using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostRecorder : MonoBehaviour
{
    public GameObject itemRecorded;
    public GameObject itemReproducer;
    
    public List<Vector3> datospos;
    public List<Quaternion> datosrot;
    public GameObject planorec;
    RegistroController regc;
    public float paso = 0.25f;
    public bool reproduce = false;
    public int contador;
    public float speed;
    public float timeValue=0;
    public bool stop;
    // Start is called before the first frame update
    void Start()
    {
        stop = false;
        regc =planorec.GetComponent<RegistroController>();
    }

    // Update is called once per frame
    void Update()
    {
        timeValue += Time.unscaledDeltaTime;
        /*if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("GRABANDO");
            StartCoroutine(recordMovement());
        } 

        if (Input.GetKeyDown(KeyCode.C))
        {
            contador = 0;
            reproduce = true;
            itemReproducer.transform.position = datospos[0];
            itemReproducer.transform.rotation = datosrot[0];
            Debug.Log("REPETICION");
        }
        */

    }
    float frames = 0;
    float maxFrames = 6;
    float t;
    private void FixedUpdate()
    {

        if (reproduce)
        {
            if (frames > 6)
            {
                contador++;
                itemReproducer.transform.position = datospos[contador];
                frames = 0;
            }
            t = frames / maxFrames;
            Debug.Log("La T: " + t + " La frame:" + frames + "La maxFramE: " + maxFrames);
            itemReproducer.transform.position = Vector3.Lerp(itemReproducer.transform.position, datospos[contador + 1], t);
            frames++;
            if(contador + 2 > datospos.Count)
            {
                reproduce = false;
            }
        }
      

    }
    public void graba()
    {
        StartCoroutine(recordMovement());
    }
    private IEnumerator recordMovement()
    {
        int i = 0;
        //while (!Input.GetKeyDown(KeyCode.B))
        while(i<120)
        {
            yield return new WaitForSeconds(0.2F);
            datospos.Add(itemRecorded.transform.position);
            datosrot.Add(itemRecorded.transform.rotation);
            i++;
            if (stop)
            {
                stop = false;
                break;
            }
        }
    }
    public void stoprecording()
    {
        stop = true;
        regc.anadirRegistro("saltograb", 1, 1, 1,datospos,datosrot);


    }
    public void reproducemovent( List<Vector3> datospos, List<Quaternion> datosrot)
    {
        this.datospos = datospos;
        this.datosrot = datosrot;
        contador = 0;
        reproduce = true;
        itemReproducer.transform.position = datospos[0];
        itemReproducer.transform.rotation = datosrot[0];
    }
}



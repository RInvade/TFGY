using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEditor;
using UnityEngine.SceneManagement;

public class ObjectGrabber : MonoBehaviour
{
    public XRNode inputSource;
    InputDevice device;
    Vector3 posiciones;
    GameObject ignorar;
    int i = 0;
    int j = 0;
    bool pulsado = false;
    bool pulsadof = false;
    public GameObject[] controles;
    public GameObject doll;
    private bool selecting;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        device = InputDevices.GetDeviceAtXRNode(inputSource);

    }
    private void OnTriggerEnter(Collider other)
    {
        ignorar = other.gameObject;
    }

    private void OnTriggerStay(Collider other)
    {
        bool blocking;
        device.TryGetFeatureValue(CommonUsages.triggerButton, out selecting);
        device.TryGetFeatureValue(CommonUsages.gripButton, out blocking);//bloquear y desbloquear con grip
        if (other.gameObject.CompareTag("Capturar") || other.gameObject.CompareTag("Reproducir") || other.gameObject.CompareTag("Reiniciar")|| other.gameObject.CompareTag("Tumbar"))
        {
            return;
        }
        else if (other.gameObject.CompareTag("balls"))
        {
           // Debug.Log("Colisiono con bola");

            animSold elementoanim = doll.GetComponent<animSold>();

            int z = 0;
            if (blocking)
            {
                pulsadof = true;
            }
            if (blocking==false && pulsadof==true)
            {
                //Debug.Log("avanzara");

                while (z < controles.Length )
                {
                    //Debug.Log("iter"+z);
                    if (controles[z] == other.gameObject)
                    {
                        if (z == 0)
                        {
                            elementoanim.avanzar();
                            break;
                        }
                        else if (z == 1)
                        {
                            elementoanim.levantarse();
                            break;

                        }
                        else if (z == 2)
                        {

                            elementoanim.agacharse();
                            break;

                        }
                        else if (z == 3)
                        {
                            elementoanim.girari();
                            break;

                        }
                        else if (z == 4)
                        {
                            elementoanim.girard();
                            break;


                        }
                        else if (z == 5)
                        {
                            elementoanim.girar();
                            break;


                        }

                    }
                    z++;

                }


                pulsadof = false;

            }
            return;

        }
        else if (other.gameObject.CompareTag("ballcontroller"))
        {
            GameObject padrecol = other.transform.parent.gameObject;
            ghostRecorder grabrec = padrecol.GetComponent<ghostRecorder>();
            if (selecting || Input.GetKeyDown(KeyCode.Space))
            {
                if (i == 0)
                {
                    posiciones = padrecol.transform.position - this.transform.position;
                    grabrec.graba();
                    i++;
                }
                padrecol.transform.position = this.transform.position + posiciones;
                j = 0;

            }
            else if(j == 0)
            {
                i = 0;
                grabrec.stoprecording();
                j++;

            }
            else
            {
                i = 0;
            }
            return;
        }
        else if (other.gameObject == ignorar)
        {
            if (blocking)
            {
                pulsado = true;
            }
           if((pulsado == true && blocking == false))
            {
                MaterialGest[] mats = other.GetComponentsInChildren<MaterialGest>();
                int size = mats.Length;
                pulsado = false;

                other.gameObject.GetComponent<Rigidbody>();
                if (other.gameObject.GetComponent<Rigidbody>().constraints == RigidbodyConstraints.FreezeAll)
                {

                        UnlockChildren(other.transform) ;
                }
                else
                {

                    BloqParentsAux(other.transform);
                }

            }

            if (selecting || Input.GetKeyDown(KeyCode.Space))
            {
                try
                {
                    other.gameObject.GetComponent<Rigidbody>();

                    if (i == 0)
                    {
                        posiciones = other.transform.position - this.transform.position;
                        i++;
                    }
                    if (other.gameObject.GetComponent<Rigidbody>().constraints != RigidbodyConstraints.FreezeAll)
                    {
                        other.transform.position = this.transform.position + posiciones;
                    }
                    else
                    {

                    }
                }
                catch (MissingComponentException) { }

            }
            else
            {
                i = 0;
            }
            return;
        }
    }
    void SelectElement(Collider other)
    {
        posiciones = other.transform.position - this.transform.position;
        while (selecting)
        {
            if (other.gameObject.GetComponent<Rigidbody>().constraints != RigidbodyConstraints.FreezeAll)
            {
                other.transform.position = this.transform.position + posiciones;
            }
            else
            {
            }

        }

    }

    void BlockElement(Transform other)//testear02
    {
        MaterialGest[] mats = other.GetComponentsInChildren<MaterialGest>();
        int size = mats.Length;

            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        mats[size - 1].Block();
        if (other.parent != null)
        {
              BlockElement(other.parent);
        }
        return;
    }
    void BloqParentsAux(Transform other) 
    {
        while(other.parent != null)
        {
            MaterialGest[] mats = other.GetComponentsInChildren<MaterialGest>();
            int size = mats.Length;

            try
            {
                other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                mats[size - 1].Block();
            }
            catch
            {
            }
            other = other.parent;
        }
        return;
    }
    void UnlockChildren(Transform other)
    {
        MaterialGest[] mats = other.GetComponentsInChildren<MaterialGest>();
        int size = mats.Length;
        for (int i = 0; i < size;i++)
        {
            mats[i].gameObject.transform.parent.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            mats[i].Unlock();
        }
        return;
    }

}

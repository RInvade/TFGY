using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEditor;
using UnityEngine.SceneManagement;

public class ArticulationGrabber : MonoBehaviour
{
    public XRNode inputSource;
    InputDevice device;
    Vector3 positions;
    GameObject ignorar;
    int i = 0;
    int j = 0;
    bool pressed = false;
    bool releasedf = false;
    public GameObject doll;
    private bool selecting;
    public GameObject elemRecorded;

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
        device.TryGetFeatureValue(CommonUsages.triggerButton, out selecting);
        device.TryGetFeatureValue(CommonUsages.gripButton, out bool blocking);//bloquear y desbloquear con grip

        if (other.gameObject.CompareTag("balls"))
        {
            animSold elementoanim = doll.GetComponent<animSold>();
            int z = 0;
            if (blocking)
            {
                releasedf = true;
            }
            if (blocking == false && releasedf == true)
            {
                releasedf = false;
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
                    positions = padrecol.transform.position - this.transform.position;
                    grabrec.graba();
                    i++;
                }
                padrecol.transform.position = this.transform.position + positions;
                j = 0;

            }
            else if (j == 0)
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
                pressed = true;
            }
            if ((pressed == true && blocking == false))
            {
                MaterialGest[] mats = other.GetComponentsInChildren<MaterialGest>();
                int size = mats.Length;
                pressed = false;

                other.gameObject.GetComponent<Rigidbody>();
                if (other.gameObject.GetComponent<Rigidbody>().constraints == RigidbodyConstraints.FreezeAll)
                {
                    UnlockChildren(other.transform);
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
                        //Debug.Log("AQUI ENTRAMOS UNA VEZ");
                        positions = other.transform.position - this.transform.position;
                        i++;
                    }
                    if (other.gameObject.GetComponent<Rigidbody>().constraints != RigidbodyConstraints.FreezeAll)
                    {
                        other.transform.position = this.transform.position + positions;
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
        positions = other.transform.position - this.transform.position;
        while (selecting)
        {
            if (other.gameObject.GetComponent<Rigidbody>().constraints != RigidbodyConstraints.FreezeAll)
            {
                other.transform.position = this.transform.position + positions;
            }
            else
            {
            }

        }

    }

    void BlockElement(Transform other)
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
        while (other.parent != null)
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
        for (int i = 0; i < size; i++)
        {
            mats[i].gameObject.transform.parent.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            mats[i].Unlock();
        }
        return;
    }

}

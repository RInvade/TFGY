using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEditor;
using UnityEngine.SceneManagement;

public class AngleObjectGrabber : MonoBehaviour
{
    public XRNode inputSource;
    InputDevice device;
    private Vector3 posiciones;
    private GameObject cube;
    GameObject ignorar;

    bool pulsado = false;
    public GameObject[] controles;
    public GameObject mun;
    private bool selecting;
    public GameObject elemAGrabar;
    GameObject lineR;
    public Material lineMat;
    GameObject objetivoAgarre = null;
    int k = 0;
    bool colisionando;
    bool blocking;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.triggerButton, out selecting);
        device.TryGetFeatureValue(CommonUsages.gripButton, out blocking);

        if (selecting && objetivoAgarre != null)
        {
            float xdistance = 0f;
            float ydistance = 0f;
            float zdistance = 0f;
            if (k == 0)
            {
                posiciones = objetivoAgarre.transform.position - this.transform.position;

                // xdistance = this.transform.position.x - objetivoAgarre.transform.forward.x;
                // ydistance = this.transform.position.y - objetivoAgarre.transform.forward.y;
                //zdistance = this.transform.position.z - objetivoAgarre.transform.forward.z;


                //Vector3 posaux = objetivoAgarre.transform.forward;
                k++;
            }
           // Quaternion rotacion = Quaternion.LookRotation(this.transform.position);
            //objetivoAgarre.transform.rotation = rotacion;
            objetivoAgarre.transform.position = this.transform.position + posiciones;
            //objetivoAgarre.transform.LookAt(this.transform.position);// +new Vector3(xdistance,ydistance,zdistance));
            // transform.LookAt(posiciones);
           // posiciones = this.transform.position - objetivoAgarre.transform.position;
        }
        if (!selecting)
        {
            k = 0;
        }
        if (!selecting  && colisionando == false)
        {
            objetivoAgarre = null;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        ignorar = other.gameObject;
        try
        {
            if (objetivoAgarre == null)
            {
                other.gameObject.GetComponent<Rigidbody>();

                objetivoAgarre = other.gameObject;
                Debug.Log("tenemos un nuevo objetivo");
                colisionando = true;
            }
        }
        catch (MissingComponentException)
        {

        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == objetivoAgarre)
        {
            colisionando = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        bool blocking;
        device.TryGetFeatureValue(CommonUsages.triggerButton, out selecting);
        device.TryGetFeatureValue(CommonUsages.gripButton, out blocking);
        if (blocking)
        {
            pulsado = true;
        }
        if ((pulsado == true && blocking == false))
        {
            MaterialGest[] mats = other.GetComponentsInChildren<MaterialGest>();
            int size = mats.Length;
            pulsado = false;
            other.gameObject.GetComponent<Rigidbody>();
            if (other.gameObject.GetComponent<Rigidbody>().constraints == RigidbodyConstraints.FreezeAll)
            {
                desbloquearHijos(other.transform);
            }
            else
            {

                bloquearPadresAux(other.transform);
            }

        }
    }

    void bloquearPadres(Transform other)
    {
        MaterialGest[] mats = other.GetComponentsInChildren<MaterialGest>();
        int size = mats.Length;

        other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        mats[size - 1].Block();
        if (other.parent != null)
        {
            bloquearPadres(other.parent);
        }
        return;
    }
    void bloquearPadresAux(Transform other)
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
    void desbloquearHijos(Transform other)//testear02
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

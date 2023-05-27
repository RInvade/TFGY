using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class MenuManager : MonoBehaviour
{
    public Material reproduce;
    public Material reproduceI;
    public Material lieDown;
    public Material lieDownI;
    public Material capture;
    public Material captureI;
    public Material reset;
    public Material resetI;

    public GameObject menuOption1;
    public GameObject menuOption2;
    public XRNode inputSource;
    InputDevice device;
    public GameObject elemRecorded;
    public GameObject ghost;
    public GameObject bed;
    bool menu;
    int i;
    bool pressed;
    bool selected;
    bool activated;
    void Start()
    {
        menuOption1.SetActive(false);
        menuOption2.SetActive(false);
        menu = false;
        activated = false;
        i = 0;
    }

    void Update()
    {
        device = InputDevices.GetDeviceAtXRNode(inputSource);

        device.TryGetFeatureValue(CommonUsages.secondaryButton, out pressed);
        device.TryGetFeatureValue(CommonUsages.triggerButton, out selected);
        if (pressed && i == 0)
        {
            i++;
            Change();
        }
        else if(!pressed)
        {
            i = 0;
        }
        if(Input.GetKeyDown(KeyCode.C) == true)
        {
            ghost.GetComponent<GhostController>().Restart();
            elemRecorded.GetComponent<AnimController>().CaptureFrame();
        }
        else if(Input.GetKeyDown(KeyCode.R) == true)
        {
            ghost.GetComponent<GhostController>().Disable();
            elemRecorded.GetComponent<AnimController>().Reproduce();
        }
    }

    void Change()
    {
        if(menu == false)
        {
            menuOption1.SetActive(true);
            menuOption2.SetActive(true);
            menu = true;
        }
        else
        {
            menuOption1.SetActive(false);
            menuOption2.SetActive(false);
            menu = false;
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Reiniciar"))
        {
            other.gameObject.GetComponent<MeshRenderer>().material = resetI;
            if (selected == true && activated == false)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                activated = true;
            }
            
        }
        else if (other.gameObject.CompareTag("Reproducir"))
        {
            other.gameObject.GetComponent<MeshRenderer>().material = reproduceI;
            if (selected == true && activated == false)
            {
                ghost.GetComponent<GhostController>().Disable();
                elemRecorded.GetComponent<AnimController>().Reproduce();
                activated = true;
            }


        }
        else if (other.gameObject.CompareTag("Capturar"))
        {
            other.gameObject.GetComponent<MeshRenderer>().material = captureI;
            if (selected == true && activated == false)
            {
                ghost.GetComponent<GhostController>().Restart();
                elemRecorded.GetComponent<AnimController>().CaptureFrame();
                activated = true;
            }

        }
        else if (other.gameObject.CompareTag("Tumbar"))
        {
            other.gameObject.GetComponent<MeshRenderer>().material = lieDownI;
            if (selected == true && activated == false)
            {
                elemRecorded.transform.position = bed.transform.position + new Vector3(0f, 1f, -1.2f);
                elemRecorded.transform.rotation = new Quaternion(0, 90, 90, 0);
                activated = true;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        activated = false;
        if (other.gameObject.CompareTag("Reiniciar"))
        {
            other.gameObject.GetComponent<MeshRenderer>().material = reset;
        }
        else if (other.gameObject.CompareTag("Reproducir"))
        {
            other.gameObject.GetComponent<MeshRenderer>().material = reproduce;

        }
        else if (other.gameObject.CompareTag("Capturar"))
        {
            other.gameObject.GetComponent<MeshRenderer>().material = capture;

        }
        else if (other.gameObject.CompareTag("Tumbar"))
        {
            other.gameObject.GetComponent<MeshRenderer>().material = lieDown;

        }
    }


}

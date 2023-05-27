using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class MovementVR : MonoBehaviour
{

    public XRNode inputSource;
    public float speed = 1;
    private Vector2 inputAxis;
    private CharacterController character;
    public Camera cameraObj;
    InputDevice device;
    void Start()
    {
        character = GetComponent<CharacterController>();
    }

    void Update()
    {
        device = InputDevices.GetDeviceAtXRNode(inputSource);

        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);

    }

    private void FixedUpdate()
    {

        Quaternion directioninic = Quaternion.Euler(0, cameraObj.transform.rotation.eulerAngles.y, 0);

        Vector3 direction = directioninic * new Vector3(inputAxis.x, 0, inputAxis.y);

        character.Move(direction * Time.fixedDeltaTime * speed);


    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CameraMovement : MonoBehaviour
{
    public XRNode inputSource;
    private Vector2 inputAxis;
    private CharacterController character;
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
        Vector3 direction = new Vector3(inputAxis.x, 0, inputAxis.y);
        if (direction.x > 0)
        {
            character.transform.Rotate(new Vector3(0, this.gameObject.transform.rotation.y + 2, 0));
        }
        else if (direction.x < 0)
        {
            character.transform.Rotate(new Vector3(0, this.gameObject.transform.rotation.y - 2, 0));
        }

    }
}

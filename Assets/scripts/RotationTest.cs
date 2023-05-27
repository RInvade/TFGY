using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTest : MonoBehaviour
{
    public GameObject objetivo;
    float x;
    float y;
    float z;
    Quaternion forwardV;
    Vector3 upwardV;

    // Start is called before the first frame update
    void Start()
    {
        forwardV = this.gameObject.transform.rotation;
        upwardV = this.gameObject.transform.up;

    }

    // Update is called once per frame
    void Update()
    {
        x = this.transform.position.x - objetivo.transform.position.x;
        y = this.transform.position.y - objetivo.transform.position.y;
        z = this.transform.position.z - objetivo.transform.position.z;

        Vector3 direccion = objetivo.transform.position- this.transform.position;

        Quaternion rotacion = Quaternion.LookRotation(direccion) ;
        this.transform.rotation = forwardV*rotacion;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(this.transform.position, this.transform.position+new Vector3(1, 0, 0));

        Gizmos.DrawLine(this.transform.position, objetivo.transform.position);

    }
}

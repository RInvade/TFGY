using UnityEngine;

public class MaterialGest : MonoBehaviour
{
    public Material blocked;
    public Material unblocked;
    void Start()
    {
        this.GetComponent<MeshRenderer>().material = unblocked;
    }


    public void ChangeMat()
    {
        if (this.transform.parent.GetComponent<Rigidbody>().constraints == RigidbodyConstraints.None)
        {
            this.GetComponent<MeshRenderer>().material = unblocked;
        }
        else
        {
            this.GetComponent<MeshRenderer>().material = blocked;
        }
    }
    public void Unlock()
    {
        this.GetComponent<MeshRenderer>().material = unblocked;

    }
    public void Block()
    {
        this.GetComponent<MeshRenderer>().material = blocked;

    }
}

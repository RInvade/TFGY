using UnityEngine;

public class SphereInicializ : MonoBehaviour
{
    public BoxCollider[] boxBones;
    public CapsuleCollider[] capsuleBones;
    public SphereCollider[] sphereBones;


    public GameObject spherePrimitive;

    void Start()
    {
        boxBones = GetComponentsInChildren<BoxCollider>();
        capsuleBones = GetComponentsInChildren<CapsuleCollider>();
        sphereBones = GetComponentsInChildren<SphereCollider>();

        foreach (BoxCollider bodypart in boxBones)
        {
            GameObject element = bodypart.gameObject;
            GameObject sphere = Instantiate(spherePrimitive) as GameObject;

            sphere.GetComponent<Collider>().enabled = false;
            sphere.transform.rotation = bodypart.transform.rotation;
            sphere.transform.localScale = new Vector3(0.34F, 0.34F, 0.34F);
            sphere.transform.position = bodypart.transform.position+ new Vector3(0,0, 0.34F);
            sphere.transform.parent = element.transform;

        }
        foreach(SphereCollider bodypart in sphereBones)
        {
            GameObject element = bodypart.gameObject;
            GameObject sphere = Instantiate(spherePrimitive) as GameObject;

            sphere.GetComponent<Collider>().enabled = false;
            sphere.transform.rotation = bodypart.transform.rotation;
            sphere.transform.localScale =new Vector3(0.34F, 0.34F, 0.34F);
            sphere.transform.position = bodypart.transform.position ;
            sphere.transform.parent = element.transform;
        }
        foreach (CapsuleCollider bodypart in capsuleBones)
        {
            GameObject elemento = bodypart.gameObject;
            GameObject sphere = Instantiate(spherePrimitive) as GameObject;

            sphere.GetComponent<Collider>().enabled = false;
            sphere.transform.rotation = bodypart.transform.rotation;
            sphere.transform.localScale = new Vector3(0.34F, 0.34F, 0.34F);
            sphere.transform.position = bodypart.transform.position;
            sphere.transform.parent = elemento.transform;
        }
    }
}

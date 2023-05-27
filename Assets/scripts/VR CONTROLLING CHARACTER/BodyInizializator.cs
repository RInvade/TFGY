using UnityEngine;

public class BodyInizializator : MonoBehaviour
{
    public BoxCollider[] boxBones;
    public CapsuleCollider[] capsuleBones;
    public SphereCollider[] sphereBones;
    public GameObject capsulePrimitive;
    public GameObject spherePrimitive;
    public GameObject cubePrimitive;
    public float scale = 1.7f;
    void Start()
    {

        boxBones = GetComponentsInChildren<BoxCollider>();
        capsuleBones = GetComponentsInChildren<CapsuleCollider>();
        sphereBones = GetComponentsInChildren<SphereCollider>();

        foreach (BoxCollider bodypart in boxBones)
        {
            GameObject element = bodypart.gameObject;
            GameObject cube = Instantiate(cubePrimitive) as GameObject;

            cube.GetComponent<Collider>().enabled = false;
            cube.transform.rotation = bodypart.transform.rotation;
            cube.transform.localScale = bodypart.size* scale;
            cube.transform.position = bodypart.transform.position + bodypart.center;
            cube.transform.parent = element.transform;

        }
        foreach(SphereCollider bodypart in sphereBones)
        {
            GameObject element = bodypart.gameObject;
            GameObject sphere = Instantiate(spherePrimitive) as GameObject;

            sphere.GetComponent<Collider>().enabled = false;
            sphere.transform.rotation = bodypart.transform.rotation;
            sphere.transform.localScale = new Vector3(0.2F* scale, 0.2F* scale, 0.2F* scale);
            sphere.transform.position = bodypart.transform.position;
            sphere.transform.parent = element.transform;
        }
        foreach(CapsuleCollider bodypart in capsuleBones)
        {
            GameObject elemento = bodypart.gameObject;
            GameObject capsule = Instantiate(capsulePrimitive) as GameObject;
            float radius = bodypart.radius;
            float height = bodypart.height ;

            capsule.GetComponent<Collider>().enabled = false;

            if (bodypart.transform.forward.y > -0.5f)
            {
                capsule.transform.position = bodypart.transform.position - new Vector3(bodypart.center.x, bodypart.center.y, bodypart.center.z);

            }
            else 
            {
                if (bodypart.transform.right.z < 0f)
                {
                    capsule.transform.position = bodypart.transform.position + new Vector3(bodypart.height / 2, 0, bodypart.center.z);
                }
                else
                {
                    capsule.transform.position = bodypart.transform.position + new Vector3(-bodypart.height / 2, 0, bodypart.center.z);
                }

            }
            Vector3 beginPoint = bodypart.center + (height * capsule.transform.forward);
            Vector3 endPoint = bodypart.center - (height * capsule.transform.forward );

            Vector3 beginPointL = bodypart.center + (radius * capsule.transform.right);
            Vector3 endPointL = bodypart.center - (radius * capsule.transform.right);


            Vector3 localScale = capsule.transform.localScale;
            localScale.y = (beginPoint + endPoint).magnitude/2;
            localScale.x = (endPointL - beginPointL).magnitude;
            localScale.z = (endPointL - beginPointL).magnitude;

            capsule.transform.localScale = localScale* scale;
            capsule.transform.rotation = bodypart.transform.rotation;
            capsule.transform.parent = elemento.transform;

        }
    }
}

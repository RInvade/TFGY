using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soldieranim : MonoBehaviour
{
    public AnimationClip clip;
    List<estruct> hijoestr = new List<estruct>();
    Transform[] hijos;
    float i;
    // Start is called before the first frame update
    void Start()
    {
        i = 0.0f;
        clip = new AnimationClip();
        hijos = this.gameObject.GetComponentsInChildren<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("CREANDO screenshot");
            grabar();
            i = i + 1.0f;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("REPRODUCIR");
            reproducir();
        }
    }
    void grabar()
    {

        foreach (Transform elem in hijos)
        {
            estruct aux = new estruct(elem.gameObject);
            try
            {
                aux.addfr(elem.localRotation.x, elem.localRotation.y, elem.localRotation.z, i, "");

            }
            catch
            {
                aux.addfr(elem.localRotation.x, elem.localRotation.y, elem.localRotation.z, i, "");

            }
            hijoestr.Add(aux);
        }
        

    }
    void reproducir()
    {
        foreach (estruct est in hijoestr)
        {
            AnimationCurve curvex = new AnimationCurve(est.keysx.ToArray());
            AnimationCurve curvey = new AnimationCurve(est.keysy.ToArray());
            AnimationCurve curvez = new AnimationCurve(est.keysz.ToArray());
            Debug.Log("ARRAY DE "+est.elem.name+" CAMPOS"+est.keysx.ToArray().ToString());
            Debug.Log("PARENTS" + est.parent);
            clip.SetCurve(est.parent+est.elem.name, typeof(Transform), "localRotation.x", curvex);
            clip.SetCurve(est.parent + est.elem.name, typeof(Transform), "localRotation.y", curvey);
            clip.SetCurve(est.parent + est.elem.name, typeof(Transform), "localRotation.z", curvez);


        }

        Animation animation = GetComponent<Animation>();
        clip.name = "rota";
        clip.legacy = true;
        clip.wrapMode = WrapMode.Loop;
        animation.AddClip(clip, clip.name);
        animation.clip = clip;
        animation.Play(clip.name);
    }
}

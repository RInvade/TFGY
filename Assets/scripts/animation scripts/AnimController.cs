 using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System;

using System.Text;
using System.Linq;
using UnityEditor;

public class AnimController : MonoBehaviour
{
    public AnimationClip clip;
    float time;

    // AnimationCurve curvex = new AnimationCurve();
    //AnimationCurve curvey = new AnimationCurve();
    //AnimationCurve curvez = new AnimationCurve();
    //AnimationCurve curvew= new AnimationCurve();
    public Transform[] elementChildren;
    public List<AnimationCurve> curveList;
    void Start()
    {

        elementChildren = this.GetComponentsInChildren<Transform>();
        time = 0f;
        clip = new AnimationClip();
        clip.name = "rota";
        clip.legacy = true;
        clip.wrapMode = WrapMode.Loop;
        for (int i = 0; i < elementChildren.Length; i += 1)
        {
            curveList.Add(new AnimationCurve());
            curveList.Add(new AnimationCurve());
            curveList.Add(new AnimationCurve());
            curveList.Add(new AnimationCurve());

        }
    }
        void Update(){
        if (Input.GetKeyDown(KeyCode.A))
        {
            CaptureFrame();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Reproduce();
        }

    }
    public void Disable()
    {
        this.gameObject.SetActive(false);
    }
    public void CaptureFrame() {
        int z = 0;
        for (int i = 0; i < elementChildren.Length; i+=1)
        {
            curveList[z].AddKey(new Keyframe(time, elementChildren[i].localRotation.x));
            curveList[z+1].AddKey(new Keyframe(time, elementChildren[i].localRotation.y));
            curveList[z+2].AddKey(new Keyframe(time, elementChildren[i].localRotation.z));
            curveList[z+3].AddKey(new Keyframe(time, elementChildren[i].localRotation.w));
            z = z + 4;
        }
        time = time + 3;
    }






    public void Reproduce()
    {
        for (int i = 0; i < elementChildren.Length; i += 1)
        {
            string elementName = GetGameObjectPath(elementChildren[i]);
            elementName = DeleteRootString(this.gameObject.name, elementName);
            clip.SetCurve(elementName, typeof(Transform), "localRotation.x", curveList[i*4]);
            clip.SetCurve(elementName, typeof(Transform), "localRotation.y", curveList[i*4 + 1]);
            clip.SetCurve(elementName, typeof(Transform), "localRotation.z", curveList[i*4 + 2]);
            clip.SetCurve(elementName, typeof(Transform), "localRotation.w", curveList[i*4 + 3]);
        }
        Animation anim = GetComponent<Animation>();
        anim.enabled = true;
        anim.AddClip(clip, clip.name);
        anim.clip = clip;
        anim.Play(clip.name);
    }


    public static string GetGameObjectPath(Transform obj)
    {
        if (obj.parent != null)
        {
            return GetGameObjectPath(obj.parent) + "/" + obj.name;
        }
        return obj.name;
    }
    
    public string DeleteRootString(string str1, string objetivo)
    {
        int x = str1.Length;
        if(x == objetivo.Length)
        {
            objetivo = objetivo.Substring(x);
        }
        else
        {
            objetivo = objetivo.Substring(x+1);
        }
        return objetivo;

    }

}

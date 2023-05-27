using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class estruct 
{
    public List<Keyframe> keysx;
    public List<Keyframe> keysy;
    public List<Keyframe> keysz;
    public string parent;
    public GameObject elem;
    public estruct(GameObject elem)
    {
        this.elem = elem;
        keysx = new List<Keyframe>();
        keysy = new List<Keyframe>();
        keysz = new List<Keyframe>();

    }
    public void addfr(float x, float y, float z,float time,string parent)
    {

        keysx.Add(new Keyframe(time,x));
        keysy.Add(new Keyframe(time, y));
        keysz.Add(new Keyframe(time, z));
        this.parent = parent;
    }
}

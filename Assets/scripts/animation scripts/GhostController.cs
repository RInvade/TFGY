using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public AnimationClip clip;
    float time;
    Animation anim;
    public GameObject hipsGhost;
    public GameObject hips;
    public Transform[] elementChildren;
    public List<AnimationCurve> curveList;
    public GameObject imitatedDoll;
    // Start is called before the first frame update
    void Start()
    {
        curveList.Clear();
        anim = GetComponent<Animation>();

        elementChildren = imitatedDoll.GetComponentsInChildren<Transform>();
        time = 0f;
        clip = new AnimationClip();
        clip.name = "rota";
        clip.legacy = true;
        clip.wrapMode = WrapMode.Once;
        for (int i = 0; i < elementChildren.Length; i += 1)
        {
            curveList.Add(new AnimationCurve());
            curveList.Add(new AnimationCurve());
            curveList.Add(new AnimationCurve());
            curveList.Add(new AnimationCurve());

        }
        CaptureInitialPosition();
        CaptureActualPosition();
    }
    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = new Vector3(elementChildren[0].position.x, elementChildren[0].position.y, elementChildren[0].position.z);
        hipsGhost.transform.position = new Vector3(hips.transform.position.x, hips.transform.position.y, hips.transform.position.z);
        if (!anim.isPlaying)
        {
            EditFrame(1);
            Reproduce();
        }

    }
    public void Restart()
    {
        Start();
    }
    public void Disable()
    {
        this.gameObject.SetActive(false);
    }
    public void EditFrame(int frame)
    {
        int z = 0;
        time = 3;
        Debug.Log("frame "+frame);
        Debug.Log("longitud "+curveList[z].length);
        for (int i = 0; i < elementChildren.Length; i += 1)
        {
            curveList[z].MoveKey(frame, new Keyframe(time, elementChildren[i].localRotation.x));
            curveList[z + 1].MoveKey(frame, new Keyframe(time, elementChildren[i].localRotation.y));
            curveList[z + 2].MoveKey(frame, new Keyframe(time, elementChildren[i].localRotation.z));
            curveList[z + 3].MoveKey(frame, new Keyframe(time, elementChildren[i].localRotation.w));

            z = z + 4;

        }
    }
    public void CaptureInitialPosition()
    {
        int z = 0;
        time = 0;
        for (int i = 0; i < elementChildren.Length; i += 1)
        {
            curveList[z].AddKey(new Keyframe(time, elementChildren[i].localRotation.x));
            curveList[z + 1].AddKey(new Keyframe(time, elementChildren[i].localRotation.y));
            curveList[z + 2].AddKey(new Keyframe(time, elementChildren[i].localRotation.z));
            curveList[z + 3].AddKey(new Keyframe(time, elementChildren[i].localRotation.w));

            z = z + 4;

        }
        time = time + 3;
    }
    public void CaptureActualPosition()
    {
        int z = 0;
        time = 3;
        for (int i = 0; i < elementChildren.Length; i += 1)
        {
            curveList[z].AddKey(new Keyframe(time, elementChildren[i].localRotation.x));
            curveList[z + 1].AddKey(new Keyframe(time, elementChildren[i].localRotation.y));
            curveList[z + 2].AddKey(new Keyframe(time, elementChildren[i].localRotation.z));
            curveList[z + 3].AddKey(new Keyframe(time, elementChildren[i].localRotation.w));

            z = z + 4;
        }
    }

    public void Reproduce()
    {

        for (int i = 0; i < elementChildren.Length; i += 1)
        {
            string elementName = GetGameObjectPath(elementChildren[i]);
            elementName = DeleteRootString(imitatedDoll.gameObject.name, elementName);
            clip.SetCurve(elementName, typeof(Transform), "localRotation.x", curveList[i * 4]);
            clip.SetCurve(elementName, typeof(Transform), "localRotation.y", curveList[i * 4 + 1]);
            clip.SetCurve(elementName, typeof(Transform), "localRotation.z", curveList[i * 4 + 2]);
            clip.SetCurve(elementName, typeof(Transform), "localRotation.w", curveList[i * 4 + 3]);

        }

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

    public string DeleteRootString(string str1, string objective)
    {
        int x = str1.Length;
        if (x == objective.Length)
        {
            objective = objective.Substring(x);
        }
        else
        {
            objective = objective.Substring(x + 1);
        }
        return objective;

    }

}

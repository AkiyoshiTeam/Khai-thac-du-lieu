using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjOnWall : MonoBehaviour
{
    public int wall;
    float x;
    void OnTriggerEnter(Collider obj)
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (obj.transform.tag == "DayKe")
            {
                Drag.onWall = wall;
                switch (wall)
                {
                    case 1:
                        while (obj.transform.childCount > 1)
                        {
                            x = GameObject.FindWithTag("DayKeTop").transform.childCount - 1;
                            Transform child = obj.transform.GetChild(1);
                            child.SetParent(GameObject.FindWithTag("DayKeTop").transform);
                            child.localPosition = new Vector3(x * 1.95f, 0, 0);
                            child.localRotation = new Quaternion(0, 0, 0, 0);
                        }
                        break;
                    case 2:
                        while (obj.transform.childCount > 1)
                        {
                            x = GameObject.FindWithTag("DayKeTopRight").transform.childCount - 1;
                            Transform child = obj.transform.GetChild(1);
                            child.SetParent(GameObject.FindWithTag("DayKeTopRight").transform);
                            child.localPosition = new Vector3(x * 1.95f, 0, 0);
                            child.localRotation = new Quaternion(0, 0, 0, 0);
                        }
                        break;
                    case 3:
                        while (obj.transform.childCount > 1)
                        {
                            x = GameObject.FindWithTag("DayKeRight").transform.childCount - 1;
                            Transform child = obj.transform.GetChild(1);
                            child.SetParent(GameObject.FindWithTag("DayKeRight").transform);
                            child.localPosition = new Vector3(x * 1.95f, 0, 0);
                            child.localRotation = new Quaternion(0, 0, 0, 0);
                        }
                        break;
                    case 4:
                        while (obj.transform.childCount > 1)
                        {
                            x = GameObject.FindWithTag("DayKeBot").transform.childCount - 1;
                            Transform child = obj.transform.GetChild(1);
                            child.SetParent(GameObject.FindWithTag("DayKeBot").transform);
                            child.localPosition = new Vector3(x * 1.95f, 0, 0);
                            child.localRotation = new Quaternion(0, 0, 0, 0);
                        }
                        break;
                }
                Destroy(obj.gameObject);
            }
        }
        else
        {
            if (obj.transform.tag == "Ke")
            {
                Drag.onWall = wall;
                switch (wall)
                {
                    case 1:
                        x = GameObject.FindWithTag("DayKeTop").transform.childCount - 1;
                        obj.transform.SetParent(GameObject.FindWithTag("DayKeTop").transform);
                        obj.transform.localPosition = new Vector3(x * 1.95f, 0, 0);
                        obj.transform.localRotation = new Quaternion(0, 0, 0, 0);
                        break;
                    case 2:
                        x = GameObject.FindWithTag("DayKeTopRight").transform.childCount - 1;
                        obj.transform.SetParent(GameObject.FindWithTag("DayKeTopRight").transform);
                        obj.transform.localPosition = new Vector3(x * 1.95f, 0, 0);
                        obj.transform.localRotation = new Quaternion(0, 0, 0, 0);
                        break;
                    case 3:
                        x = GameObject.FindWithTag("DayKeRight").transform.childCount - 1;
                        obj.transform.SetParent(GameObject.FindWithTag("DayKeRight").transform);
                        obj.transform.localPosition = new Vector3(x * 1.95f, 0, 0);
                        obj.transform.localRotation = new Quaternion(0, 0, 0, 0);
                        break;
                    case 4:
                        x = GameObject.FindWithTag("DayKeBot").transform.childCount - 1;
                        obj.transform.SetParent(GameObject.FindWithTag("DayKeBot").transform);
                        obj.transform.localPosition = new Vector3(x * 1.95f, 0, 0);
                        obj.transform.localRotation = new Quaternion(0, 0, 0, 0);
                        break;
                }
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Drop : NetworkBehaviour
{
    public Light highLight;
    public static Transform objDrag;
    public static bool draged = false;
    public bool isDrag = false;
    public static bool sort = false;

    public void OnMouseEnter()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (draged)
            {
                if (!isDrag)
                {
                    highLight.enabled = true;
                    Drag drag = objDrag.GetComponent<Drag>();

                    if (Math.Round(objDrag.parent.eulerAngles.y, 0) == 0 && Math.Round(transform.parent.eulerAngles.y, 0) == 180)
                    {
                        float pos = 1.95f * (transform.parent.childCount - 1);
                        drag.x = transform.parent.position.x - pos - 0.05f;
                        drag.z = transform.parent.position.z - 0.1f;
                        drag.focusPosition = true;
                    }
                    if (Math.Round(objDrag.parent.eulerAngles.y, 0) == 180 && Math.Round(transform.parent.eulerAngles.y, 0) == 0)
                    {
                        float pos = 1.95f * (transform.parent.childCount - 1);
                        drag.x = transform.parent.position.x + pos + 0.05f;
                        drag.z = transform.parent.position.z + 0.1f;
                        drag.focusPosition = true;
                    }
                    if (Math.Round(objDrag.parent.eulerAngles.y, 0) == 90 && Math.Round(transform.parent.eulerAngles.y, 0) == 270)
                    {
                        float pos = 1.95f * (transform.parent.childCount - 1);
                        drag.x = transform.parent.position.x - 0.1f;
                        drag.z = transform.parent.position.z + pos + 0.05f;
                        drag.focusPosition = true;

                    }
                    if (Math.Round(objDrag.parent.eulerAngles.y, 0) == 270 && Math.Round(transform.parent.eulerAngles.y, 0) == 90)
                    {
                        float pos = 1.95f * (transform.parent.childCount - 1);
                        drag.x = transform.parent.position.x + 0.1f;
                        drag.z = transform.parent.position.z - pos - 0.05f;
                        drag.focusPosition = true;
                    }
                    if (Math.Round(objDrag.parent.eulerAngles.y, 0) == 45 && Math.Round(transform.parent.eulerAngles.y, 0) == 225)
                    {
                        float pos = 1.38f * (transform.parent.childCount - 1);
                        float x = transform.parent.position.x - 0.1414f;
                        float z = transform.parent.position.z - 0.1414f;
                        drag.x = x - pos;
                        drag.z = z + pos;
                        drag.focusPosition = true;
                    }
                    if (Math.Round(objDrag.parent.eulerAngles.y, 0) == 225 && Math.Round(transform.parent.eulerAngles.y, 0) == 45)
                    {
                        float pos = 1.38f * (transform.parent.childCount - 1);
                        float x = transform.parent.position.x + 0.1414f;
                        float z = transform.parent.position.z + 0.1414f;
                        drag.x = x + pos;
                        drag.z = z - pos;
                        drag.focusPosition = true;
                    }
                }
            }
            else
            {
                highLight.enabled = false;
            }
        }
        else
        {
            if (draged)
            {
                if (!isDrag)
                {
                    if (transform.parent != null)
                    {
                        highLight.enabled = true;
                        Drag drag = objDrag.GetComponent<Drag>();
                        drag.x = transform.GetSiblingIndex() * 1.95f;
                        drag.parent = transform.parent;
                        drag.index = transform.GetSiblingIndex() + 1;
                        drag.focusPosition = true;
                        for (int i = transform.GetSiblingIndex() + 1; i < transform.parent.childCount; i++)
                        {
                            transform.parent.GetChild(i).localPosition += new Vector3(1.95f, 0, 0);
                        }
                        sort = true;
                    }
                    else
                    {
                        highLight.enabled = true;
                        Drag drag = objDrag.GetComponent<Drag>();
                        drag.x = 1.95f;
                        drag.parent = transform.parent;
                        drag.index = 2;
                        drag.keFirst = transform;
                        drag.focusPosition = true;
                        drag.createNew = true;
                        sort = true;
                    }
                }
            }
            else
            {
                highLight.enabled = false;
            }
        }
    }

    public void OnMouseExit()
    {
        if (objDrag != null)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                Drag drag = objDrag.GetComponent<Drag>();
                highLight.enabled = false;
                drag.focusPosition = false;
                drag.createNew = false;
            }
            else
            {

                Drag drag = objDrag.GetComponent<Drag>();
                highLight.enabled = false;
                drag.focusPosition = false;
                drag.createNew = false;
                if (sort)
                {
                    for (int i = transform.GetSiblingIndex() + 1; i < transform.parent.childCount; i++)
                    {
                        transform.parent.GetChild(i).localPosition -= new Vector3(1.95f, 0, 0);
                    }
                    sort = false;
                }
            }
        }
    }


}

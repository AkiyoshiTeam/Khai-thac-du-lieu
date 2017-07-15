using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Drag : NetworkBehaviour
{
    public Ke ke;
    public float x;
    public float z;
    public Transform parent;
    public Transform keFirst;
    public int index;
    public bool focusPosition = false;
    public bool createNew = false;
    public static int onWall = 0;
    public GameObject[] respawns;
    bool editParent = false;
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (transform.parent != null)
            {
                RotateDayKe.choseDayKe = true;
                if (RotateDayKe.dayKe != null)
                    RotateDayKe.dayKe.transform.GetChild(0).GetComponent<Outline>().enabled = false;
                if (RotateDayKe.dayKe != transform.parent.gameObject)
                {
                    RotateDayKe.dayKe = transform.parent.gameObject;
                    transform.parent.GetChild(0).GetComponent<Outline>().enabled = true;
                    RotateDayKe.nameTxtSta.GetComponent<InputField>().text = transform.parent.GetComponent<DragDayKe>().dayKe.Name;
                }
                else
                {
                    RotateDayKe.choseDayKe = false;
                    transform.parent.GetChild(0).GetComponent<Outline>().enabled = false;
                    RotateDayKe.nameTxtSta.GetComponent<InputField>().text = "";
                }
            }
            else
            {
                RotateDayKe.choseDayKe = false;
                RotateDayKe.dayKe.transform.GetChild(0).GetComponent<Outline>().enabled = false;
                RotateDayKe.nameTxtSta.GetComponent<InputField>().text = "";
            }
        }

    }

    private Vector3 distance;

    void OnMouseDown()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (transform.parent.GetComponent<DragDayKe>().allowDrag)
            {
                //RaycastHit hit;
                //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //if (Physics.Raycast(ray, out hit))
                //{
                //    distance = hit.point - transform.parent.position;

                //}
                distance = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.parent.position).z)) - transform.parent.position;
                Drop.objDrag = transform;
                Drop.draged = true;
                gameObject.GetComponent<Drop>().isDrag = true;
            }
        }
        else
        {
            distance = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z)) - transform.position;
            Drop.objDrag = transform;
            Drop.draged = true;
            gameObject.GetComponent<Drop>().isDrag = true;
            editParent = true;
            transform.GetComponent<BoxCollider>().enabled = false;
        }

    }

    public void OnMouseDrag()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (transform.parent.GetComponent<DragDayKe>().allowDrag)
            {
                if (onWall == 0)
                {
                    //RaycastHit hit; Vector3 objPosition = new Vector3();
                    //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    //if (Physics.Raycast(ray, out hit))
                    //{
                    //    objPosition = hit.point;
                    //}
                    Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.parent.position).z);
                    Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                    transform.parent.position = new Vector3(objPosition.x - distance.x, 0, objPosition.z - distance.z);
                }
            }
        }
        else
        {
            //if (onWall == 0)
            //{
            if (editParent)
            {
                if (transform.parent != null)
                {
                    GameObject go = transform.parent.gameObject;
                    int siblingIndex = transform.GetSiblingIndex();
                    transform.SetParent(null);
                    if (go.transform.childCount > 1)
                    {
                        for (int i = siblingIndex; i < go.transform.childCount; i++)
                        {
                            go.transform.GetChild(i).localPosition -= new Vector3(1.95f, 0, 0);
                        }
                    }
                    else
                    {
                        if (go.name != "DayKeTop" && go.name != "DayKeTopRight" && go.name != "DayKeBot" && go.name != "DayKeRight")
                            Destroy(go);
                    }
                }
                editParent = false;
            }
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            GameObject.FindGameObjectWithTag("KeHide").transform.position = new Vector3(Mathf.Clamp(objPosition.x - distance.x, -14f, 39f), 0, Mathf.Clamp(objPosition.z - distance.z, -24f, 24f));
            //if (((objPosition.x - distance.x) + (objPosition.z - distance.z)) < 48)
            if ((GameObject.FindGameObjectWithTag("KeHide").transform.GetChild(0).GetChild(0).position.x + GameObject.FindGameObjectWithTag("KeHide").transform.GetChild(0).GetChild(0).position.z < 48)
                && (GameObject.FindGameObjectWithTag("KeHide").transform.GetChild(0).GetChild(1).position.x + GameObject.FindGameObjectWithTag("KeHide").transform.GetChild(0).GetChild(1).position.z < 48)
                && (GameObject.FindGameObjectWithTag("KeHide").transform.GetChild(0).GetChild(2).position.x + GameObject.FindGameObjectWithTag("KeHide").transform.GetChild(0).GetChild(2).position.z < 48)
                && (GameObject.FindGameObjectWithTag("KeHide").transform.GetChild(0).GetChild(3).position.x + GameObject.FindGameObjectWithTag("KeHide").transform.GetChild(0).GetChild(3).position.z < 48)
                && ((GameObject.FindGameObjectWithTag("KeHide").transform.GetChild(0).GetChild(0).position.x < 42f 
                    && GameObject.FindGameObjectWithTag("KeHide").transform.GetChild(0).GetChild(1).position.x < 42f
                    && GameObject.FindGameObjectWithTag("KeHide").transform.GetChild(0).GetChild(2).position.x < 42f
                    && GameObject.FindGameObjectWithTag("KeHide").transform.GetChild(0).GetChild(3).position.x < 42f) && 
                    (GameObject.FindGameObjectWithTag("KeHide").transform.GetChild(0).GetChild(0).position.x > -16f
                    && GameObject.FindGameObjectWithTag("KeHide").transform.GetChild(0).GetChild(1).position.x > -16f
                    && GameObject.FindGameObjectWithTag("KeHide").transform.GetChild(0).GetChild(2).position.x > -16f
                    && GameObject.FindGameObjectWithTag("KeHide").transform.GetChild(0).GetChild(3).position.x > -16f)
                    )
                && (GameObject.FindGameObjectWithTag("KeHide").transform.GetChild(0).GetChild(0).position.z < 26f
                    && GameObject.FindGameObjectWithTag("KeHide").transform.GetChild(0).GetChild(1).position.z < 26f
                    && GameObject.FindGameObjectWithTag("KeHide").transform.GetChild(0).GetChild(2).position.z < 26f
                    && GameObject.FindGameObjectWithTag("KeHide").transform.GetChild(0).GetChild(3).position.z < 26f) &&
                    (GameObject.FindGameObjectWithTag("KeHide").transform.GetChild(0).GetChild(0).position.z > -24f
                    && GameObject.FindGameObjectWithTag("KeHide").transform.GetChild(0).GetChild(1).position.z > -24f
                    && GameObject.FindGameObjectWithTag("KeHide").transform.GetChild(0).GetChild(2).position.z > -24f
                    && GameObject.FindGameObjectWithTag("KeHide").transform.GetChild(0).GetChild(3).position.z > -24f)
                )
                transform.position = new Vector3(Mathf.Clamp(objPosition.x - distance.x, -14f, 39f), 0, Mathf.Clamp(objPosition.z - distance.z, -24f, 24f));
            //}
        }
    }

    public void OnMouseUp()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (transform.parent.GetComponent<DragDayKe>().allowDrag)
            {
                gameObject.GetComponent<Drop>().isDrag = false;
                Drop.draged = false;
                //onWall = 0;
                if (focusPosition)
                {
                    transform.parent.position = new Vector3(x, 0, z);
                }

            }
        }
        else
        {
            gameObject.GetComponent<Drop>().isDrag = false;
            Drop.draged = false;
            //onWall = 0;
            if (createNew)
                CreateNewDayKe(keFirst);
            if (focusPosition)
            {
                transform.SetParent(parent);
                transform.SetSiblingIndex(index);
                transform.localPosition = new Vector3(x, 0, 0);
                transform.localRotation = new Quaternion(0, 0, 0, 0);
                Drop.sort = false;
            }
        }

        transform.GetComponent<BoxCollider>().enabled = true;
    }

    void CreateNewDayKe(Transform keDau)
    {
        Debug.Log(NextID(CreateObjOnMap.listDayKe[CreateObjOnMap.listDayKe.Count - 1].Id, "DK"));
        DayKe dayKeTemp = new DayKe();
        dayKeTemp.Id = NextID(CreateObjOnMap.listDayKe[CreateObjOnMap.listDayKe.Count - 1].Id, "DK");
        dayKeTemp.X = keDau.position.x;
        dayKeTemp.Z = keDau.position.z;
        dayKeTemp.RotY = (int)keDau.eulerAngles.y;
        dayKeTemp.Name = dayKeTemp.Id;
        GameObject objDK = Instantiate(CreateObjOnMap.dayKePrefabSta, new Vector3(keDau.position.x, 0, keDau.position.z), keDau.rotation);
        objDK.name = NextID(CreateObjOnMap.listDayKe[CreateObjOnMap.listDayKe.Count - 1].Id, "DK");
        dayKeTemp.Obj = objDK;
        objDK.GetComponent<DragDayKe>().dayKe = dayKeTemp;
        CreateObjOnMap.listDayKe.Add(dayKeTemp);
        keDau.SetParent(objDK.transform);
        parent = objDK.transform;
        objDK.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = dayKeTemp.Name;
    }

    public string NextID(string lastID, string prefixID)
    {
        if (lastID == "")
        {
            return prefixID + "001";  // fixwidth default
        }
        int nextID = int.Parse(lastID.Remove(0, prefixID.Length)) + 1;
        int lengthNumerID = lastID.Trim().Length - prefixID.Length;
        string zeroNumber = "";
        for (int i = 1; i <= lengthNumerID; i++)
        {
            if (nextID < Math.Pow(10, i))
            {
                for (int j = 1; j <= lengthNumerID - i; i++)
                {
                    zeroNumber += "0";
                }
                return prefixID + zeroNumber + nextID.ToString();
            }
        }
        return prefixID + nextID;
    }
}
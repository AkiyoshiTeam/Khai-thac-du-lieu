using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNewKe : MonoBehaviour
{

    void OnMouseDown()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                GameObject objKe = Instantiate(CreateObjOnMap.kePrefabSta, hit.point, new Quaternion(0, 0, 0, 0));
                objKe.GetComponent<BoxCollider>().enabled = true;
                Ke ke = new Ke();
                ke.Id = NextID(CreateObjOnMap.listKe[CreateObjOnMap.listKe.Count-1].Id, "K");
                ke.X = 0;
                ke.DayKeID = "";
                objKe.name = ke.Id;
                ke.Obj = objKe;
                objKe.GetComponent<Drag>().ke = ke;
                CreateObjOnMap.listKe.Add(ke);
            }
        }

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

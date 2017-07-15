using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCamClick : MonoBehaviour
{
    public static bool isAdmin = false;
    public GameObject[] respawns;
    public GameObject adminControl;
    public GameObject saveBtn;

    string conn;
    IDataReader reader;
    IDbConnection dbconn;
    IDbCommand dbcmd;

    public void ChangeCam(string cam)
    {
        if (!isAdmin)
        {
            isAdmin = true;
            respawns = GameObject.FindGameObjectsWithTag("Ke");
            foreach (GameObject respawn in respawns)
            {
                respawn.GetComponent<BoxCollider>().enabled = true;
            }
            respawns = GameObject.FindGameObjectsWithTag("Position");
            foreach (GameObject respawn in respawns)
            {
                respawn.GetComponent<BoxCollider>().enabled = false;
            }
            respawns = GameObject.FindGameObjectsWithTag("Khung");
            foreach (GameObject respawn in respawns)
            {
                respawn.GetComponent<BoxCollider>().enabled = false;
            }
            foreach (SanPham sp in CreateObjOnMap.listItem)
            {
                Destroy(sp.Obj);
            }
            CreateObjOnMap.listItem.Clear();
            adminControl.SetActive(true);
            saveBtn.SetActive(true);
            Camera.main.depth = 2;
        }
        else
        {
            isAdmin = false;
            respawns = GameObject.FindGameObjectsWithTag("Ke");
            foreach (GameObject respawn in respawns)
            {
                respawn.GetComponent<BoxCollider>().enabled = false;
            }
            respawns = GameObject.FindGameObjectsWithTag("Position");
            foreach (GameObject respawn in respawns)
            {
                respawn.GetComponent<BoxCollider>().enabled = true;
            }
            respawns = GameObject.FindGameObjectsWithTag("Khung");
            foreach (GameObject respawn in respawns)
            {
                respawn.GetComponent<BoxCollider>().enabled = true;
            }
            ReCreateItem();
            adminControl.SetActive(false);
            saveBtn.SetActive(false);
            Camera.main.depth = -1;
        }
    }

    public void ReCreateItem()
    {
        conn = "URI=file:" + Application.dataPath + "/dbSM.s3db";
        try
        {
            dbconn = new SqliteConnection(conn);
            dbconn.Open();
            string sqlQuery = "SELECT * FROM SanPham";
            dbcmd = dbconn.CreateCommand();
            dbcmd.CommandText = sqlQuery;
            reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                if (reader.GetString(9) == "N")
                {
                    SanPham sanPham = new SanPham();
                    sanPham.Id = reader.GetString(0);
                    sanPham.Name = reader.GetString(1);
                    sanPham.Type = reader.GetString(2);
                    sanPham.KeID1 = reader.GetString(3);
                    sanPham.KeID2 = reader.GetString(4);
                    sanPham.Size = (int)reader.GetFloat(5);
                    sanPham.Price = reader.GetInt32(6);
                    sanPham.Pos1 = reader.GetInt32(7);
                    sanPham.Pos2 = reader.GetInt32(8);
                    GameObject item;
                    if (sanPham.Size == 1)
                        item = Instantiate(CreateObjOnMap.item1xSta, FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position, FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).rotation);
                    else
                    {
                        int y = (int)FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).eulerAngles.y;
                        switch (y)
                        {
                            case 0:
                                item = Instantiate(CreateObjOnMap.item2xSta, new Vector3(FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.x + 0.5f, FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.y, FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.z), FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).rotation);
                                break;
                            case 180:
                                item = Instantiate(CreateObjOnMap.item2xSta, new Vector3(FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.x - 0.5f, FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.y, FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.z), FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).rotation);
                                break;
                            case 90:
                                item = Instantiate(CreateObjOnMap.item2xSta, new Vector3(FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.x, FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.y, FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.z - 0.5f), FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).rotation);
                                break;
                            case 270:
                                item = Instantiate(CreateObjOnMap.item2xSta, new Vector3(FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.x, FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.y, FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.z + 0.5f), FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).rotation);
                                break;
                            case 44:
                                item = Instantiate(CreateObjOnMap.item2xSta, new Vector3(FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.x + 0.3535f, FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.y, FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.z - 0.3535f), FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).rotation);
                                break;
                            case 224:
                                item = Instantiate(CreateObjOnMap.item2xSta, new Vector3(FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.x - 0.3535f, FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.y, FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.z + 0.3535f), FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).rotation);
                                break;
                            default:
                                item = Instantiate(CreateObjOnMap.item2xSta, new Vector3(FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.x + 0.5f, FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.y, FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.z), FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).rotation);
                                break;
                        }
                    }

                    FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).GetComponent<BoxCollider>().enabled = false;
                    item.GetComponent<DragItem>().posObj = FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1);
                    item.GetComponent<DragItem>().posObj.GetComponent<DropItem>().allowDrop = false;

                    FindKe(sanPham.KeID2).transform.Search("Position " + sanPham.Pos2).GetComponent<BoxCollider>().enabled = false;
                    item.GetComponent<DragItem>().posObj2 = FindKe(sanPham.KeID2).transform.Search("Position " + sanPham.Pos2);
                    item.GetComponent<DragItem>().posObj2.GetComponent<DropItem>().allowDrop = false;
                    sanPham.Obj = item;
                    item.GetComponent<DragItem>().sanPham = sanPham;
                    CreateObjOnMap.listItem.Add(sanPham);
                }
            }
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
            dbconn = null;
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    GameObject FindKe(string name)
    {
        foreach (Ke ke in CreateObjOnMap.listKe)
        {
            if (ke.Id == name)
                return ke.Obj;
        }
        return null;
    }
}

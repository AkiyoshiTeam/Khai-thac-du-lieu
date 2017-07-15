using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.Data.SqlClient;
using Mono.Data.Sqlite;
using System;
using UnityEngine.UI;
using System.IO;

public class CreateObjOnMap : MonoBehaviour
{

    public GameObject item1x;
    public GameObject item2x;
    public GameObject dayKePrefab;
    public GameObject kePrefab;
    public static GameObject dayKePrefabSta;
    public static GameObject kePrefabSta;
    public static GameObject item1xSta;
    public static GameObject item2xSta;


    public static List<DayKe> listDayKe;
    public static List<Ke> listKe;
    public static List<SanPham> listItemAll;
    public static List<SanPham> listItem;
    public static List<SanPham> listItemWareHouse;
    public static List<LoaiSanPham> listType;
    public static List<string> listNameType;

    public static string conn;
    public static IDataReader reader;
    public static IDbConnection dbconn;
    public static IDbCommand dbcmd;

    void Start()
    {
        dayKePrefabSta = dayKePrefab;
        kePrefabSta = kePrefab;
        item1xSta = item1x;
        item2xSta = item2x;
        listDayKe = new List<DayKe>();
        listKe = new List<Ke>();
        listItemAll = new List<SanPham>();
        listItem = new List<SanPham>();
        listItemWareHouse = new List<SanPham>();
        listType = new List<LoaiSanPham>();
        listNameType = new List<string>();
        conn = "URI=file:" + Application.dataPath + "/dbSM.s3db";
        try
        {
            dbconn = new SqliteConnection(conn);
            dbconn.Open();
            dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT * FROM DayKe";
            dbcmd.CommandText = sqlQuery;
            reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                string keID = reader.GetString(0);
                string name = reader.GetString(1);
                float X = reader.GetFloat(2);
                float Z = reader.GetFloat(3);
                int rotY = reader.GetInt32(4);
                DayKe dayKe = new DayKe();
                dayKe.Id = keID;
                dayKe.X = X;
                dayKe.Z = Z;
                dayKe.RotY = rotY;
                dayKe.Name = name;
                GameObject objDK = Instantiate(dayKePrefab, new Vector3(dayKe.X, 0, dayKe.Z), new Quaternion(0, 0, 0, 0));
                objDK.transform.eulerAngles = new Vector3(0, dayKe.RotY, 0);
                objDK.name = dayKe.Id;
                dayKe.Obj = objDK;
                objDK.GetComponent<DragDayKe>().dayKe = dayKe;
                objDK.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = dayKe.Name;
                listDayKe.Add(dayKe);

            }

            sqlQuery = "SELECT * FROM Ke";
            dbcmd = dbconn.CreateCommand();
            dbcmd.CommandText = sqlQuery;
            reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                string keID = reader.GetString(0);
                string daykeID = reader.GetString(1);
                float X = reader.GetFloat(2);
                Ke ke = new Ke();
                ke.Id = keID;
                ke.X = X;
                ke.DayKeID = daykeID;
                GameObject objKe = Instantiate(kePrefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
                objKe.name = ke.Id;
                objKe.transform.SetParent(FindParent(ke.DayKeID).transform);
                objKe.transform.localPosition = new Vector3(ke.X, 0, 0);
                objKe.transform.localEulerAngles = new Vector3(0, 0, 0);
                ke.Obj = objKe;
                objKe.GetComponent<Drag>().ke = ke;
                listKe.Add(ke);
            }

            sqlQuery = "SELECT * FROM SanPham";
            dbcmd = dbconn.CreateCommand();
            dbcmd.CommandText = sqlQuery;
            reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                if (reader.GetString(9) == "N")
                {
                    GameObject ke1Found = FindKe(reader.GetString(3));
                    if (ke1Found != null)
                    {
                        GameObject ke2Found = FindKe(reader.GetString(4));
                        if ((int)reader.GetFloat(5) == 1 || ((int)reader.GetFloat(5) == 2 && ke2Found != null && ((ke1Found = ke2Found) || (ke1Found.transform.parent == ke2Found.transform.parent && ke1Found.transform.GetSiblingIndex() == ke2Found.transform.GetSiblingIndex() - 1))))
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
                            sanPham.Status = false;
                            sanPham.Description = reader.GetString(10);
                            //sanPham.Description = "Đang đợi nhập liệu...";
                            sanPham.Exp = reader.GetString(11);
                            sanPham.Weight = reader.GetInt32(12);
                            sanPham.Quantity = reader.GetInt32(13);
                            sanPham.Image = reader.GetString(14);
                            GameObject item;

                            if (sanPham.Size == 1)
                                item = Instantiate(item1x, FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position, FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).rotation);
                            else
                            {
                                int y = (int)FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).eulerAngles.y;
                                switch (y)
                                {
                                    case 0:
                                        item = Instantiate(item2x, new Vector3(FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.x + 0.5f, FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.y, FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.z), FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).rotation);
                                        break;
                                    case 180:
                                        item = Instantiate(item2x, new Vector3(FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.x - 0.5f, FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.y, FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.z), FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).rotation);
                                        break;
                                    case 90:
                                        item = Instantiate(item2x, new Vector3(FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.x, FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.y, FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.z - 0.5f), FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).rotation);
                                        break;
                                    case 270:
                                        item = Instantiate(item2x, new Vector3(FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.x, FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.y, FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.z + 0.5f), FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).rotation);
                                        break;
                                    case 44:
                                        item = Instantiate(item2x, new Vector3(FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.x + 0.3535f, FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.y, FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.z - 0.3535f), FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).rotation);
                                        break;
                                    case 224:
                                        item = Instantiate(item2x, new Vector3(FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.x - 0.3535f, FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.y, FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.z + 0.3535f), FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).rotation);
                                        break;
                                    default:
                                        item = Instantiate(item2x, new Vector3(FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.x + 0.5f, FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.y, FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).position.z), FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).rotation);
                                        break;
                                }
                            }
                            item.transform.eulerAngles = new Vector3(item.transform.eulerAngles.x, item.transform.eulerAngles.y, 180f);
                            FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1).GetComponent<BoxCollider>().enabled = false;
                            item.GetComponent<DragItem>().posObj = FindKe(sanPham.KeID1).transform.Search("Position " + sanPham.Pos1);
                            item.GetComponent<DragItem>().posObj.GetComponent<DropItem>().allowDrop = false;

                            FindKe(sanPham.KeID2).transform.Search("Position " + sanPham.Pos2).GetComponent<BoxCollider>().enabled = false;
                            item.GetComponent<DragItem>().posObj2 = FindKe(sanPham.KeID2).transform.Search("Position " + sanPham.Pos2);
                            item.GetComponent<DragItem>().posObj2.GetComponent<DropItem>().allowDrop = false;
                            sanPham.Obj = item;
                            item.GetComponent<DragItem>().sanPham = sanPham;
                            item.GetComponent<MeshRenderer>().material.mainTexture = LoadTexture(Application.dataPath + "/Images/Products/" + sanPham.Image);
                            listItem.Add(sanPham);
                            listItemAll.Add(sanPham);
                        }
                        else
                        {
                            SanPham sanPham = new SanPham();
                            sanPham.Id = reader.GetString(0);
                            sanPham.Name = reader.GetString(1);
                            sanPham.Type = reader.GetString(2);
                            sanPham.KeID1 = "";
                            sanPham.KeID2 = "";
                            sanPham.Size = (int)reader.GetFloat(5);
                            sanPham.Price = reader.GetInt32(6);
                            sanPham.Pos1 = 0;
                            sanPham.Pos2 = 0;
                            sanPham.Status = true;
                            sanPham.Description = reader.GetString(10);
                            //sanPham.Description = "Đang đợi nhập liệu...";
                            sanPham.Exp = reader.GetString(11);
                            sanPham.Weight = reader.GetInt32(12);
                            sanPham.Quantity = reader.GetInt32(13);
                            sanPham.Image = reader.GetString(14);
                            sanPham.Obj = null;
                            listItemWareHouse.Add(sanPham);
                            listItemAll.Add(sanPham);
                            Warehouse.AddWarehouse(sanPham);
                        }
                    }
                    else
                    {
                        SanPham sanPham = new SanPham();
                        sanPham.Id = reader.GetString(0);
                        sanPham.Name = reader.GetString(1);
                        sanPham.Type = reader.GetString(2);
                        sanPham.KeID1 = "";
                        sanPham.KeID2 = "";
                        sanPham.Size = (int)reader.GetFloat(5);
                        sanPham.Price = reader.GetInt32(6);
                        sanPham.Pos1 = 0;
                        sanPham.Pos2 = 0;
                        sanPham.Status = true;
                        sanPham.Description = reader.GetString(10);
                        //sanPham.Description = "Đang đợi nhập liệu...";
                        sanPham.Exp = reader.GetString(11);
                        sanPham.Weight = reader.GetInt32(12);
                        sanPham.Quantity = reader.GetInt32(13);
                        sanPham.Image = reader.GetString(14);
                        sanPham.Obj = null;
                        listItemWareHouse.Add(sanPham);
                        listItemAll.Add(sanPham);
                        Warehouse.AddWarehouse(sanPham);
                    }

                }
                else
                {
                    SanPham sanPham = new SanPham();
                    sanPham.Id = reader.GetString(0);
                    sanPham.Name = reader.GetString(1);
                    sanPham.Type = reader.GetString(2);
                    sanPham.KeID1 = "";
                    sanPham.KeID2 = "";
                    sanPham.Size = (int)reader.GetFloat(5);
                    sanPham.Price = reader.GetInt32(6);
                    sanPham.Pos1 = 0;
                    sanPham.Pos2 = 0;
                    sanPham.Status = true;
                    sanPham.Description = reader.GetString(10);
                    //sanPham.Description = "Đang đợi nhập liệu...";
                    sanPham.Exp = reader.GetString(11);
                    sanPham.Weight = reader.GetInt32(12);
                    sanPham.Quantity = reader.GetInt32(13);
                    sanPham.Image = reader.GetString(14);
                    sanPham.Obj = null;
                    listItemWareHouse.Add(sanPham);
                    listItemAll.Add(sanPham);
                    Warehouse.AddWarehouse(sanPham);
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

    GameObject FindParent(string name)
    {
        return GameObject.Find(name);
    }

    GameObject FindKe(string name)
    {
        foreach (Ke ke in listKe)
        {
            if (ke.Id == name)
                return ke.Obj;
        }
        return null;
    }

    public static void UpdateProductPositionToDB(string spid, string ke1, string ke2, int pos1, int pos2)
    {
        try
        {
            dbconn = new SqliteConnection(conn);
            dbconn.Open();
            string sqlQuery = "UPDATE SanPham Set KeID1='" + ke1 + "', KeID2='" + ke2 + "', ViTri1= " + pos1 + ", ViTri2=" + pos2 + ", TinhTrang='N' WHERE SanPhamID='" + spid + "'";
            dbcmd = dbconn.CreateCommand();
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteNonQuery();
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
            dbconn = null;
        }
        catch (Exception ex)
        { Debug.Log(ex); }
    }

    public static Texture LoadTexture(string filePath)
    {

        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData);
        }
        return tex;
    }
}

public static class Extensions
{
    public static Transform Search(this Transform target, string name)
    {
        if (target.name == name) return target;

        for (int i = 0; i < target.childCount; ++i)
        {
            var result = Search(target.GetChild(i), name);

            if (result != null) return result;
        }

        return null;
    }

    public static Transform Clear(this Transform transform)
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        return transform;
    }
}


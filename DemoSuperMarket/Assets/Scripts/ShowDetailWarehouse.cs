using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class ShowDetailWarehouse : MonoBehaviour
{

    public GameObject image;
    public GameObject name;
    public GameObject price;
    public GameObject type;
    public GameObject exp;
    public GameObject weight;
    public GameObject quantity;
    public GameObject size;
    public GameObject desciption;
    public GameObject detail;
    public GameObject update;
    public GameObject delete;
    public GameObject ok;

    public static GameObject imageSt;
    public static GameObject nameSt;
    public static GameObject priceSt;
    public static GameObject typeSt;
    public static GameObject expSt;
    public static GameObject weightSt;
    public static GameObject quantitySt;
    public static GameObject sizeSt;
    public static GameObject desciptionSt;
    public static GameObject detailSt;
    public static GameObject updateSt;
    public static GameObject deleteSt;
    public static GameObject okSt;

    string conn;
    IDataReader reader;
    IDbConnection dbconn;
    IDbCommand dbcmd;

    void Start()
    {
        imageSt = image;
        nameSt = name;
        priceSt = price;
        typeSt = type;
        expSt = exp;
        weightSt = weight;
        quantitySt = quantity;
        sizeSt = size;
        desciptionSt = desciption;
        detailSt = detail;
        updateSt = update;
        deleteSt = delete;
        okSt = ok;

        conn = "URI=file:" + Application.dataPath + "/dbSM.s3db";
        try
        {
            dbconn = new SqliteConnection(conn);
            dbconn.Open();
            string sqlQuery = "SELECT * FROM LoaiSanPham";
            dbcmd = dbconn.CreateCommand();
            dbcmd.CommandText = sqlQuery;
            reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                string loaiID = reader.GetString(0);
                string loaiName = reader.GetString(1);
                string loaiImage = "";
                LoaiSanPham loai = new LoaiSanPham();
                loai.Id = loaiID;
                loai.Name = loaiName;
                loai.Image = loaiImage;
                CreateObjOnMap.listType.Add(loai);
                CreateObjOnMap.listNameType.Add(loaiName);
            }
            typeSt.GetComponent<Dropdown>().AddOptions(CreateObjOnMap.listNameType);
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
            dbconn = null;
        }
        catch (Exception ex)
        {

        }
    }

    public static void ShowDetails(SanPham sanPham, bool isAdd)
    {
        detailSt.SetActive(true);
        if (isAdd)
        {
            okSt.SetActive(true);
            updateSt.SetActive(false);
            deleteSt.SetActive(false);
            nameSt.GetComponent<InputField>().text = "";
            priceSt.GetComponent<InputField>().text = "";
            desciptionSt.GetComponent<InputField>().text = "";
            typeSt.GetComponent<Dropdown>().value = 0;
            expSt.GetComponent<InputField>().text = "";
            weightSt.GetComponent<InputField>().text = "";
            quantitySt.GetComponent<InputField>().text = "";
            sizeSt.GetComponent<Toggle>().isOn = false;
        }
        else
        {
            okSt.SetActive(false);
            updateSt.SetActive(true);
            deleteSt.SetActive(true);
            nameSt.GetComponent<InputField>().text = sanPham.Name;
            priceSt.GetComponent<InputField>().text = sanPham.Price.ToString();
            desciptionSt.GetComponent<InputField>().text = sanPham.Description;
            expSt.GetComponent<InputField>().text = sanPham.Exp;
            weightSt.GetComponent<InputField>().text = sanPham.Weight.ToString();
            quantitySt.GetComponent<InputField>().text = sanPham.Quantity.ToString();
            sizeSt.GetComponent<Toggle>().isOn = (sanPham.Size == 1) ? false : true;
            imageSt.GetComponent<RawImage>().texture = ShowDetail.LoadImage(Application.dataPath + "/Images/Products/" + sanPham.Image);
            int index = 0;
            foreach (LoaiSanPham lsp in CreateObjOnMap.listType)
            {
                if (lsp.Id == sanPham.Type)
                {
                    break;
                }
                index++;
            }
            typeSt.GetComponent<Dropdown>().value = index;
        }
    }
}

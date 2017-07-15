using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Warehousing : MonoBehaviour
{

    public static SanPham sanPhamUpdate;

    string conn;
    IDataReader reader;
    IDbConnection dbconn;
    IDbCommand dbcmd;

    void Start()
    {
        conn = "URI=file:" + Application.dataPath + "/dbSM.s3db";
    }

    public void Warehousings()
    {
        ShowDetailWarehouse.ShowDetails(new SanPham(), true);
    }

    public void CreateNewProduct()
    {
        SanPham sp = new SanPham();
        sp.Id = NextID(CreateObjOnMap.listItemAll[CreateObjOnMap.listItemAll.Count - 1].Id, "SP");
        sp.Name = ShowDetailWarehouse.nameSt.GetComponent<InputField>().text;
        sp.Price = int.Parse(ShowDetailWarehouse.priceSt.GetComponent<InputField>().text);
        sp.Description = ShowDetailWarehouse.desciptionSt.GetComponent<InputField>().text;
        sp.Type = CreateObjOnMap.listType[ShowDetailWarehouse.typeSt.GetComponent<Dropdown>().value].Id;
        sp.KeID1 = "";
        sp.KeID2 = "";
        sp.Size = (ShowDetailWarehouse.sizeSt.GetComponent<Toggle>().isOn == true) ? 2 : 1;
        sp.Pos1 = 0;
        sp.Pos2 = 0;
        sp.Status = true;
        sp.Weight = int.Parse(ShowDetailWarehouse.weightSt.GetComponent<InputField>().text);
        sp.Exp = ShowDetailWarehouse.expSt.GetComponent<InputField>().text;
        sp.Quantity = int.Parse(ShowDetailWarehouse.quantitySt.GetComponent<InputField>().text);
        sp.Image = "";
        sp.Obj = null;

        try
        {
            dbconn = new SqliteConnection(conn);
            dbconn.Open();
            dbcmd = dbconn.CreateCommand();
            string sqlQuery = "INSERT INTO SanPham VALUES ('" + sp.Id + "','" + sp.Name + "','" + sp.Type + "','" + sp.KeID1 + "','" + sp.KeID2 + "'," + sp.Size + "," + sp.Price + "," + sp.Pos1 + "," + sp.Pos2 + ", 'Y','" + sp.Description + "','" + sp.Exp + "'," + sp.Weight + "," + sp.Quantity + ",'" + sp.Image + "' )";
            dbcmd = dbconn.CreateCommand();
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteNonQuery();
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
            dbconn = null;
            CreateObjOnMap.listItemWareHouse.Add(sp);
            CreateObjOnMap.listItemAll.Add(sp);
            Warehouse.ReloadWarehouse();
        }
        catch (Exception ex)
        { Debug.Log(ex); }

    }

    public void UpdateProduct()
    {
        SanPham sp = new SanPham();
        sp.Id = sanPhamUpdate.Id;
        sp.Name = ShowDetailWarehouse.nameSt.GetComponent<InputField>().text;
        sp.Price = int.Parse(ShowDetailWarehouse.priceSt.GetComponent<InputField>().text);
        sp.Description = ShowDetailWarehouse.desciptionSt.GetComponent<InputField>().text;
        sp.Type = CreateObjOnMap.listType[ShowDetailWarehouse.typeSt.GetComponent<Dropdown>().value].Id;
        sp.KeID1 = sanPhamUpdate.KeID1;
        sp.KeID2 = sanPhamUpdate.KeID2;
        sp.Size = (ShowDetailWarehouse.sizeSt.GetComponent<Toggle>().isOn == true) ? 2 : 1;
        sp.Pos1 = sanPhamUpdate.Pos1;
        sp.Pos2 = sanPhamUpdate.Pos2;
        sp.Status = true;
        sp.Weight = int.Parse(ShowDetailWarehouse.weightSt.GetComponent<InputField>().text);
        sp.Exp = ShowDetailWarehouse.expSt.GetComponent<InputField>().text;
        sp.Quantity = int.Parse(ShowDetailWarehouse.quantitySt.GetComponent<InputField>().text);
        sp.Image = "";
        sp.Obj = null;
        
        try
        {
            dbconn = new SqliteConnection(conn);
            dbconn.Open();
            dbcmd = dbconn.CreateCommand();
            string sqlQuery = "UPDATE SanPham SET TenSanPham='" + sp.Name + "', LoaiSanPham='" + sp.Type + "', KeID1='" + sp.KeID1 + "', KeID2='" + sp.KeID2 + "', KickThuoc=" + sp.Size + ", Gia=" + sp.Price + ", ViTri1=" + sp.Pos1 + ", ViTri2=" + sp.Pos2 + ", MoTa='" + sp.Description + "', HanSuDung='" + sp.Exp + "', TrongLuong=" + sp.Weight + ", SoLuong=" + sp.Quantity + ", HinhAnh='" + sp.Image + "' WHERE SanPhamID='" + sanPhamUpdate.Id + "'";
            dbcmd = dbconn.CreateCommand();
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteNonQuery();
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
            dbconn = null;
            CreateObjOnMap.listItemWareHouse.Remove(sanPhamUpdate);
            CreateObjOnMap.listItemWareHouse.Add(sp);
            CreateObjOnMap.listItemWareHouse = CreateObjOnMap.listItemWareHouse.OrderBy(o => o.Id).ToList();
            CreateObjOnMap.listItemAll.Remove(sanPhamUpdate);
            CreateObjOnMap.listItemAll.Add(sp);
            CreateObjOnMap.listItemAll = CreateObjOnMap.listItemAll.OrderBy(o => o.Id).ToList();
            Warehouse.ReloadWarehouse();
        }
        catch (Exception ex)
        { Debug.Log(ex); }

    }

    public void DeleteProduct()
    {
        try
        {
            dbconn = new SqliteConnection(conn);
            dbconn.Open();
            dbcmd = dbconn.CreateCommand();
            string sqlQuery = "DELETE FROM SanPham WHERE SanPhamID='" + sanPhamUpdate.Id + "'";
            dbcmd = dbconn.CreateCommand();
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteNonQuery();
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
            dbconn = null;
            CreateObjOnMap.listItemWareHouse.Remove(sanPhamUpdate);
            CreateObjOnMap.listItemWareHouse = CreateObjOnMap.listItemWareHouse.OrderBy(o => o.Id).ToList();
            Warehouse.ReloadWarehouse();
        }
        catch (Exception ex)
        { Debug.Log(ex); }

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

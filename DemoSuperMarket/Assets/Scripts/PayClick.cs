using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PayClick : MonoBehaviour
{
    string conn;
    IDbConnection dbconn;
    IDbCommand dbcmd;

    public void PayCart()
    {
        conn = "URI=file:" + Application.dataPath + "/dbSM.s3db";
        try
        {
            dbconn = new SqliteConnection(conn);
            dbconn.Open();
            dbcmd = dbconn.CreateCommand();
            if (Cart.gioHang.Count > 0)
            {
                foreach (SanPham sp in Cart.gioHang)
                {
                    string sqlQuery = "UPDATE SanPham SET TinhTrang = 'Y' WHERE SanPhamID = '" + sp.Id + "' ";
                    dbcmd = dbconn.CreateCommand();
                    dbcmd.CommandText = sqlQuery;
                    dbcmd.ExecuteNonQuery();
                    sp.Obj.GetComponent<DragItem>().posObj.GetComponent<BoxCollider>().enabled = true;
                    sp.Obj.GetComponent<DragItem>().posObj.GetComponent<DropItem>().allowDrop = true;
                    sp.Obj.GetComponent<DragItem>().posObj.GetChild(2).gameObject.SetActive(false);
                    if (sp.Size == 2)
                    {
                        sp.Obj.GetComponent<DragItem>().posObj2.GetComponent<BoxCollider>().enabled = true;
                        sp.Obj.GetComponent<DragItem>().posObj2.GetComponent<DropItem>().allowDrop = true;
                        sp.Obj.GetComponent<DragItem>().posObj2.GetChild(2).gameObject.SetActive(false);
                    }
                }
                Cart.gioHang.Clear();
                foreach (Transform child in Cart.cartContentStatic.transform)
                {
                    Destroy(child.gameObject);
                }
                Cart.cartContentStatic.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(Cart.cartContentStatic.transform.GetComponent<RectTransform>().sizeDelta.x, 0);

            }
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
            dbconn = null;
        }
        catch (Exception ex)
        { Debug.Log(ex); }
    }
}

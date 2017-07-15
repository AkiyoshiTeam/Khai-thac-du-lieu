using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class RotateDayKe : MonoBehaviour
{
    public static GameObject dayKe;
    public static bool choseDayKe = false;

    public GameObject[] dayKes;
    public GameObject[] kes;
    public GameObject nameTxt;

    public static GameObject nameTxtSta;

    string conn;
    IDataReader reader;
    IDbConnection dbconn;
    IDbCommand dbcmd;

    public void Start()
    {
        nameTxtSta = nameTxt;
    }

    public void Rotate()
    {
        if (choseDayKe)
            dayKe.transform.eulerAngles = new Vector3(dayKe.transform.eulerAngles.x, dayKe.transform.eulerAngles.y + 45, dayKe.transform.eulerAngles.z);
    }

    public void SaveName()
    {
        if (choseDayKe)
        {
            dayKe.GetComponent<DragDayKe>().dayKe.Name = nameTxtSta.GetComponent<InputField>().text;
            dayKe.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = nameTxtSta.GetComponent<InputField>().text;
        }
    }

    public void Save()
    {
        conn = "URI=file:" + Application.dataPath + "/dbSM.s3db";
        try
        {
            dbconn = new SqliteConnection(conn);
            dbconn.Open();
            string sqlQuery = "DELETE FROM DayKe; DELETE FROM Ke";
            dbcmd = dbconn.CreateCommand();
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteNonQuery();

            dayKes = GameObject.FindGameObjectsWithTag("DayKe");
            sqlQuery = "";
            foreach (GameObject dayKe in dayKes)
            {
                sqlQuery += " INSERT INTO DayKe VALUES ('" + dayKe.name + "', '" + dayKe.GetComponent<DragDayKe>().dayKe.Name + "' , " + Math.Round(dayKe.transform.position.x, 2) + ",  " + Math.Round(dayKe.transform.position.z, 2) + ",  " + Math.Round(dayKe.transform.eulerAngles.y, 0) + "); ";

                //Debug.Log(dayKe.name + " - " + dayKe.transform.position + " - " + Math.Round(dayKe.transform.eulerAngles.y, 0));
            }
            dbcmd = dbconn.CreateCommand();
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteNonQuery();
            Debug.Log("insert dayke success");

            kes = GameObject.FindGameObjectsWithTag("Ke");
            sqlQuery = "";
            foreach (GameObject ke in kes)
            {
                if(ke.transform.parent!=null)
                    sqlQuery += " INSERT INTO Ke VALUES ('" + ke.GetComponent<Drag>().ke.Id + "','" + ke.transform.parent.name + "', " + Math.Round(ke.transform.localPosition.x, 2) + " ); ";

                //Debug.Log(ke.GetComponent<Drag>().ke.Id + " - " + Math.Round(ke.transform.localPosition.x, 2) + " - " + ke.transform.parent.name);
            }
            dbcmd = dbconn.CreateCommand();
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteNonQuery();
            Debug.Log("insert ke success");
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
            dbconn = null;
        }
        catch (Exception ex)
        { Debug.Log(ex); }

    }


}

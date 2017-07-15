using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarehouseClick : MonoBehaviour
{

    public GameObject wareHousing;
    public GameObject scrollview;
    public static GameObject wareHousingSt;
    public static GameObject scrollviewSt;
    public static bool isWareHouse = false;

    void Start()
    {
        wareHousingSt = wareHousing;
        scrollviewSt = scrollview;
    }

    public static void WarehouseUI()
    {
        if (!CartClick.isCart)
        {
            if (!isWareHouse)
            {
                scrollviewSt.SetActive(true);
                wareHousingSt.SetActive(true);
                isWareHouse = true;
            }
            else
            {
                scrollviewSt.SetActive(false);
                wareHousingSt.SetActive(false);
                isWareHouse = false;
            }
        }
    }

    public void WarehouseShow()
    {
        WarehouseUI();
    }
}

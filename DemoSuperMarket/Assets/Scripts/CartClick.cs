using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CartClick : MonoBehaviour
{
    public GameObject sumBill;
    public GameObject scrollview;
    public static bool isCart = false;
    long bill;

    public void CartUI()
    {
        if(!isCart)
        {
            bill = 0;
            scrollview.SetActive(true);
            sumBill.SetActive(true);
            foreach(SanPham sp in Cart.gioHang)
            {
                bill += sp.Price;
            }
            sumBill.transform.GetChild(0).GetComponent<Text>().text = "Tổng cộng: " + bill + " đ";
            isCart = true;
        }
        else
        {
            scrollview.SetActive(false);
            sumBill.SetActive(false);
            isCart = false;
        }
    }

    public void SumBill()
    {
        bill = 0;
        foreach (SanPham sp in Cart.gioHang)
        {
            bill += sp.Price;
        }
        sumBill.transform.GetChild(0).GetComponent<Text>().text = "Tổng cộng: " + bill + " đ";

    }
}

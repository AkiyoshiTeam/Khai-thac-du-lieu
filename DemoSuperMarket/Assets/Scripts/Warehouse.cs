using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Warehouse : MonoBehaviour
{

    public static GameObject wareHouseItemPrefabStatic;
    public static GameObject wareHouseContentStatic;
    public GameObject wareHouseItemPrefab;
    public GameObject wareHouseContent;

    void Start()
    {
        wareHouseItemPrefabStatic = wareHouseItemPrefab;
        wareHouseContentStatic = wareHouseContent;
    }

    public static void AddWarehouse(SanPham monHang)
    {
        GameObject obj = Instantiate(wareHouseItemPrefabStatic, wareHouseContentStatic.transform);
        obj.GetComponent<CartItemClick>().sp = monHang;
        obj.transform.SetAsLastSibling();
        wareHouseContentStatic.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(wareHouseContentStatic.transform.GetComponent<RectTransform>().sizeDelta.x, CreateObjOnMap.listItemWareHouse.Count * 50 + CreateObjOnMap.listItemWareHouse.Count * 5 + 20);
        obj.transform.GetChild(0).GetComponent<RawImage>().texture = ShowDetail.LoadImage(Application.dataPath + "/Images/Products/" + monHang.Image);
        obj.transform.GetChild(1).GetComponent<Text>().text = " " + monHang.Name;
        obj.transform.GetChild(2).GetComponent<Text>().text = monHang.Price.ToString() + " đ";

    }

    public static void ReloadWarehouse()
    {
        wareHouseContentStatic.transform.Clear();
        foreach (SanPham sp in CreateObjOnMap.listItemWareHouse)
        {
            AddWarehouse(sp);
        }
    }

    //public void AddCartFromDetail()
    //{
    //    if (gioHang == null)
    //        gioHang = new List<SanPham>();
    //    gioHang.Add(sanPhamAdd);
    //    sanPhamAdd.Obj.SetActive(false);
    //    GameObject obj = Instantiate(cartItemPrefabStatic, cartContentStatic.transform);
    //    obj.GetComponent<CartItemClick>().sp = sanPhamAdd;
    //    obj.transform.SetAsLastSibling();
    //    cartContentStatic.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(cartContentStatic.transform.GetComponent<RectTransform>().sizeDelta.x, gioHang.Count * 50 + gioHang.Count * 5 + 20);
    //    obj.transform.GetChild(1).GetComponent<Text>().text = sanPhamAdd.Name;
    //    obj.transform.GetChild(2).GetComponent<Text>().text = sanPhamAdd.Price.ToString() + " đ";

    //    cartClick.SumBill();
    //}
}

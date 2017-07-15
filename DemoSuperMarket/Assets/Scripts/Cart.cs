using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cart : MonoBehaviour
{
    public static List<SanPham> gioHang;
    public static List<GameObject> gioHangUI;
    public static GameObject cartItemPrefabStatic;
    public static GameObject cartContentStatic;
    public GameObject cartItemPrefab;
    public GameObject cartContent;
    public static SanPham sanPhamAdd;
    public static SanPham sanPhamReturn;
    public CartClick cartClick;
    public static CartClick cartClickSt;
    public static Vector3 oldPosition;
    public static Vector3 oldEulerAngles;
    public static bool isReturn = false;

    void Start()
    {
        gioHang = new List<SanPham>();
        gioHangUI = new List<GameObject>();
        cartItemPrefabStatic = cartItemPrefab;
        cartContentStatic = cartContent;
        cartClickSt = cartClick;
    }

    public static void AddCart(SanPham monHang)
    {
        if (gioHang == null)
            gioHang = new List<SanPham>();
        gioHang.Add(monHang);
        monHang.Obj.SetActive(false);
        GameObject obj = Instantiate(cartItemPrefabStatic, cartContentStatic.transform);
        obj.GetComponent<CartItemClick>().sp = monHang;
        obj.transform.SetAsLastSibling();
        cartContentStatic.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(cartContentStatic.transform.GetComponent<RectTransform>().sizeDelta.x, gioHang.Count * 50 + gioHang.Count * 5 + 20);
        obj.transform.GetChild(0).GetComponent<RawImage>().texture = ShowDetail.LoadImage(Application.dataPath + "/Images/Products/" + monHang.Image);
        obj.transform.GetChild(1).GetComponent<Text>().text = " " + monHang.Name;
        obj.transform.GetChild(2).GetComponent<Text>().text = monHang.Price.ToString() + " đ";
        gioHangUI.Add(obj);

    }

    public void AddCartFromDetail()
    {
        if (gioHang == null)
            gioHang = new List<SanPham>();
        gioHang.Add(sanPhamAdd);
        sanPhamAdd.Obj.SetActive(false);
        GameObject obj = Instantiate(cartItemPrefabStatic, cartContentStatic.transform);
        obj.GetComponent<CartItemClick>().sp = sanPhamAdd;
        obj.transform.SetAsLastSibling();
        cartContentStatic.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(cartContentStatic.transform.GetComponent<RectTransform>().sizeDelta.x, gioHang.Count * 50 + gioHang.Count * 5 + 20);
        obj.transform.GetChild(0).GetComponent<RawImage>().texture = ShowDetail.LoadImage(Application.dataPath + "/Images/Products/" + sanPhamAdd.Image);
        obj.transform.GetChild(1).GetComponent<Text>().text = " " + sanPhamAdd.Name;
        obj.transform.GetChild(2).GetComponent<Text>().text = sanPhamAdd.Price.ToString() + " đ";
        gioHangUI.Add(obj);
        sanPhamAdd.Obj.GetComponent<DragItem>().posObj.GetChild(2).gameObject.SetActive(true);
        if (sanPhamAdd.Obj.tag == "Item2x")
        {
            sanPhamAdd.Obj.GetComponent<DragItem>().posObj2.GetChild(2).gameObject.SetActive(true);
        }
        cartClickSt.SumBill();
    }

    public static void ReturnFromCart()
    {
        if (gioHang == null)
            gioHang = new List<SanPham>();
        gioHang.Remove(sanPhamReturn);
        sanPhamReturn.Obj.SetActive(true);
        foreach (GameObject go in gioHangUI)
        {
            if (go.GetComponent<CartItemClick>().sp.Id == sanPhamReturn.Id)
            {
                gioHangUI.Remove(go);
                Destroy(go);
                break;
            }
        }
        cartContentStatic.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(cartContentStatic.transform.GetComponent<RectTransform>().sizeDelta.x, gioHang.Count * 50 + gioHang.Count * 5 + 20);
        sanPhamReturn.Obj.GetComponent<DragItem>().posObj.GetChild(2).gameObject.SetActive(false);
        if (sanPhamReturn.Obj.tag == "Item2x")
        {
            sanPhamReturn.Obj.GetComponent<DragItem>().posObj2.GetChild(2).gameObject.SetActive(false);
        }
        sanPhamReturn.Obj.GetComponent<MeshRenderer>().material.color = Color.white;
        cartClickSt.SumBill();
    }

    public void ReturnFromCartUI()
    {
        ReturnFromCart();
    }

}

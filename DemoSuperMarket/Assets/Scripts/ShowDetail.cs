using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ShowDetail : MonoBehaviour
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
    public GameObject add;
    public GameObject ret;

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
    public static GameObject addSt;
    public static GameObject retSt;

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
        addSt = add;
        retSt = ret;
    }

    public static void ShowDetails(SanPham sanPham, bool isAdd)
    {
        nameSt.GetComponent<Text>().text = sanPham.Name;
        priceSt.GetComponent<Text>().text = "Giá: " + sanPham.Price.ToString() + " đ";
        desciptionSt.GetComponent<Text>().text = sanPham.Description;
        expSt.GetComponent<Text>().text = "Hạn sử dụng: " + sanPham.Exp;
        weightSt.GetComponent<Text>().text = "Khối lượng: " + sanPham.Weight.ToString() + " gr";
        quantitySt.GetComponent<Text>().text = "Số lượng: " + sanPham.Weight.ToString();
        sizeSt.GetComponent<Text>().text = "Kích thước: " + sanPham.Size.ToString() + "x";
        imageSt.GetComponent<RawImage>().texture = LoadImage(Application.dataPath + "/Images/Products/" + sanPham.Image);
        foreach (LoaiSanPham lsp in CreateObjOnMap.listType)
        {
            if (lsp.Id == sanPham.Type)
            {
                typeSt.GetComponent<Text>().text = "Loại: " + lsp.Name;
                break;
            }
        }
        detailSt.SetActive(true);
        if (isAdd)
        {
            addSt.SetActive(true);
            retSt.SetActive(false);
        }
        else
        {
            retSt.SetActive(true);
            addSt.SetActive(false);
        }
    }

    public static Texture2D LoadImage(string filePath)
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
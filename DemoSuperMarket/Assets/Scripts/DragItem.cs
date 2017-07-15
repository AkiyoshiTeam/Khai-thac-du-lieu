using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DragItem : MonoBehaviour
{
    public SanPham sanPham;
    public Transform keObj;
    public Transform keObj2;
    public Transform posObj;
    public Transform posObj2;
    public static Transform dragingItem;
    public static bool isDragingItemWarehouse = false;
    public static bool isDraging = false;
    

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                Cart.AddCart(sanPham);
                posObj.GetChild(2).gameObject.SetActive(true);
                if (transform.tag == "Item2x")
                {
                    posObj2.GetChild(2).gameObject.SetActive(true);
                }
            }
            else
            {
                ShowDetail.ShowDetails(sanPham, true);
                Cart.sanPhamAdd = sanPham;
            }
        }

    }

    void OnMouseDown()
    {
        if (!isDraging)
        {
            isDragingItemWarehouse = false;
            isDraging = true;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<MeshRenderer>().material.color = Color.white;
            dragingItem = transform;
            foreach (GameObject cur in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (cur.GetComponent<NetworkIdentity>().isLocalPlayer)
                {
                    transform.SetParent(cur.transform.Find("Camera"));
                    transform.localPosition = new Vector3(0, -0.5f, 1);
                    transform.localEulerAngles = new Vector3(0, 0, 180);
                }
            }
            posObj.GetComponent<BoxCollider>().enabled = true;
            posObj.GetComponent<DropItem>().allowDrop = true;
            if (transform.tag == "Item2x")
            {
                posObj2.GetComponent<BoxCollider>().enabled = true;
                posObj2.GetComponent<DropItem>().allowDrop = true;
            }
        }
    }

    void OnMouseEnter()
    {
        if (!isDraging)
        {
            GetComponent<MeshRenderer>().material.color = Color.black;
        }
    }

    void OnMouseExit()
    {
        GetComponent<MeshRenderer>().material.color = Color.white;
    }

    public void SetPosObj(Transform pos, Transform pos2)
    {
        if(Cart.isReturn)
        {
            Cart.ReturnFromCart();
            Cart.sanPhamReturn.Obj.GetComponent<DragItem>().posObj.GetComponent<BoxCollider>().enabled = true;
            Cart.sanPhamReturn.Obj.GetComponent<DragItem>().posObj.GetComponent<DropItem>().allowDrop = true;
            if (transform.tag == "Item2x")
            {
                Cart.sanPhamReturn.Obj.GetComponent<DragItem>().posObj2.GetComponent<BoxCollider>().enabled = true;
                Cart.sanPhamReturn.Obj.GetComponent<DragItem>().posObj2.GetComponent<DropItem>().allowDrop = true;
            }
            Cart.isReturn = false;
        }
        posObj = pos;
        posObj.GetComponent<BoxCollider>().enabled = false;
        posObj.GetComponent<DropItem>().allowDrop = false;
        sanPham.KeID1 = pos.parent.parent.name;
        sanPham.Pos1 = int.Parse(pos.name.Replace("Position ", ""));
        if (transform.tag == "Item2x")
        {
            posObj2 = pos2;
            posObj2.GetComponent<BoxCollider>().enabled = false;
            posObj2.GetComponent<DropItem>().allowDrop = false;
            sanPham.KeID2 = pos2.parent.parent.name;
            sanPham.Pos2 = int.Parse(pos2.name.Replace("Position ", ""));
        }
        CreateObjOnMap.UpdateProductPositionToDB(sanPham.Id, pos.parent.parent.name, pos2.parent.parent.name, int.Parse(pos.name.Replace("Position ", "")), int.Parse(pos2.name.Replace("Position ", "")));
        Debug.Log("success");
        if(isDragingItemWarehouse)
        {
            CreateObjOnMap.listItem.Add(sanPham);
            CreateObjOnMap.listItemWareHouse.Remove(sanPham);
            Warehouse.ReloadWarehouse();
            CartItemClick.chosen = null;
        }
    }
}

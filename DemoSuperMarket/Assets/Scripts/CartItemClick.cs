using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CartItemClick : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public static Transform chosen;
    int tap;
    public SanPham sp;

    public void OnPointerClick(PointerEventData eventData)
    {
        tap = eventData.clickCount;
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (!sp.Status)
            {
                if (DragItem.dragingItem == null || DragItem.dragingItem.GetComponent<DragItem>().sanPham.Id != sp.Id)
                {
                    if (DragItem.isDraging)
                    {
                        if (DragItem.isDragingItemWarehouse)
                            Destroy(DragItem.dragingItem.gameObject);
                        else
                        {
                            DragItem.dragingItem.gameObject.SetActive(false);
                            DragItem.dragingItem.parent = null;
                            DragItem.dragingItem.position = Cart.oldPosition;
                            DragItem.dragingItem.eulerAngles = Cart.oldEulerAngles;
                        }
                        DragItem.isDragingItemWarehouse = false;
                        DragItem.isDraging = true;
                        Cart.oldPosition = sp.Obj.transform.position;
                        Cart.oldEulerAngles = sp.Obj.transform.eulerAngles;
                        Cart.sanPhamReturn = sp;
                        Cart.isReturn = true;
                        sp.Obj.transform.GetComponent<Rigidbody>().isKinematic = true;
                        sp.Obj.transform.GetComponent<MeshRenderer>().material.color = Color.white;
                        sp.Obj.SetActive(true);
                        DragItem.dragingItem = sp.Obj.transform;
                        foreach (GameObject cur in GameObject.FindGameObjectsWithTag("Player"))
                        {
                            if (cur.GetComponent<NetworkIdentity>().isLocalPlayer)
                            {
                                sp.Obj.transform.SetParent(cur.transform.Find("Camera"));
                                sp.Obj.transform.localPosition = new Vector3(0, -0.5f, 1);
                                sp.Obj.transform.localEulerAngles = new Vector3(0, 0, 180);
                            }
                        }
                        if (chosen != null)
                            chosen.GetComponent<Image>().color = new Color32(255, 199, 228, 255);
                        chosen = transform;

                    }
                    if (!DragItem.isDraging)
                    {

                        DragItem.isDragingItemWarehouse = false;
                        DragItem.isDraging = true;
                        Cart.oldPosition = sp.Obj.transform.position;
                        Cart.oldEulerAngles = sp.Obj.transform.eulerAngles;
                        Cart.sanPhamReturn = sp;
                        Cart.isReturn = true;
                        sp.Obj.transform.GetComponent<Rigidbody>().isKinematic = true;
                        sp.Obj.transform.GetComponent<MeshRenderer>().material.color = Color.white;
                        sp.Obj.SetActive(true);
                        DragItem.dragingItem = sp.Obj.transform;
                        foreach (GameObject cur in GameObject.FindGameObjectsWithTag("Player"))
                        {
                            if (cur.GetComponent<NetworkIdentity>().isLocalPlayer)
                            {
                                sp.Obj.transform.SetParent(cur.transform.Find("Camera"));
                                sp.Obj.transform.localPosition = new Vector3(0, -0.5f, 1);
                                sp.Obj.transform.localEulerAngles = new Vector3(0, 0, 180);
                            }
                        }
                        if (chosen != null)
                            chosen.GetComponent<Image>().color = new Color32(255, 199, 228, 255);
                        chosen = transform;
                    }
                }
                else
                {
                    Cart.isReturn = false;
                    DragItem.dragingItem.gameObject.SetActive(false);
                    DragItem.dragingItem.parent = null;
                    DragItem.dragingItem.position = Cart.oldPosition;
                    DragItem.dragingItem.eulerAngles = Cart.oldEulerAngles;
                    DragItem.isDragingItemWarehouse = false;
                    DragItem.isDraging = false;
                    DragItem.dragingItem = null;
                    chosen = null;
                }
            }
            else
            {
                if (GameObject.FindGameObjectWithTag("Player") != null)
                {
                    if (DragItem.dragingItem == null || DragItem.dragingItem.GetComponent<DragItem>().sanPham.Id != sp.Id)
                    {
                        if (DragItem.isDraging && DragItem.isDragingItemWarehouse)
                        {
                            Destroy(DragItem.dragingItem.gameObject);
                            DragItem.isDragingItemWarehouse = true;
                            DragItem.isDraging = true;
                            if (sp.Size == 1)
                                sp.Obj = Instantiate(CreateObjOnMap.item1xSta);
                            else
                                sp.Obj = Instantiate(CreateObjOnMap.item2xSta);

                            sp.Obj.transform.GetComponent<Rigidbody>().isKinematic = true;
                            sp.Obj.transform.GetComponent<MeshRenderer>().material.color = Color.white;
                            sp.Obj.transform.GetComponent<DragItem>().sanPham = sp;
                            sp.Obj.transform.GetComponent<MeshRenderer>().material.mainTexture = CreateObjOnMap.LoadTexture(Application.dataPath + "/Images/Products/" + sp.Image);
                            DragItem.dragingItem = sp.Obj.transform;
                            foreach (GameObject cur in GameObject.FindGameObjectsWithTag("Player"))
                            {
                                if (cur.GetComponent<NetworkIdentity>().isLocalPlayer)
                                {
                                    sp.Obj.transform.SetParent(cur.transform.Find("Camera"));
                                    sp.Obj.transform.localPosition = new Vector3(0, -0.5f, 1);
                                    sp.Obj.transform.localEulerAngles = new Vector3(0, 0, 180);
                                }
                            }
                            if (chosen != null)
                                chosen.GetComponent<Image>().color = new Color32(255, 199, 228, 255);
                            chosen = transform;

                        }
                        if (!DragItem.isDraging)
                        {
                            DragItem.isDragingItemWarehouse = true;
                            DragItem.isDraging = true;
                            if (sp.Size == 1)
                                sp.Obj = Instantiate(CreateObjOnMap.item1xSta);
                            else
                                sp.Obj = Instantiate(CreateObjOnMap.item2xSta);

                            sp.Obj.transform.GetComponent<Rigidbody>().isKinematic = true;
                            sp.Obj.transform.GetComponent<MeshRenderer>().material.color = Color.white;
                            sp.Obj.transform.GetComponent<DragItem>().sanPham = sp;
                            sp.Obj.transform.GetComponent<MeshRenderer>().material.mainTexture = CreateObjOnMap.LoadTexture(Application.dataPath + "/Images/Products/" + sp.Image);
                            DragItem.dragingItem = sp.Obj.transform;
                            foreach (GameObject cur in GameObject.FindGameObjectsWithTag("Player"))
                            {
                                if (cur.GetComponent<NetworkIdentity>().isLocalPlayer)
                                {
                                    sp.Obj.transform.SetParent(cur.transform.Find("Camera"));
                                    sp.Obj.transform.localPosition = new Vector3(0, -0.5f, 1);
                                    sp.Obj.transform.localEulerAngles = new Vector3(0, 0, 180);
                                }
                            }
                            if (chosen != null)
                                chosen.GetComponent<Image>().color = new Color32(255, 199, 228, 255);
                            chosen = transform;
                        }

                    }
                    else
                    {
                        Destroy(DragItem.dragingItem.gameObject);
                        DragItem.isDragingItemWarehouse = false;
                        DragItem.isDraging = false;
                        chosen = null;
                    }
                }
                else
                {
                    Debug.Log("null");
                }
            }
        }
        else
        {
            if (tap == 2)
            {
                if (!sp.Status)
                {
                    ShowDetail.ShowDetails(sp, false);
                    Cart.sanPhamReturn = sp;
                }
                else
                {
                    ShowDetailWarehouse.ShowDetails(sp, false);
                    Warehousing.sanPhamUpdate = sp;
                }
            }
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.GetComponent<Image>().color = new Color32(144, 218, 255, 255);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (chosen == null || chosen != transform)
        {
            transform.GetComponent<Image>().color = new Color32(255, 199, 228, 255);
        }
    }
}

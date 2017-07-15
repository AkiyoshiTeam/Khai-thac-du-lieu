using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public bool allowDrop;

    public void OnMouseUp()
    {
        if (DragItem.isDraging)
        {
            if (DragItem.dragingItem.tag == "Item1x")
            {
                DragItem.dragingItem.GetComponent<DragItem>().SetPosObj(transform, transform);
                DragItem.dragingItem.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 180f);
                DragItem.dragingItem.position = transform.position;
                DragItem.dragingItem.parent = null;
                transform.GetChild(0).gameObject.active = false;
            }
            else
            {
                switch (transform.name)
                {
                    case "Position 2":
                    case "Position 4":
                    case "Position 6":
                    case "Position 8":
                    case "Position 10":
                        if (transform.parent.parent.GetSiblingIndex() == (transform.parent.parent.parent.childCount - 1))
                            return;
                        int posInt = int.Parse(transform.name.Replace("Position ", "")) - 1;

                        if (!transform.parent.parent.parent.GetChild(transform.parent.parent.GetSiblingIndex() + 1).Search("ListItem").Search("Position " + posInt).GetComponent<DropItem>().allowDrop)
                            return;
                        DragItem.dragingItem.GetComponent<DragItem>().SetPosObj(transform, transform.parent.parent.parent.GetChild(transform.parent.parent.GetSiblingIndex() + 1).Search("ListItem").Search("Position " + posInt));
                        transform.GetChild(0).gameObject.active = false;
                        transform.parent.parent.parent.GetChild(transform.parent.parent.GetSiblingIndex() + 1).Search("ListItem").Search("Position " + posInt).GetChild(0).gameObject.active = false;
                        break;
                    case "Position 1":
                    case "Position 3":
                    case "Position 5":
                    case "Position 7":
                    case "Position 9":
                        if (!transform.parent.GetChild(transform.GetSiblingIndex() + 1).GetComponent<DropItem>().allowDrop)
                            return;
                        DragItem.dragingItem.GetComponent<DragItem>().SetPosObj(transform, transform.parent.GetChild(transform.GetSiblingIndex() + 1));
                        transform.GetChild(0).gameObject.active = false;
                        transform.parent.GetChild(transform.GetSiblingIndex() + 1).GetChild(0).gameObject.active = false;
                        break;
                }
                DragItem.dragingItem.SetParent(null);
                DragItem.dragingItem.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 180f);
                int y = (int)transform.eulerAngles.y;
                switch (y)
                {
                    case 0:
                        DragItem.dragingItem.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
                        break;
                    case 180:
                        DragItem.dragingItem.position = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
                        break;
                    case 90:
                        DragItem.dragingItem.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.5f);
                        break;
                    case 270:
                        DragItem.dragingItem.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f);
                        break;
                    case 44:
                    case 45:
                        DragItem.dragingItem.position = new Vector3(transform.position.x + 0.3535f, transform.position.y, transform.position.z - 0.3535f);
                        break;
                    case 224:
                    case 225:
                        DragItem.dragingItem.position = new Vector3(transform.position.x - 0.3535f, transform.position.y, transform.position.z + 0.3535f);
                        break;
                }
                DragItem.dragingItem.SetParent(null);
            }
            DragItem.isDraging = false;
            DragItem.dragingItem.GetComponent<Rigidbody>().isKinematic = false;
            DragItem.dragingItem.parent = null;
        }
    }

    void OnMouseEnter()
    {
        if (DragItem.isDraging)
        {
            if (DragItem.dragingItem.tag == "Item1x")
                transform.GetChild(0).gameObject.active = true;
            if (DragItem.dragingItem.tag == "Item2x")
            {
                switch (transform.name)
                {
                    case "Position 2":
                    case "Position 4":
                    case "Position 6":
                    case "Position 8":
                    case "Position 10":
                        if (transform.parent.parent.GetSiblingIndex() == (transform.parent.parent.parent.childCount - 1))
                            return;
                        int posInt = int.Parse(transform.name.Replace("Position ", "")) - 1;
                        if (!transform.parent.parent.parent.GetChild(transform.parent.parent.GetSiblingIndex() + 1).Search("ListItem").Search("Position " + posInt).GetComponent<DropItem>().allowDrop)
                            return;
                        transform.GetChild(0).gameObject.active = true;
                        transform.parent.parent.parent.GetChild(transform.parent.parent.GetSiblingIndex() + 1).Search("ListItem").Search("Position " + posInt).GetChild(0).gameObject.active = true;
                        break;
                    case "Position 1":
                    case "Position 3":
                    case "Position 5":
                    case "Position 7":
                    case "Position 9":
                        if (!transform.parent.GetChild(transform.GetSiblingIndex() + 1).GetComponent<DropItem>().allowDrop)
                            return;
                        transform.GetChild(0).gameObject.active = true;
                        transform.parent.GetChild(transform.GetSiblingIndex() + 1).GetChild(0).gameObject.active = true;
                        break;
                }
            }
        }
        else
        {
            if(!CartClick.isCart)
                transform.GetChild(1).gameObject.active = true;
        }
    }

    void OnMouseExit()
    {
        if (DragItem.isDraging)
        {
            if (DragItem.dragingItem.tag == "Item1x")
                transform.GetChild(0).gameObject.active = false;
            if (DragItem.dragingItem.tag == "Item2x")
            {
                switch (transform.name)
                {
                    case "Position 2":
                    case "Position 4":
                    case "Position 6":
                    case "Position 8":
                    case "Position 10":
                        int posInt = int.Parse(transform.name.Replace("Position ", "")) - 1;
                        transform.GetChild(0).gameObject.active = false;
                        transform.parent.parent.parent.GetChild(transform.parent.parent.GetSiblingIndex() + 1).Search("ListItem").Search("Position " + posInt).GetChild(0).gameObject.active = false;
                        break;
                    case "Position 1":
                    case "Position 3":
                    case "Position 5":
                    case "Position 7":
                    case "Position 9":
                        transform.GetChild(0).gameObject.active = false;
                        transform.parent.GetChild(transform.GetSiblingIndex() + 1).GetChild(0).gameObject.active = false;
                        break;
                }
            }
        }
        else
        {
            transform.GetChild(1).gameObject.active = false;
        }


    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            WarehouseClick.WarehouseUI();
        }

    }
}

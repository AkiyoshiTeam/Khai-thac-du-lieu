using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Class : MonoBehaviour {
    
}
public class DayKe
{
    public string Id { get; set; }
    public float X { get; set; }
    public float Z { get; set; }
    public int RotY { get; set; }
    public string Name { get; set; }
    public GameObject Obj { get; set; }
}

public class Ke
{
    public string Id { get; set; }
    public float X { get; set; }
    public GameObject Obj { get; set; }
    public string DayKeID { get; set; }
}

public class SanPham
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string KeID1 { get; set; }
    public string KeID2 { get; set; }
    public int Size { get; set; }
    public int Price { get; set; }
    public int Pos1 { get; set; }
    public int Pos2 { get; set; }
    public bool Status { get; set; }
    public string Description { get; set; }
    public string Exp { get; set; }
    public int Weight { get; set; }
    public int Quantity { get; set; }
    public string Image { get; set; }
    public GameObject Obj { get; set; }
}

public class LoaiSanPham
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
}
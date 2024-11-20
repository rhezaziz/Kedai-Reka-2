
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/Items", order = 1)]
public class Items : ScriptableObject
{
    public itemType itemType;
    public string namaItem;
    
    public int indexItem;
    

    public Vector3[] spawnPoint;

    public Sprite gambarItem;

    public bool isSpawn;
    public bool isShop;
    public bool isInventory;

    public int Harga;
    public bool Terbeli;



    //Ability
}


public enum itemType
{
    Duct_Tape,
    Tissue,
    Korek_Api,
    Kamus_Bhs_Inggris,
    Sapu,
    Lem_Kayu,
    Buku_Tulis,
    Power_Bank,
    Karet,
    Kunci,
    Meteran,
    Perkakas,
    Senter,
    Box_Alat_Tertutup,
    Box_Alat_Terbuka,
    Barbel,
    Buku_Catatan,
    Protein,
    Buku_Biologi,
    Baterai,
    Set_Makanan,
    Water_can,
    Pupuk,
    Bibit,
    Double_Tape,
    Lem_Kertas,
    Apar,
    Map_Folder,
    Kumpulan_Buku,
    Rim_Kertas,
    Spidol,
    Kalkulator,
    Masker,
    Disinfectant,
    Kamera
}

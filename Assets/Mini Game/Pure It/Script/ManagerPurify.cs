using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ManagerPurify : MonoBehaviour
{
    public int JumlahBahan;

    static public int jumlahTerisi;

    [Header("Component")]
    public FlowWater Atas;
    public FlowWater Bawah;
    public FlowWater Penampung;

    [Header("value")]
    public Vector2 PosAirAtas;
    public Vector2 PosAirTengah;
    public Vector2 PosAirBawah;
    public float zoom;

    [Header("Material")]
    public List<GameObject> Bahan;
    public GameObject placement;
    public GameObject kainDalam;
    public GameObject dalam;

    [Header("Air")]
    public Material[] AirBersih;
    public Material[] AirKotor;
    public LineRenderer AirAwal;
    public SpriteRenderer AirBaskom;
    public SpriteRenderer AirDiDalam;


    private void Awake()
    {
        //Material_Bahan[] temp = FindObjectsOfType<Material_Bahan>();
        foreach(var temp in FindObjectsOfType<Material_Bahan>())
        {
            Bahan.Add(temp.gameObject);
        }
    }
    IEnumerator Start()
    {
        if (jumlahTerisi != JumlahBahan)
        {

        }
        else
        {
            //GameEnd();
        }
        yield return null;
    }

    
    public void GameEnd(bool isCorrect)
    {
        var camera = Camera.main;
        foreach(var col in Bahan)
        {
            col.GetComponent<Collider>().enabled = false;
        }

        Material[] temp = isCorrect ? AirBersih : AirKotor;

        //AirAwal.material = temp[0];
        //AirTerisi.material = temp[1];
        //AirDiDalam.material = temp[1];
        placement.SetActive(false);
        camera.DOOrthoSize(4, 1f);
        camera.transform.DOMove(new Vector3(0, 3.75f, -10f), 1f).OnComplete(() => Atas.Begin(temp)); 
    }
}

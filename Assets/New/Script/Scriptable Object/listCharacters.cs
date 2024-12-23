using Terbaru;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CharacterInfo", order = 1)]
public class listCharacters : ScriptableObject
{

    public int index;
    public Nama nama;
    public string[] skills;
    public string[] coba = new string[]
    {
        "A",
        "B",
        "C",
    };
    public bool onComputer;
    public bool selected;
    public Vector3 Posisi;
    public GameObject objectNPC;
    public string hobbyCharacter;
    public string titleCharacter;
    public string[] skillCharacter;
    public string namaCharacter;
    [TextArea]
    public string deskripsiKarakter;
    public Sprite imageCharacter;
    public Sprite imageInfoCharacter;
    public bool isLockSkill;
    public bool characterLock;
    public string info;
    public int cost;
    public Sprite iconKarakterinKasus;

    public Sprite icon;
}

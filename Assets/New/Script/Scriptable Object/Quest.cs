using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/SideQuest", order = 1)]
public class Quest : ScriptableObject
{
    public int index;
    public int Reward;
    public Kemampuan[] skills;
   // public string[] skills;
    public string judulMisi;
    public Items item;

    [TextArea(9, 10)]
    public string Deskripsi;

    public int jmlEnergy;

    public string sceneGame;
    public bool firstChangeScene;
    public int level;
    public bool isDone;
}

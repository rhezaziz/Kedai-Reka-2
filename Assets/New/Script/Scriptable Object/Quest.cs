using System.Collections;
using System.Collections.Generic;
using Terbaru;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/SideQuest", order = 1)]
public class Quest : ScriptableObject
{
    public ID_Quiz iD_Quiz;
    public int index;
    public int Reward;
    public Kemampuan[] skills;
    public Nama[] nama;
   // public string[] skills;
    public string judulMisi;
    public Items item;

    [TextArea(9, 10)]
    public string Deskripsi;

    public int jmlEnergy;

    public string sceneGame;
    public bool firstChangeScene;
    public int level;

    public int pointBonus;
    public bool isDone;
}

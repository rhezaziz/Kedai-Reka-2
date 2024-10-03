using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/Profil", order = 1)]
public class playerProfil : ScriptableObject
{
    public string NamaKarakter;
    public Vector3 position;

    public int jmlHari;
    public int Energy;
    public int Saldo;

    public List<Items> item;
    public List<listCharacters> character;
    public List<Quest> quest;
    public int setEnergy(int jml)
    {
        if ((Energy + jml) > 3)
        {
            Energy = 3;
        }
        else
        {
            Energy += jml;
        }
        return Energy;
    }

    public int getEnergy()
    {
        return Energy;

    }

    public int getPoint()
    {
        return Saldo;
    }

    public int SetPoint(int jml)
    {

        return Saldo += jml;
    }

    public int SetHari()
    {
        return jmlHari += 1;
    }

    public int GetHari()
    {
        return jmlHari;
    }
}

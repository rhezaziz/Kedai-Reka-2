using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraksi : MonoBehaviour, interaksi_Object
{
    public Interaksi _interaksi;
    public float distance;
    public Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }
    public void action(Transform Player)
    {
        jenis_Interaksi();
    }

    public void jenis_Interaksi()
    {
        switch (_interaksi)
        {
            case Interaksi.Tanaman:
                break;

            case Interaksi.Kucing:
                break;

            case Interaksi.Masak:
                break;

            case Interaksi.Nonton:
                break;

            case Interaksi.Siram_Tanaman:
                break;
        }
    }
}

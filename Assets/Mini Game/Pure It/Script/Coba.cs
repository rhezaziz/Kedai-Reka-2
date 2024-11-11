using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CobaMateriall : MonoBehaviour
{
    public Transform Bola;

    private void Start()
    {
        coba();
    }
    void coba()
    {
        Bola.DOMoveX(3f, 1f).OnComplete(() => Naik());
    }

    void Naik()
    {
        Bola.DOMoveY(3f, 1f);
    }
}

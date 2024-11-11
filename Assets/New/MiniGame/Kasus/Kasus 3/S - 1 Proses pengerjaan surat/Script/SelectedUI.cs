using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace MiniGame3_3{
    public class SelectedUI : MonoBehaviour
    {
        public int value;
        public TMPro.TMP_Text indikator;
        public GameObject panelIndikator;
        public UnityEngine.UI.Image hasil;
        public UnityEngine.UI.Button tombol;
        public Transform panelHasil;

        public Manager manager;


        public void pilih(){
            manager.pilih(value, this.gameObject);
            tombol.enabled = false;
            panelIndikator.SetActive(true);
            indikator.text = $"{manager.index}";
             
        }

        public void hasilAkhir(Sprite temp){
            hasil.transform.localScale = Vector3.zero;
            hasil.sprite = temp;
            panelHasil.localScale = new Vector3(0f, 1f, 1f);

            panelHasil.DOScaleX(1f, 0.75f).OnComplete(()=>
                hasil.transform.DOScale(Vector3.one, .75f)
            );
        }

        public void resetValue(){
            tombol.enabled = true;
            panelIndikator.SetActive(false);
            

        }
    }
}

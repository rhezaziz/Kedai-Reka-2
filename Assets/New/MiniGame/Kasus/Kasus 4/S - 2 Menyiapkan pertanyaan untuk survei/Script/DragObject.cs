using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MiniGame4_4{
    public class DragObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler 
    {
        Vector2 posAwal;

        public float jarak;
        public TMPro.TMP_Text textPertanyaan;

        public bool value;

        public bool canMove;
        private CanvasGroup canvasGroup;
        private RectTransform rectTransform;

        public Transform Buang, Simpan;

        Vector2 tempPos;

        public void spawn(dataSoal soal){
            textPertanyaan.text = soal.Pertanyaan;
            value = soal.value;
            transform.position = posAwal;
            transform.localScale = Vector2.zero;
            transform.DOScale(Vector2.one, 1f).OnComplete(()
                => canMove = true);
            
        }
        //private CanvasGroup canvasGroup;

        private void Awake()
        {
            posAwal = transform.position;
            rectTransform = GetComponent<RectTransform>();

            canvasGroup = GetComponent<CanvasGroup>();
        }

        // Dipanggil saat drag dimulai
        public void OnBeginDrag(PointerEventData eventData)
        {
            // Membuat UI yang di-drag sedikit transparan
            canvasGroup.alpha = 0.6f;
            // Mencegah raycast selama drag
            canvasGroup.blocksRaycasts = false;
        }

        // Dipanggil saat UI sedang di-drag
        public void OnDrag(PointerEventData eventData)
        {
            // Menggerakkan posisi RectTransform berdasarkan posisi kursor
            if(!canMove)
                return;
            CloseObject();
            rectTransform.anchoredPosition += eventData.delta;
        }

        // Dipanggil saat drag selesai
        public void OnEndDrag(PointerEventData eventData)
        {
            Transform temp = CloseObject();

            // Mengembalikan transparansi UI
            canvasGroup.alpha = 1f;
            // Mengaktifkan kembali raycast
            canvasGroup.blocksRaycasts = true;

            if(temp != null){
                transform.position = temp.position;
                temp.GetComponent<CanvasGroup>().alpha = 1f;
                transform.localScale = Vector2.zero;
                FindObjectOfType<Manager>().nextPertanyaan();
                return;
            }

            transform.position = posAwal;
            
        }

        public Transform CloseObject(){
            Transform tempObj = transform.position.x - posAwal.x > 0.01f ? Buang : Simpan;

            float close = Vector2.Distance(transform.position, tempObj.position);

            if(close < jarak){
                tempObj.GetComponent<CanvasGroup>().alpha = 0.6f;
                return tempObj;
            }

            tempObj.GetComponent<CanvasGroup>().alpha = 1f;
            return null;
        }
    }
}

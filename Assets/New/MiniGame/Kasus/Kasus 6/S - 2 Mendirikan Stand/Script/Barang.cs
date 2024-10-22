using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace MiniGame6_4{
    public class Barang : MonoBehaviour
    {
        public bool active;
        public bool tutup;

        public Sprite onSelected;
        private SpriteRenderer sprite;

        public posisiItem item;
        [SerializeField] Transform locPos;
        // Start is called before the first frame update

        void Start(){
            sprite = GetComponent<SpriteRenderer>();
        }
        private void OnMouseDown()
        {

            if(!active)
                return;
            
            FindObjectOfType<Manager>().checkGame();
            StartCoroutine(animasHiddenObject());
            // bool isCorrect = FindObjectOfType<Manager>().isHiddenObject(gameObject);
            // Debug.Log("Klik");
            
            // if (tutup)
            // {
            //     StartCoroutine(pindahTutup());
            // }
            // else
            // {
            //     if (isCorrect)
            //     {
            //         // if(item != null)
            //         // {
            //         //     //item.isGet = true;
            //         // }
            //         Debug.Log("Benar");
            //         FindObjectOfType<CariBarangManager>().checkGame();
            //         StartCoroutine(animasHiddenObject());
            //     }
            //     else
            //     {
            //         Debug.Log("Salah");
            //         StartCoroutine(animasiNotHiddenObject());
            //     }
            // }
        }

        // IEnumerator pindahTutup()
        // {
        //     float DistY = transform.position.y;
        //     transform.DOLocalMoveY(DistY - 15f, 0.5f);

        //     gameObject.GetComponent<Collider2D>().enabled = false;
        //     yield return null;
        // }
        IEnumerator animasHiddenObject()
        {
            Vector2 end = locPos.localPosition;
            sprite.sprite = onSelected;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 6;
            Vector2 start = transform.localPosition;
            transform.SetParent(locPos.transform.parent);

            //transform.localPosition = Vector2.Lerp(start, end, 0.5f);
            transform.DOLocalMove(end, 0.5f);
            transform.DOLocalRotate(item.Rotation, .5f);
            transform.DOScale(item.size, .5f);
            yield return new WaitForSeconds(0.5f);

            //string namaBarang =  loctText.GetComponent<TMPro.TMP_Text>().text;

            locPos.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.SetActive(false);
            //StopCoroutine();
        }

        public Transform SetHiddenLoct(Transform value)
        {
            locPos = value;
            return locPos;
        }

        [System.Serializable]
        public class posisiItem{
            public Vector3 Rotation;
            public Vector3 size;
        }
    }
}
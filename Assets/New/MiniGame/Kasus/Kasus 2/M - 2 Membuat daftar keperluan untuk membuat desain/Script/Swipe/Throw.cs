using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace MiniGame2_2{
    public class Throw : MonoBehaviour
    {

        public int onThrow;

        SpriteRenderer sprite;
        int sortingOrder;
        [SerializeField] Vector2 startPos, endPos, direction, temp;
        [SerializeField] float touchTimeStart, touchTimeFinish, timeInterval;

        [Range(0.05f, 1f)]
        public float throwForce = 0.3f;

        public float speed, time;

        public Vector2 MaxDir, MinDir;

        public LineRenderer line;

        Vector2 tempClamp;
        bool isMove;

        bool isCorrect;

        void Start(){
            sprite = GetComponent<SpriteRenderer>();
            sortingOrder = sprite.sortingOrder;
        }


        public void initValueBarang(bool correct, Sprite sprite)
        {
            this.isCorrect = correct;
            this.GetComponent<SpriteRenderer>().sprite = sprite;
        }

        private void Update()
        {
            Vector2 pos = transform.position;
            if (pos != tempClamp)
            {
            //    isMove = true;
            }
            else
            {
                isMove = false;
            }
        }
        private void OnMouseDown()
        {
            if(!isMove)
            {
                startPos = transform.position;
            // isMove = true;
            }

        }


        private void OnMouseDrag()
        {
            if (!isMove)
            {
                var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                DrawLine(startPos, new Vector2(mousePos.x, mousePos.y));

            }

            //transform.position = new Vector2(mousePos.x, mousePos.y);
        }

        private void OnMouseUp()
        {
            if (!isMove)
            {

                isMove = true;
                sprite.sortingOrder = onThrow;
                var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //endPos = transform.position;
                endPos = new Vector2(mousePos.x, mousePos.y);
                direction = startPos - endPos;

                temp = (-direction + endPos) / 2;

                tempClamp = new Vector2(Mathf.Clamp(temp.x, MinDir.x, MaxDir.x), Mathf.Clamp(temp.y, MinDir.y, MaxDir.y));


                transform.DOMove(tempClamp, countTime(startPos, tempClamp, speed)).OnComplete(()=>{
                    sprite.sortingOrder = sortingOrder;
                });
                visibleLine();

            }
        }


        void DrawLine(Vector2 startPoint, Vector2 endPoint)
        {
            line.positionCount = 2;
            Vector3[] AllPoint = new Vector3[2];
            AllPoint[0] = new Vector3 (startPoint.x, startPoint.y, 0f);

            AllPoint[1] = new Vector3 (endPoint.x, endPoint.y, 0f);

            line.SetPositions(AllPoint);
        }
        void visibleLine()
        {
            line.positionCount = 0;
        }

        private float countTime(Vector2 awal, Vector2 akhir, float kecepatan)
        {
            Vector2 dir = akhir - awal;
            float jarak = Mathf.Sqrt(dir.x * dir.x + dir.y * dir.y);
            time = jarak / kecepatan;
            //Debug.Log(time);
            return time;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Tas"))
            {

                transform.DOMoveY(transform.localPosition.y + -2f, 0.5f);
                StartCoroutine(delay());
            }
        }

        IEnumerator delay()
        {
            SwipeManager.instance.updateJumlah(gameObject);
            //SwipeManager.instance.checkBarang(isCorrect);
            yield return new WaitForSeconds(0.5f);
            gameObject.SetActive(false);
            
            
        }
    }
}
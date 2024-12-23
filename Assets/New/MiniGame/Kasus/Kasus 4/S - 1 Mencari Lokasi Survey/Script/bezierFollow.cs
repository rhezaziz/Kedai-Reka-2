using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace MiniGame4_3{
    public class bezierFollow : MonoBehaviour
    {

        public bool loop;
        [SerializeField]
        private Transform[] routes;

        public int index;

        private int routeToGo;

        private float tParam;

        private Vector2 objectPosition;

        private float speedModifier;

        private bool coroutineAllowed = false;

        // Start is called before the first frame update
        void Start()
        {
            routeToGo = 0;
            tParam = 0f;
            speedModifier = 0.5f;
            //coroutineAllowed = true;
        }

        void OnMouseDown(){
            coroutineAllowed = true;
            Debug.Log(gameObject.name + " Klik");
            FindObjectOfType<Manager>().onClickCar(gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            if (coroutineAllowed)
            {
                StartCoroutine(GoByTheRoute(routeToGo));
            }
        }

        private IEnumerator GoByTheRoute(int routeNum)
        {
            coroutineAllowed = false;

            Vector2 p0 = routes[routeNum].GetChild(0).position;
            Vector2 p1 = routes[routeNum].GetChild(1).position;
            Vector2 p2 = routes[routeNum].GetChild(2).position;
            Vector2 p3 = routes[routeNum].GetChild(3).position;

            while(tParam < 1)
            {
                tParam += Time.deltaTime * speedModifier;

                objectPosition = Mathf.Pow(1 - tParam, 3) * p0 + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + Mathf.Pow(tParam, 3) * p3;

                transform.position = objectPosition;
                yield return new WaitForEndOfFrame();
            }

            tParam = 0f;

            routeToGo += 1;
            coroutineAllowed = loop || routeToGo < routes.Length;

            if(!coroutineAllowed)
                FindObjectOfType<Manager>().GameOver(index);
            if(routeToGo > routes.Length - 1)
            {
                routeToGo = 0;
            }

            

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tapping
{

    public class Noda : MonoBehaviour
    {
       

        [Header("Value")]
        public int tapReference;
        public int score;
        public jenisNoda jenis;
        private float diffiucltNoda = .1f;
        private float hardNoda = .1f;
        public float duration;
        public int indexNodaData;
        bool hide;
        int index;

        [Header("Component")]
        Collider2D col;
        Vector2 pos;
        SpriteRenderer sprite;
        public List<nodaValue> noda;
        public Manager manager;


        private void Start()
        {
            sprite = GetComponent<SpriteRenderer>();
            col = GetComponent<Collider2D>();
        }
        private void OnMouseDown()
        {
            //tapReference -= 1;
            tapping();
        }

        void tapping()
        {
            
            switch (jenis)
            {
                case jenisNoda.Biasa: //Noda Jenis Biasa
                    //Add Score;
                    manager.addScore(index);
                    tapReference = 0;
                    hideNoda();
                    break;

                case jenisNoda.Menengah: // Noda Jenis Sedang
                    if (tapReference > 1)
                    {
                        tapReference -= 1;
                        float temp = noda[1].tapReference;
                        float valueVisible = tapReference / temp;

                        Color tempColor = noda[1].warnaNoda;

                        sprite.color = new Color(tempColor.r, tempColor.g, tempColor.b, valueVisible);
                    }
                    else
                    {

                        manager.addScore(index);
                        hideNoda();
                    }
                    break;

                case jenisNoda.Susah: // Noda Jenis Susah
                    if(tapReference > 1)
                    {
                        tapReference -= 1;
                        float temp = noda[2].tapReference;
                        //Color tempColor = noda[2].warnaNoda;
                        float valueVisible = tapReference / temp;
                        Color tempColor = noda[2].warnaNoda;

                        sprite.color = new Color(tempColor.r, tempColor.g, tempColor.b, valueVisible);
                    }
                    else
                    {

                        manager.addScore(index);
                        hideNoda();
                    }

                    break;
            }
        }


        public void hideNoda()
        {
            sprite.color = new Color(1, 1, 1, 0);
            col.enabled = false;
            hide = false;
            StopAllCoroutines();
        }

        public void setIndex(int value)
        {
            index = value;
        }

        public void initDataNdoda()
        {
            float posX = Random.Range(-7f, 7f);
            float posY = Random.Range(-3f, 2f);

            transform.position = new Vector2(posX, posY);
            col.enabled = true;
            
            if (Random.Range(0, 1f) > diffiucltNoda)
            {
                visualNoda(0);
                jenis = jenisNoda.Biasa;
            }
            else
            {
                if (Random.Range(0, 1f) > hardNoda)
                {
                    visualNoda(1);
                    jenis = jenisNoda.Menengah;
                }
                else
                {
                    visualNoda(2);
                    jenis = jenisNoda.Susah;
                }
            }
        }

        void visualNoda(int index)
        {
            tapReference = noda[index].tapReference;
            sprite.color = noda[index].warnaNoda;
            score = noda[index].score;
            jenis = noda[index].jenis;
            indexNodaData = index;
        }
        private void SetLevel(int level)
        {
            // As level increases increse the bomb rate to 0.25 at level 10.
            diffiucltNoda = Mathf.Min(level * 0.025f, 1f);

            // Increase the amounts of HardHats until 100% at level 40.
            hardNoda = Mathf.Min(level * 0.025f, .65f);

            // Duration bounds get quicker as we progress. No cap on insanity.
            float durationMin = Mathf.Clamp(1 - level * 0.1f, 0.1f, 1f);
            float durationMax = Mathf.Clamp(2 - level * 0.1f, 0.1f, 2f);
            duration = Random.Range(durationMin, durationMax);
        }

        IEnumerator ConditionNoda()
        {
            
            
            while(hide)
            {

                float temp = noda[indexNodaData].tapReference;
                Color tempColor = noda[indexNodaData].warnaNoda;
                if (jenis == jenisNoda.Susah && tapReference == temp)
                {
                    yield return new WaitForSeconds(duration);

                    manager.descreaseTime();
                }
                else
                {
                    if (tapReference != temp)
                    {
                        yield return new WaitForSeconds(duration);

                        tapReference += 1;
                        float valueVisible = tapReference / temp;
                        
                        
                        sprite.color = new Color(tempColor.r, tempColor.g, tempColor.b, valueVisible);
                    }
                    else
                    {
                        yield return new WaitForSeconds(duration);

                        indexNodaData += 1;
                        visualNoda(indexNodaData);
                    }
                }
            }
        }

        public void Active(int level)
        {
            SetLevel(level);
            initDataNdoda();
            hide = true;
            StartCoroutine(ConditionNoda());
        }

        public void StopGame()
        {
            hideNoda();
            StopAllCoroutines();
        }
    }

    [System.Serializable]
    public class nodaValue
    {
        public int tapReference;
        public int score;
        public jenisNoda jenis;
        public Color warnaNoda;
    }
    public enum jenisNoda
    {
        Biasa,
        Menengah,
        Susah
    };
}
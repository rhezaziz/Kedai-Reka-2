using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Video;

namespace Terbaru
{
    public class ManagerPurify : MonoBehaviour
    {
        public int JumlahBahan;

        static public int jumlahTerisi;

        public DialogInGame Awal;
        public DialogInGame Akhir;

        public Dialog_Object Benar, TidakBenar;

        [Header("Component")]
        public FlowWater Atas;
        public FlowWater Bawah;
        public FlowWater Penampung;

        [Header("value")]
        public Vector2 PosAirAtas;
        public Vector2 PosAirTengah;
        public Vector2 PosAirBawah;
        public float zoom;

        [Header("Material")]
        public List<GameObject> Bahan;
        public GameObject placement;
        public GameObject kainDalam;
        public GameObject dalam;

        [Header("Air")]
        public Material[] AirBersih;
        public Material[] AirKotor;
        public LineRenderer AirAwal;
        public SpriteRenderer AirBaskom;
        public SpriteRenderer AirDiDalam;


        public void startGame()
        {
            //Material_Bahan[] temp = FindObjectsOfType<Material_Bahan>();
            foreach (var temp in FindObjectsOfType<Material_Bahan>())
            {
                temp.GetComponent<Collider>().enabled = true;
                Bahan.Add(temp.gameObject);
            }
        }
        IEnumerator Start()
        {

            yield return new WaitForSeconds(3f);
            Awal.startDialog();
        }

        bool correct;
        public void GameEnd(bool isCorrect)
        {
            Debug.Log(isCorrect);
            var camera = Camera.main;
            foreach (var col in Bahan)
            {
                col.GetComponent<Collider>().enabled = false;
            }

            Material[] temp = isCorrect ? AirBersih : AirKotor;

            //AirAwal.material = temp[0];
            //AirTerisi.material = temp[1];
            //AirDiDalam.material = temp[1];
            placement.SetActive(false);
            camera.DOOrthoSize(3, 1f);
            correct = isCorrect;
            camera.transform.DOMove(new Vector3(0, 5f, -10f), 1f).OnComplete(() => Atas.Begin(temp));
        }


        public void pourBawah()
        {
            Material[] temp = correct ? AirBersih : AirKotor;
            Dialog_Object tempDialog = correct ? Benar : TidakBenar;
            var camera = Camera.main;
            camera.transform.DOMove(new Vector3(2, 0, -10f), 1f).OnComplete(() =>
            {

                Bawah.Begin(temp);

                StartCoroutine(AkhirGame(tempDialog));
            }
            );
        }

        public VideoClip clips;
        IEnumerator AkhirGame(Dialog_Object obj)
        {
            yield return new WaitForSeconds(2f);
            Akhir.StartDialog(obj);
            Akhir.events.AddListener(() =>
            {
                FindObjectOfType<VideoManager>().actionOnDialogEnd(clips);
            });
        }
    }
}
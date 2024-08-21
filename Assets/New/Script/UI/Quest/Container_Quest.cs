using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;

namespace Terbaru{
    public class Container_Quest : MonoBehaviour
    {
        public TMP_Text judul;
        public TMP_Text Deskripsi;
        public TMP_Text Reward;
        public int index;
        public UnityEngine.UI.Button button;
        public GameObject infoQuest;

        NumberFormatInfo info = new CultureInfo("de-de", false).NumberFormat;

        public void initContent(Quest quest, int value)
        {
            index = value;
            judul.text = quest.judulMisi;
            Deskripsi.text = quest.Deskripsi;
            Reward.text = "Rp" + quest.Reward.ToString("n0", info);
            button.onClick.AddListener(() => pilihQuest(quest));
        }

        void pilihQuest(Quest quest)
        {
            infoQuest.SetActive(true);
            Container_InfoQuest initInfo = infoQuest.GetComponent<Container_InfoQuest>();

            initInfo.initKonten(quest);
        }
    }
}
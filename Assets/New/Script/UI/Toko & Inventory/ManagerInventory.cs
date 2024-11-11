using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Terbaru;

namespace Terbaru{
    public class ManagerInventory : MonoBehaviour
    {
        public GameObject prefabs;
        public Transform parent;
        public void mulai()
        {
            playerProfil profil = GameManager.instance.profil;
            if(parent.transform.childCount > 1)
            {
                lihatItem(profil);
            }
            else
            {
                initInventory(profil);
            }
        }
        List<GameObject> container = new List<GameObject>();
        void initInventory(playerProfil profil)
        {
            container.Clear();
            //GameObject item;
            foreach(var item in profil.item){
                GameObject temp = Instantiate(prefabs);
                temp.name = item.namaItem;
                temp.GetComponent<ContainerItem>().ItemInInventory(item);
                temp.transform.SetParent(parent);
                temp.transform.localScale = new Vector2(1f, 1f);
                temp.transform.Rotate(0f, 0f, -90, Space.Self);
                container.Add(temp);
            }
        }

        void lihatItem(playerProfil profil)
        {
            for(int i = 0; i < container.Count; i++){
                Items item = profil.item[i];

                if(item.isInventory){
                    container[i].SetActive(true);
                }else{
                    container[i].SetActive(false);
                }
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClosePanel : MonoBehaviour
{
    public List<GameObject> panels = new List<GameObject>();
    public GameObject pausePanel;

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(panels.Count <= 0){
                bukaScene(pausePanel);
            }
            closePanel();
        }
    }
    public void bukaScene(GameObject panel){
        panel.SetActive(true);
        panels.Add(panel);
    }

    void closePanel(){
        int index = panels.Count - 1;
        panels[index].SetActive(false);
        panels.RemoveAt(index);
    }
}

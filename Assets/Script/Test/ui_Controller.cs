using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ui_Controller : MonoBehaviour
{
    public static ui_Controller instance;
    public TMPro.TMP_Text currentName;
    public List<ListButton> buttons = new List<ListButton>();

    public List<viewAnimated> contents = new List<viewAnimated>();
    public GameObject Kucing;
    public GameObject[] gameObjects;
    int index = 0;

    public LineRenderer line;
    public Slider Slider;
    public Slider speedAnim;
    Camera cam;


    public Vector3 defaultPosition;
    public Vector3 reviewPosition;
    public Vector3 rotCamReview;

    public GameObject twoD, threeD;
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        Slider.value = line.startWidth;
        currentContent().player.animatedSpeed(speedAnim.value);
    }

    public void valueLine()
    {
        line.startWidth = Slider.value;
        line.endWidth = Slider.value;
    }



    public void changeBackground(Button button)
    {
        bool active = twoD.activeInHierarchy;
        button.GetComponentInChildren<TMP_Text>().text = active ? "3D Ground" : "2D Ground";
        twoD.SetActive(!active);
        threeD.SetActive(active);
    }

    private void Start()
    {
        cam = Camera.main;
        defaultPosition = cam.transform.position;
        initialContent();
    }
    public void AllButtons(bool value)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            Button button = buttons[i].button.GetComponentInChildren<Button>();

            button.interactable = value;
        }
    }
    
    public void checkFoot(Button button)
    {
        bool review = cam.transform.position == reviewPosition;
        cam.transform.position = review ? defaultPosition : reviewPosition;
        cam.transform.rotation = review ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(rotCamReview);
        button.GetComponentInChildren<TMP_Text>().text = review ? "Camera Default" : "Camera Review";
    }
    
    viewAnimated currentContent()
    {
        return contents[currentIndex];
    }

    int currentIndex
    {
        get
        {
            return index;
        }
        set
        {
            index = value;
            if(index > contents.Count - 1)
            {
                index = 0;
            }
            else if (index < 0)
            {
                index = contents.Count - 1;
            }
        }
    }

    public void changeContent(int value)
    {
        enabledButton();
        currentContent().player.gameObject.SetActive(false);
        Kucing.SetActive(false);
        currentIndex += value;
        initialContent();
    }

    void enabledButton()
    {
        foreach (var button in buttons)
        {
            button.button.SetActive(false);
        }
    }

    void initialContent()
    {
        currentName.text = currentContent().Nama;
        //gameObjects[currentIndex].gameObject.SetActive(true);
        currentContent().Object.SetActive(true);
        if (GameObject.Find("Laki Utama"))
            Kucing.SetActive(true);

        if(currentContent().indexButton.Count != 0)
        {

            int indexButton = 0;
            foreach (var button in buttons)
            {
                if (currentContent().indexButton.Contains(indexButton))
                {

                    Button tempButton = button.button.GetComponentInChildren<Button>();
                    button.button.SetActive(true);
                    tempButton.onClick.RemoveAllListeners();
                    if (button.isBool)
                    {

                        tempButton.onClick.AddListener(() =>
                        {
                            currentContent().player.animatedBool(tempButton, button.Text,
                                button.variabel, button.tempState);
                        });
                    }
                    else
                    {

                        tempButton.onClick.AddListener(() =>
                        {
                            currentContent().player.animatedTrigger(button.variabel);
                        });

                        if (button.variabel.Contains("Elus"))
                        {

                            Controller kucing = Kucing.GetComponent<Controller>();
                            
                            tempButton.onClick.AddListener(() =>
                            {
                                kucing.animatedTrigger(button.variabel);
                            });
                        }
                    }
                }

                indexButton += 1;
            }
        }
    }
}

[System.Serializable]
public class viewAnimated
{
    public string Nama;
    public Controller player;
    public GameObject Object;
    public List<int> indexButton;
    public List<ListButton> buttons = new List<ListButton>();

}

[System.Serializable]
public class ListButton
{
    public bool isBool;
    public GameObject button;
    public string variabel;
    public string Text;
    public state tempState;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [Header("Condition")]
    public bool canMove;
    public bool MoveVertical;
    public bool canFlip;

    //Atribut
    Animator anim;
    SpriteRenderer viewPlayer;
    Vector3 Coordinate;
    bool flip;
    public LineRenderer line;
    BoxCollider box;

    public state currentState;
    Camera cam;


    float duration;
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        viewPlayer = GetComponentInChildren<SpriteRenderer>();
        box = GetComponent<BoxCollider>();
        cam = Camera.main;
        flip = viewPlayer.flipX;
    }

    private void Update()
    {
        if (canMove)
        {
            movePlayer();
        }
        if (line)
        {
            showCollider();
        }
    }

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        if (!line)
            return;

        for(int i = 0; i < line.positionCount; i++)
        {
            line.SetPosition(i, Vector3.zero);
        }
    }

    public void animatedSpeed(float value)
    {
        anim.speed = value * 2;
    }

    void showCollider()
    {
        Vector3[] positions = new Vector3[4];
        float posY =  box.size.y / 2.0f ;
        float posX = box.size.x / 2.0f;
        positions[0] = transform.TransformPoint(new Vector3(posX, posY +box.center.y, 0));
        positions[1] = transform.TransformPoint(new Vector3(-posX, posY + box.center.y, 0));
        positions[2] = transform.TransformPoint(new Vector3(-posX, -posY + box.center.y, 0));
        positions[3] = transform.TransformPoint(new Vector3(posX, -posY + box.center.y, 0));
        line.SetPositions(positions);
    }

    public void animatedTrigger(string variabel)
    {
        Debug.Log(variabel);
        anim.SetTrigger(variabel);
    }

    public void animatedBool(UnityEngine.UI.Button button, string Text, string variabel, state tempState)
    {
        bool siram = currentState == state.Idle ? true : false;
        currentState = siram ? tempState : state.Idle;

        anim.SetBool(variabel, siram);
        ui_Controller.instance.AllButtons(!siram);
        button.interactable = true;
        button.GetComponentInChildren<TMPro.TMP_Text>().text = $"{Text} ({kondisi(siram)})";
    }

    string kondisi(bool value)
    {
        return value ? "ON" : "OFF";
    }

    void movePlayer()
    {
        Coordinate.x = Input.GetAxisRaw("Horizontal");
        Coordinate.z = MoveVertical ? Input.GetAxisRaw("Vertical") : 0;

        if (Coordinate.x != 0 || Coordinate.z != 0)
        {
            //duration = 0f;
            //anim.SetBool("Kegiatan", false);
            if (canMove)
            {
                anim.SetBool("Jalan", true);
                anim.SetFloat("Xinput", Coordinate.x);

                if(MoveVertical) anim.SetFloat("Yinput", Coordinate.z);


                if (canFlip)
                {
                    if (Coordinate.x > 0)
                    {
                        viewPlayer.flipX = true;
                    }
                    else if (Coordinate.x < 0)
                    {
                        viewPlayer.flipX = false;
                    }
                }
            }
        }
        else if (Coordinate.x == 0 && Coordinate.z == 0)
        {
            anim.SetBool("Jalan", false);

           // duration += Time.deltaTime;

            //if(duration >= 10f)
           // {
           //     anim.SetBool("Kegiatan", true);
           // }
        }
    }
}

public enum state
{
    Idle,
    Siram,
    Tanaman,
    Elus,
    Walk,
    Aktivitas,
    Ngobrol,
    Buka_Gorden,
    Buka_Lemari,
    Ambil,
    Duduk,
    Klik,
    Masak,
    Nonton
}

    
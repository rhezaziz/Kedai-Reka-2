using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Terbaru;
public class Ngobrol : MonoBehaviour, interaksi_Object
{
    public Konten_Dialog dialog;

    private Queue<Dialog> sentences;
    public List<Mimik> mimik;
    public List<Mimik> mimikPlayer;
    public TMP_Text dialogText;
    public float distance;
    bool isDialog = false;
    // public Dialog cakap;


    private Animator animator;

    void Start()
    {

        sentences = new Queue<Dialog>();

    }

    private void Update()
    {
        if (isDialog && Input.GetKeyDown(KeyCode.N))
            DisplayNextDialog();
    }

    public void action(Transform player)
    {
        int temp = transform.position.x - player.position.x > 0 ? -1 : 1;
        float xDir = player.position.x + (distance * temp);
        float zDir = transform.position.z;
        player.transform.position = new Vector3(xDir, player.position.y, zDir);
        player.GetComponentInChildren<SpriteRenderer>().flipX = temp > 0 ? false : true;
        GetComponentInChildren<SpriteRenderer>().flipX = temp > 0 ? false : true;
        mimikPlayer = new List<Mimik>();
        mimikPlayer = player.GetComponent<Terbaru.Controller>().mimik;
        InteraksiPlayer.Interaksi = true;
        UIManager.Instance.parentDialog.SetActive(true);
        isDialog = true;
        dialogText = UIManager.Instance.text_Dialog;

        GetComponent<NPC_Controller>().currentCondition(animasi.Ngobrol);
        animator = player.GetComponentInChildren<Animator>();
        
        
        StartDialog();
        
    }

    public void StartDialog()
    {

        animator.SetBool("Ngomong", true);
        animator.SetTrigger("Ngobrol");

        sentences.Clear();

        foreach (var sentence in dialog.dialog)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextDialog();
    }


    public void DisplayNextDialog()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }
            
        var sentence = sentences.Dequeue();


        UIManager.Instance.initDialog(dialog.Nama, currentWajah(sentence), sentence.isPlayer);

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence.sentence));
    }

    Sprite currentWajah(Dialog temp)
    {
        List<Mimik> _mimik = new List<Mimik>();
        _mimik = temp.isPlayer ? animator.GetComponentInParent<Terbaru.Controller>().mimik : mimik;
        
        foreach(var wajah in _mimik)
        {
            if(wajah.ekspresi == temp.currentEkspresi)
            {
                return wajah.Sprite;
            }
        }
        Debug.Log("null");
        return null;
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return null;
        }
    }

    public void EndDialog()
    {
        animator.SetBool("Ngomong", false);
        InteraksiPlayer.Interaksi = false;
        GetComponent<NPC_Controller>().currentCondition(animasi.Idle);
        UIManager.Instance.parentDialog.SetActive(false);
    }
}
[System.Serializable]
public class Konten_Dialog
{
    
    public string Nama;
    public List<Dialog> dialog;
   
}

[System.Serializable]
public class Dialog
{
    [TextArea(5, 10)]
    public string sentence;
    public Ekspresi currentEkspresi;
    public bool isPlayer;
}

[System.Serializable]
public class Mimik
{
    public Sprite Sprite;
    public Ekspresi ekspresi;
}

public enum Ekspresi
{
    Marah,
    Bingung,
    Normal,
    Sedih,
    Senang
}
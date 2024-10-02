using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public string scene;
    [SerializeField] private float Timer = 3f;

    IEnumerator startCount()
    {
        while(Timer > 0)
        {
            yield return null;
        }
    }

    private void OnEnable()
    {
        
    }
    public void pindahScene()
    {
        FindObjectOfType<Terbaru.MiniGame>().pindah("Asrama BackUp");
    }
}

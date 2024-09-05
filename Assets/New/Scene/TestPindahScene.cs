using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TestPindahScene : MonoBehaviour
{


    public string namaScene;
    public void test(){
       // string temp = scene.name;
        SceneManager.LoadScene(namaScene, LoadSceneMode.Additive);
    }

    public void pindah(){

        SceneManager.UnloadSceneAsync(namaScene);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load_scene : MonoBehaviour {

    public void LoadFreePresident_Scene()
    {
        SceneManager.LoadScene("free_president");
    }
    public void LoadMain_Scene()
    {
        SceneManager.LoadScene("scene_main");
    }
}

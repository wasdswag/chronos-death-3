using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int nextSceneIndex;

    void Start()
    {
        nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            LoadNextScene();
       
    }

    public void LoadNextScene()
    {
        Application.OpenURL("http://repetitions.xyz/");
    }


}

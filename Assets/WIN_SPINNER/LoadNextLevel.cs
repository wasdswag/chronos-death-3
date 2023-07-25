using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class LoadNextLevel : MonoBehaviour
{

    [FormerlySerializedAs("LevelIndex")] public int levelIndex;
    private static LoadNextLevel _instance;

    private void Awake()
    {
        _instance = this;
    }

   

    public static void Go()
    {
        SceneManager.LoadScene(_instance.levelIndex);
    }
}

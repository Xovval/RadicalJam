using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneService : MonoBehaviour
{
    
    public void LoadMainScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }

    public void LoadLoginScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Login");
    }

    public void LoadIntroScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Intro");
    }
    
    public void LoadFightScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Fight");
    }
    
    
}

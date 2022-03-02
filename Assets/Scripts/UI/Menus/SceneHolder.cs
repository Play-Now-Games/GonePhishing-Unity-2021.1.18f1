using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHolder : MonoBehaviour
{
    public string levelName;

    public void GoToScene()
    {
        SceneManager.LoadScene(levelName);
    }
}

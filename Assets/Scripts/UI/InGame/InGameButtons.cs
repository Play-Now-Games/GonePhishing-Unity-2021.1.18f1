using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameButtons : MonoBehaviour
{
    public string levelName;

    public string nextLevel;

    public void PlayAgain()
    {
        SceneManager.LoadScene(levelName);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class endScreen : MonoBehaviour
{
    public ScoreSystem scoreSystem;
    public string[] clip;
    private VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += ReturnToMainMenu;
    }

    public void PlayEndVideo()
    {

        switch (scoreSystem.Stars)
        {
            case 0:
                NoStarts();
                break;
            case 1:
                OneStarts();
                break;
            case 2:
                TwoStarts();
                break;
            case 3:
                ThreeStarts();
                break;
        }
    }

    void ReturnToMainMenu(UnityEngine.Video.VideoPlayer vp)
    {
        SceneManager.LoadScene("InitialMenu");
    }

    void NoStarts()
    {
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, clip[0]);
        videoPlayer.Play();
    }

    void OneStarts()
    {
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, clip[1]);
        videoPlayer.Play();
    }

    void TwoStarts()
    {
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, clip[2]);
        videoPlayer.Play();
    }


    void ThreeStarts()
    {
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, clip[3]);
        videoPlayer.Play();
    }

}

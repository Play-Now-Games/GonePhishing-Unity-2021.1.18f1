using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class endScreen : MonoBehaviour
{
    public RawImage videoTexture;
    public DayTimer timeScript;
    public ScoreSystem scoreSystem;
    public VideoClip[] clip;
    public GameObject canvas;
    private VideoPlayer videoPlayer;
    public string thisScene;
    public string NextScene;
    

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += ReturnToMainMenu;
    }

    public void PlayEndVideo()
    {
        canvas.SetActive(true);

        Color AlphaText = videoTexture.color;
        AlphaText.a = 1;
        videoTexture.color = AlphaText;

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
        videoPlayer.clip = clip[0];
        videoPlayer.Play();
    }

    void OneStarts()
    {
        videoPlayer.clip = clip[1];
        videoPlayer.Play();
    }

    void TwoStarts()
    {
        videoPlayer.clip = clip[2];
        videoPlayer.Play();
    }


    void ThreeStarts()
    {
        videoPlayer.clip = clip[3];
        videoPlayer.Play();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : Interactable
{

    public FirstPersonController Player;
    public float CameraOffset;

    // Start is called before the first frame update
    void Start()
    {

        if (Player == null)
        {
            try
            {
                Player = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
            }
            catch
            {
                Debug.LogError("No player object found!");
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnClick()
    {
        print("Computer was clicked");
        Player.PointCameraAt(transform, CameraOffset);
    }

}

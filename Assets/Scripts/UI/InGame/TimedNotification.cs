using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedNotification : MonoBehaviour
{
    // Start is called before the first frame update

    public float CurrentTime = 0.0f;
    public float TimeLimit;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CurrentTime += Time.deltaTime;
        if (CurrentTime >= TimeLimit)
        {
            Destroy(gameObject);
        }
    }
}

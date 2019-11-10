using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Screen.fullScreen = true;
        if (Screen.fullScreen)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

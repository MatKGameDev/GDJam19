using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMenuController : MonoBehaviour
{
    string aPress = "P1A";
    public GameObject MenuPanel;
    public GameObject HelpPanel;
    bool firstFrame = true;
    // Update is called once per frame
    void Update()
    {
        if (!firstFrame)
        {
            if (Input.GetButtonUp(aPress))
            {
                MenuPanel.SetActive(true);
                HelpPanel.SetActive(false);
            }
        }
        firstFrame = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject HelpPanel;

    // Start is called before the first frame update
    void Start()
    {
        MenuPanel.SetActive(true);
        HelpPanel.SetActive(false);
    }

    public void SetMenuPanel()
    {
        MenuPanel.SetActive(true);
        HelpPanel.SetActive(false);
    }
    public void SetHelpPanel()
    {
        MenuPanel.SetActive(false);
        HelpPanel.SetActive(true);
    }

}

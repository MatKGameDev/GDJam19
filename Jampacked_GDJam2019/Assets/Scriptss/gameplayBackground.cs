using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class gameplayBackground : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("Gameplay", LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

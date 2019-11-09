using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField] private SpriteRenderer P1ButtonPrompt;
    [SerializeField] private SpriteRenderer P2ButtonPrompt;
    [SerializeField] private SpriteRenderer P3ButtonPrompt;
    [SerializeField] private SpriteRenderer P4ButtonPrompt;

    private bool P1Joined = false;
    private bool P2Joined = false;

    public static Animator[] blobList;

    // Start is called before the first frame update
    void Start()
    {
        blobList = Resources.LoadAll<Animator>("Prefabs");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("P1A"))
        {
            P1ButtonPrompt.enabled = false;
            P1Joined = true;
        }

        if (Input.GetButtonDown("P2A"))
        {
            P2ButtonPrompt.enabled = false;
            P2Joined = true;
        }
    }
}

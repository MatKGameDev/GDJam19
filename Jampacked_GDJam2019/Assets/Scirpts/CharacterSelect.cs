using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    enum PlayerReadyState
    {
        notJoined,
        joined,
        ready
    }

    private PlayerReadyState[] playersStates;

    [SerializeField] private SpriteRenderer[] buttonPrompts;
    [SerializeField] private SpriteRenderer[] characterSelectPrompts;

    public GameObject[] playerCharacterSelections;

    public static GameObject[] blobList;

    // Start is called before the first frame update
    void Start()
    {
        blobList = Resources.LoadAll<GameObject>("Prefabs");

        playersStates = new PlayerReadyState[4];

        for (int i = 0; i < playersStates.Length; i++)
        {
            playersStates[i] = PlayerReadyState.notJoined;
            characterSelectPrompts[i].enabled = false;
        }

        playerCharacterSelections = new GameObject[4];
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < playersStates.Length; i++)
        {
            //get correct player number on the buttons
            string AButton = "P" + (i + 1).ToString() + "A";
            string BButton = "P" + (i + 1).ToString() + "B";

            if (Input.GetButtonDown(AButton) && playersStates[i] == PlayerReadyState.notJoined)
            {
                buttonPrompts[i].enabled = false;
                characterSelectPrompts[i].enabled = true;
                playersStates[i] = PlayerReadyState.joined;


                playerCharacterSelections[i] = Instantiate(blobList[0]);
                Destroy(playerCharacterSelections[i].GetComponent<TestBlobMove>());

                playerCharacterSelections[i].transform.position += new Vector3(-9.0f + (2 * i + 1) * 18f / 8f, -1.5f);
                //playerCharacterSelections[i].transform.position += new Vector3(-9.0f + (i * 6.0f), -1.5f);
            }

            if (Input.GetButtonDown(BButton) && playersStates[i] == PlayerReadyState.joined)
            {
                characterSelectPrompts[i].enabled = false;
                buttonPrompts[i].enabled = true;
                playersStates[i] = PlayerReadyState.notJoined;


                Destroy(playerCharacterSelections[i]);
            }
        }
    }
}

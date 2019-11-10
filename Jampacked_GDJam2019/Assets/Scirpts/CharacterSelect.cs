using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    enum PlayerReadyState
    {
        notJoined,
        joined,
        ready
    }

    private PlayerReadyState[] playersStates;

    [SerializeField] private SpriteRenderer[] inactiveWindows;
    [SerializeField] private SpriteRenderer[] buttonPrompts;
    [SerializeField] private SpriteRenderer[] characterSelectPrompts;
    [SerializeField] private SpriteRenderer[] playerTags;
    [SerializeField] private GameObject  pressStartPrompt;

    public GameObject[] playerCharacterSelections;

    private int[] currentBlobIndexSelected;
    public static GameObject[] blobList;

    private float[] controlStickTimers;
    private float controlStickResetTime = 0.4f;

    public GameObject giftPrefab;

    // Start is called before the first frame update
    void Start()
    {

        blobList = Resources.LoadAll<GameObject>("Prefabs");

        playersStates = new PlayerReadyState[4];
        currentBlobIndexSelected = new int[4];

        controlStickTimers = new float[4];


        for (int i = 0; i < playersStates.Length; i++)
        {
            playersStates[i] = PlayerReadyState.notJoined;
            characterSelectPrompts[i].enabled = false;
            playerTags[i].enabled = false;
            currentBlobIndexSelected[i] = -1;
            controlStickTimers[i] = 0.0f;
        }

        playerCharacterSelections = new GameObject[4];
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < playersStates.Length; i++)
        {
            controlStickTimers[i] += Time.deltaTime;

            //get correct player number on the buttons
            string AButtonName = "P" + (i + 1).ToString() + "A";
            string BButtonName = "P" + (i + 1).ToString() + "B";
            string StickVerticalName = "P" + (i + 1).ToString() + "Vertical";
            string StickHorizontalName = "P" + (i + 1).ToString() + "Horizontal";

            //this is jank but basically it automatically reactivates the control stick selection when the stick is at rest
            if ((Input.GetAxis(StickHorizontalName) < 0.5f && (Input.GetAxis(StickHorizontalName) > -0.5f)))
                controlStickTimers[i] = 1.0f;

            if (Input.GetButtonDown(AButtonName))
            {
                if (playersStates[i] == PlayerReadyState.notJoined)
                {
                    playersStates[i] = PlayerReadyState.joined;

                    inactiveWindows[i].enabled = false;
                    characterSelectPrompts[i].enabled = true;
                    characterSelectPrompts[i].GetComponent<Animator>().SetBool("isMoving", false);
                    playerTags[i].enabled = true;
                    buttonPrompts[i].enabled = false;

                    bool isValid = false;
                    int selectedIndex = currentBlobIndexSelected[i];
                    while (!isValid)
                    {
                        isValid = true;

                        selectedIndex++;
                        if (selectedIndex >= blobList.Length)
                            selectedIndex = 0;

                        for (int j = 0; j < currentBlobIndexSelected.Length; j++)
                        {
                            if (currentBlobIndexSelected[j] == selectedIndex)
                                isValid = false;
                        }
                    }

                    currentBlobIndexSelected[i] = selectedIndex;

                    playerCharacterSelections[i] = Instantiate(blobList[currentBlobIndexSelected[i]]);
                    Destroy(playerCharacterSelections[i].GetComponent<TestBlobMove>());

                    playerCharacterSelections[i].transform.position +=
                        new Vector3(-9.0f + (2.0f * i + 0.95f) * 18.0f / 8.0f, 0.3f);
                }
                else if (playersStates[i] == PlayerReadyState.joined)
                {
                    playersStates[i] = PlayerReadyState.ready;

                    characterSelectPrompts[i].enabled = false;
                }
            }

            else if (Input.GetButtonDown(BButtonName))
            {
                if (playersStates[i] == PlayerReadyState.joined)
                {
                    playersStates[i] = PlayerReadyState.notJoined;

                    currentBlobIndexSelected[i] = -1;

                    characterSelectPrompts[i].enabled = false;
                    characterSelectPrompts[i].GetComponent<Animator>().SetBool("isMoving", true);
                    playerTags[i].enabled = false;
                    inactiveWindows[i].enabled = true;
                    buttonPrompts[i].enabled = true;

                    Destroy(playerCharacterSelections[i]);
                }
                else if (playersStates[i] == PlayerReadyState.ready)
                {
                    playersStates[i] = PlayerReadyState.joined;

                    characterSelectPrompts[i].enabled = true;
                }
            }

            if (Input.GetAxis(StickHorizontalName) > 0.5f && playersStates[i] == PlayerReadyState.joined &&
                controlStickTimers[i] > controlStickResetTime)
            {
                controlStickTimers[i] = 0.0f;

                Destroy(playerCharacterSelections[i]);

                bool isValid = false;
                int selectedIndex = currentBlobIndexSelected[i];
                while (!isValid)
                {
                    isValid = true;

                    selectedIndex++;
                    if (selectedIndex >= blobList.Length)
                        selectedIndex = 0;

                    for (int j = 0; j < currentBlobIndexSelected.Length; j++)
                    {
                        if (currentBlobIndexSelected[j] == selectedIndex)
                            isValid = false;
                    }
                }

                currentBlobIndexSelected[i] = selectedIndex;

                playerCharacterSelections[i] = Instantiate(blobList[currentBlobIndexSelected[i]]);
                Destroy(playerCharacterSelections[i].GetComponent<TestBlobMove>());
                playerCharacterSelections[i].transform.position += new Vector3(-9.0f + (2.0f * i + 0.95f) * 18.0f / 8.0f, 0.3f);
                playerCharacterSelections[i].transform.localScale = new Vector3(10.0f, 10.0f, 1.0f);
            }
            else if (Input.GetAxis(StickHorizontalName) < -0.5f && playersStates[i] == PlayerReadyState.joined &&
                     controlStickTimers[i] > controlStickResetTime)
            {
                characterSelectPrompts[i].GetComponent<Animator>().SetBool("isShiftLeft", true);
                controlStickTimers[i] = 0.0f;

                Destroy(playerCharacterSelections[i]);

                bool isValid = false;
                int selectedIndex = currentBlobIndexSelected[i];
                while (!isValid)
                {
                    isValid = true;

                    selectedIndex--;
                    if (selectedIndex < 0)
                        selectedIndex = blobList.Length - 1;

                    for (int j = 0; j < currentBlobIndexSelected.Length; j++)
                    {
                        if (currentBlobIndexSelected[j] == selectedIndex)
                            isValid = false;
                    }
                }

                currentBlobIndexSelected[i] = selectedIndex;

                playerCharacterSelections[i] = Instantiate(blobList[currentBlobIndexSelected[i]]);
                Destroy(playerCharacterSelections[i].GetComponent<TestBlobMove>());
                playerCharacterSelections[i].transform.position += new Vector3(-9.0f + (2.0f * i + 0.95f) * 18.0f / 8.0f, 0.3f);
            }
        }

        int numPlayersReady = 0;
        for (int i = 0; i < playersStates.Length; i++)
        {
            if (playersStates[i] == PlayerReadyState.ready)
                numPlayersReady++;
        }
        //check if the game should start (start button is pressed and at least two players are ready)
        if (numPlayersReady >= 2)
        {
            bool isAnyoneNotReady = false;
            for (int i = 0; i < playersStates.Length; i++)
            {
                if (playersStates[i] == PlayerReadyState.joined)
                    isAnyoneNotReady = true;
            }

            if (!isAnyoneNotReady)
                pressStartPrompt.GetComponent<SpriteRenderer>().enabled = true;

            else
                pressStartPrompt.GetComponent<SpriteRenderer>().enabled = false;


            if (Input.GetButtonDown("Start") && !isAnyoneNotReady)
            {
                for (int i = 0; i < playersStates.Length; i++)
                {
                    if (playersStates[i] == PlayerReadyState.ready)
                    {
                        playerCharacterSelections[i].AddComponent<TestBlobMove>();
                        playerCharacterSelections[i].GetComponent<TestBlobMove>().playerNum = i;
                        playerCharacterSelections[i].GetComponent<TestBlobMove>().gift = giftPrefab;
                        playerCharacterSelections[i].GetComponent<TestBlobMove>().gift.transform.localScale = new Vector3(0.3f, 0.3f, 1.0f);
                        DontDestroyOnLoad(playerCharacterSelections[i]);
                    }
                }
                SceneManager.LoadScene("Gameplay");
            }
        }
        else
                pressStartPrompt.GetComponent<SpriteRenderer>().enabled = false;
    }
}

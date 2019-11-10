using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameplay : MonoBehaviour
{
    [SerializeField] private SpriteRenderer winnerText;

    public float roundLength;
    private float bombTimer;

    private bool isRoundStart = true;

    private float sceneTransitionTimer = 5.0f;
    private bool isGameFinished = false;

    private float roundTransitionTimer = 1.5f;
    private bool isRoundFinished = false;

    public GameObject gift;
    public GameObject crown;

    // Start is called before the first frame update
    void Start()
    {
        winnerText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameFinished && !isRoundFinished)
        {
            if (isRoundStart)
            {
                bombTimer = roundLength;

                List<int> playerNums = new List<int>();
                GameObject[] blobs = GameObject.FindGameObjectsWithTag("Blob");
                int inactiveBlobs = 0;
                for (int i = 0; i < blobs.Length; i++)
                {
                    if (!blobs[i].GetComponent<TestBlobMove>().isActive)
                    {
                        blobs[i].transform.position = new Vector3(-6.0f + (6.0f * i), 4.0f, 3.0f);
                        inactiveBlobs++;
                    }
                    else //blob is active
                    {
                        blobs[i].transform.position = new Vector3(-9.0f + (2.0f * i + 0.95f) * 18.0f / 8.0f, 0.3f);
                        playerNums.Add(blobs[i].GetComponent<TestBlobMove>().playerNum);
                    }
                }

                int randomIndex = Random.Range(0, playerNums.Count);
                TestBlobMove.giftStarter = playerNums[randomIndex];

                var g = Instantiate(gift);
                var j = Instantiate(crown);
                g.transform.parent = blobs[randomIndex].transform;
                j.transform.parent = blobs[randomIndex].transform;

                g.transform.localScale = new Vector3(0.3f, 0.3f, 1.0f);
                j.transform.localScale = new Vector3(0.3f, 0.3f, 1.0f);

                isRoundStart = false;
            }


            bombTimer -= Time.deltaTime;

            if (bombTimer <= 0.0f)
            {
                GameObject[] blobs = GameObject.FindGameObjectsWithTag("Blob");
                int activeBlobs = 0;
                for (int i = 0; i < blobs.Length; i++)
                {
                    if (blobs[i].GetComponent<TestBlobMove>().isCarryingBomb)
                    {
                        blobs[i].GetComponent<TestBlobMove>().isActive = false;
                        blobs[i].GetComponent<TestBlobMove>().isCarryingBomb = false;

                        Destroy(blobs[i]);

                        isRoundFinished = true; //start a new round
                    }

                    if (blobs[i].GetComponent<TestBlobMove>().isActive)
                        activeBlobs++;
                }

                if (activeBlobs <= 1)
                {
                    isGameFinished = true;
                }
            }
        }
        else if (isRoundFinished)
        {
            roundTransitionTimer -= Time.deltaTime;

            if (roundTransitionTimer < 0.0f)
            {
                isRoundFinished = false;
                isRoundStart = true;
                roundTransitionTimer = 1.5f;

                GameObject[] blobs = GameObject.FindGameObjectsWithTag("Blob");
                foreach (GameObject blob in blobs)
                    blob.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(2.0f, 4.0f), Random.Range(2.0f, 4.0f));
            }
        }
        else if (isGameFinished) //game is finished
        {
            winnerText.enabled = true;

            GameObject[] blobs = GameObject.FindGameObjectsWithTag("Blob");
            foreach (GameObject blob in blobs)
                blob.transform.position = new Vector3(0.0f, 0.0f, 3.0f);

            sceneTransitionTimer -= Time.deltaTime;

            if (sceneTransitionTimer < 0.0f)
            {
                GameObject[] allBlobs = GameObject.FindGameObjectsWithTag("Blob");
                for (int i = 0; i < allBlobs.Length; i++)
                    Destroy(allBlobs[i]);

                SceneManager.LoadScene("CharacterSelect");
            }
        }
    }
}

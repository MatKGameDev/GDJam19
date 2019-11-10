using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameplay : MonoBehaviour
{
    public float bombTimer = 30.0f;

    private bool isRoundStart = true;

    public GameObject gift;
    public GameObject crown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isRoundStart)
        {
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
                    playerNums.Add(blobs[i].GetComponent<TestBlobMove>().playerNum);
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

        if (bombTimer <= 0f)
        {
            GameObject[] blobs = GameObject.FindGameObjectsWithTag("Blob");
            int activeBlobs = 0;
            for (int i = 0; i < blobs.Length; i++)
            {
                if (blobs[i].GetComponent<TestBlobMove>().isCarryingBomb)
                {
                    blobs[i].GetComponent<TestBlobMove>().isActive = false;
                    blobs[i].GetComponent<TestBlobMove>().isCarryingBomb = false;

                    isRoundStart = true; //start a new round
                }

                if (blobs[i].GetComponent<TestBlobMove>().isActive)
                    activeBlobs++;
            }

            if (activeBlobs <= 1)
            {
                GameObject[] allBlobs = GameObject.FindGameObjectsWithTag("Blob");
                for (int i = 0; i < allBlobs.Length; i++)
                    Destroy(allBlobs[i]);

                SceneManager.LoadScene("CharacterSelect");
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBlobMove : MonoBehaviour
{
    public int playerNum;

    private bool giftGivenTo = false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(4.0f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        giftGivenTo = false;
        
        float verticalInput = 0.0f;
        float horizontalInput = 0.0f;

        if (playerNum == 1)
        {
            verticalInput = Input.GetAxis("P1Vertical") * 10.0f * Time.deltaTime;
            horizontalInput = Input.GetAxis("P1Horizontal") * 10.0f * Time.deltaTime;
        }
        else if (playerNum == 2)
        {
            verticalInput = Input.GetAxis("P2Vertical") * 10.0f * Time.deltaTime;
            horizontalInput = Input.GetAxis("P2Horizontal") * 10.0f * Time.deltaTime;
        }

        GetComponent<Rigidbody2D>().velocity += new Vector2(horizontalInput, -verticalInput);

        GetComponent<Rigidbody2D>().velocity = Vector2.ClampMagnitude(GetComponent<Rigidbody2D>().velocity, 12.0f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Blob" && !giftGivenTo)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).tag == "Gift")
                {
                    collision.gameObject.GetComponent<TestBlobMove>().giftGivenTo = true;
                    transform.GetChild(i).SetParent(collision.gameObject.transform);
                }
            }
        }
    }
}

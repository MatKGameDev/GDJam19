using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBlobMove : MonoBehaviour
{
    public int playerNum;

    private float speed = 12.0f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(4.0f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = 0.0f;
        float horizontalInput = 0.0f;

        if (playerNum == 1)
        {
            verticalInput = Input.GetAxis("P1Vertical") * speed * Time.deltaTime;
            horizontalInput = Input.GetAxis("P1Horizontal") * speed * Time.deltaTime;
        }
        else if (playerNum == 2)
        {
            verticalInput = Input.GetAxis("P2Vertical") * speed * Time.deltaTime;
            horizontalInput = Input.GetAxis("P2Horizontal") * speed * Time.deltaTime;
        }

        GetComponent<Rigidbody2D>().velocity += new Vector2(horizontalInput, -verticalInput);
        GetComponent<Rigidbody2D>().velocity = Vector2.ClampMagnitude(GetComponent<Rigidbody2D>().velocity, 6.0f);

        if (GetComponent<Rigidbody2D>().velocity.x <= 0)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
    }

    //add states and shit for animation purposes//
    // - Marcus
}
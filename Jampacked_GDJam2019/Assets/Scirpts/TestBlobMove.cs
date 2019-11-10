using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBlobMove : MonoBehaviour
{
    public int playerNum;

    private bool giftGivenTo = false;
    private float speed = 12.0f;
    private Animator anim;

    private static int giftStarter = -1;
    public GameObject gift;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(4.0f, 3.0f);
        anim = GetComponent<Animator>();

        if (giftStarter == -1)
        {
            giftStarter = Random.Range(1, 5);
        }

        if (giftStarter == playerNum)
        {
            var g = Instantiate(gift);
            g.transform.parent = gameObject.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {

        giftGivenTo = false;
        
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

        setAnimationState();
    }

    private void OnCollisionEnter2D(Collision2D collision)
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

    private void OnCollisionExit2D(Collision2D collision)
    {
        anim.SetBool("isColliding", true);
    }

    void setAnimationState()
    {
        if (GetComponent<Rigidbody2D>().velocity.magnitude <= 1.0f)
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isMoving", false);
            anim.SetBool("isColliding", false);
        }
        else
        {
            anim.SetBool("isIdle", false);
            anim.SetBool("isMoving", true);
            anim.SetBool("isColliding", false);
        }
    }
}

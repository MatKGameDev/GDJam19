using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBlobMove : MonoBehaviour
{
    public int playerNum;

    public bool isActive = true;
    public bool isCarryingBomb;

    public bool giftGivenTo = false;
    private float speed = 12.0f;
    private Animator anim;

    public static int giftStarter = -1;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            giftGivenTo = false;

            float horizontalInput = 0.0f;
            float verticalInput = 0.0f;

            if (playerNum == 0)
            {
                verticalInput = Input.GetAxis("P1Vertical") * speed * Time.deltaTime;
                horizontalInput = Input.GetAxis("P1Horizontal") * speed * Time.deltaTime;
            }
            else if (playerNum == 1)
            {
                verticalInput = Input.GetAxis("P2Vertical") * speed * Time.deltaTime;
                horizontalInput = Input.GetAxis("P2Horizontal") * speed * Time.deltaTime;
            }
            else if (playerNum == 2)
            {
                verticalInput = Input.GetAxis("P3Vertical") * speed * Time.deltaTime;
                horizontalInput = Input.GetAxis("P3Horizontal") * speed * Time.deltaTime;
            }
            else if (playerNum == 3)
            {
                verticalInput = Input.GetAxis("P4Vertical") * speed * Time.deltaTime;
                horizontalInput = Input.GetAxis("P4Horizontal") * speed * Time.deltaTime;
            }

            GetComponent<Rigidbody2D>().velocity += new Vector2(horizontalInput, -verticalInput);
            GetComponent<Rigidbody2D>().velocity = Vector2.ClampMagnitude(GetComponent<Rigidbody2D>().velocity, 10.0f);

            if (GetComponentInChildren<LttieBabyFollow>())
                isCarryingBomb = true;
            else
                isCarryingBomb = false;
        }
        else //blob is inactive
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        }

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
                    var arr = GameObject.FindGameObjectsWithTag("Gift");
                    collision.gameObject.GetComponent<TestBlobMove>().giftGivenTo = true;
                    foreach (GameObject obj in arr)
                    {
                        obj.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                        obj.transform.SetParent(collision.gameObject.transform);
                    }
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

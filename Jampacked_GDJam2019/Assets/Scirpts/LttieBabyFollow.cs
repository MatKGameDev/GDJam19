using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LttieBabyFollow : MonoBehaviour
{
    private Vector2 targetPos = new Vector2(0, 0);

    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float minDist = GetComponentInParent<CircleCollider2D>().radius;

        minDist *= 1.5f;

        targetPos = -GetComponentInParent<Rigidbody2D>().velocity.normalized * minDist;

        Vector2 toTargetPos = targetPos - (Vector2) transform.localPosition;

        transform.Translate(toTargetPos * 0.9f * Time.deltaTime * 10.0f);
    }
}

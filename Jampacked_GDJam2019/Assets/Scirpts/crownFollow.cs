using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crownFollow : MonoBehaviour
{
    private Vector2 targetPos = new Vector2(0.0f, 0.3f);

    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

        targetPos = new Vector2(0.0f, 0.5f);

        Vector2 toTargetPos = targetPos - (Vector2) transform.localPosition;

        transform.Translate(toTargetPos * 0.9f * Time.deltaTime * 10f);

    }
}

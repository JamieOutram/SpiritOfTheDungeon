using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collider_test : MonoBehaviour
{
    public float forceMagnitude = 200f;
    private Rigidbody2D rb;
    private Vector2 rightForce = new Vector2();
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        Debug.Log(gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        rightForce = new Vector2(forceMagnitude * Time.deltaTime, 0f);
        rb.AddForce(rightForce);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saushot : MonoBehaviour
{
    public float xspeed;
    public float yspeed;
    public float lim;
    public bool penetrate;
    private Rigidbody2D rb = null;
    private float count = 0.0f;
    private float rad;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(xspeed*transform.localScale.x, yspeed*transform.localScale.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        count += Time.deltaTime;
        if (count > lim) {
            Destroy(gameObject);
        }
    }

    
}

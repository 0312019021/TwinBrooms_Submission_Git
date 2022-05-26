using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poison: MonoBehaviour
{
    public int touchatk;
    public float startspeed;
    public float lim;
    private float count=0;
    private float rad;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        rad = (transform.eulerAngles.z+90) * Mathf.PI / 180;
        rb.velocity = new Vector2(Mathf.Cos(rad) * startspeed, Mathf.Sin(rad) * startspeed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        count += Time.deltaTime;
        if (count >= lim)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "sau")
        {
            collision.gameObject.GetComponent<Sau>().damage(touchatk);
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "sau")
        {
            collision.gameObject.GetComponent<Sau>().damage(touchatk);

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float xspeed;
    public float yspeed;
    public int atk;
    public float lim;
    public bool penetrate;
    private Rigidbody2D rb = null;
    private float count = 0.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = new Quaternion(0,0,transform.rotation.z,1);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(xspeed * transform.localScale.x, yspeed * transform.localScale.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        count += Time.deltaTime;
        if (count > lim)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "sau")
        {
            collision.gameObject.GetComponent<Sau>().damage(atk);
        }
        else if (collision.tag == "ground")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "sau")
        {
            collision.gameObject.GetComponent<Sau>().damage(atk);
        }
        else if (collision.tag == "ground")
        {
            Destroy(gameObject);
        }
    }
}

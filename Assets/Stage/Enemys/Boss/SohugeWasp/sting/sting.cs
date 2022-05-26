using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sting : MonoBehaviour
{
    public float speed;
    public int atk;
    public float groundlim;
    public float bomblim;
    public float poisoneuler;
    public bool penetrate;
    public GameObject poison;

    private Animator anim;
    private Rigidbody2D rb = null;
    private float groundcount = 0.0f;
    private float count = 0.0f;
    private float rad;
    private int state = 0;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rad = (transform.eulerAngles.z) * Mathf.PI / 180;
        rb.velocity = new Vector2(Mathf.Cos(rad) * speed, Mathf.Sin(rad) * speed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (state == 1)
        {
            groundcount += Time.deltaTime;
            if (groundcount > groundlim)
            {
                rb.velocity = new Vector2(0, 0);
            }
        }
        if (state != 2)
        {
            count += Time.deltaTime;
            if (count > bomblim)//é©îöèàóù
            {
                Instantiate(poison, this.transform.position, Quaternion.Euler(0, 0, this.transform.eulerAngles.z + 90 + poisoneuler));
                Instantiate(poison, this.transform.position, Quaternion.Euler(0, 0, this.transform.eulerAngles.z + 90 - poisoneuler));
                anim.SetTrigger("bomb");
                state = 2;
            }
        }
    }

    public void end()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "sau")
        {
            collision.gameObject.GetComponent<Sau>().damage(atk);
        }
        else if (collision.tag == "ground")
        {
            if (state == 0)
            {
                state = 1;
            }
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
            if (state == 0)
            {
                state = 1;
            }
        }
    }
}
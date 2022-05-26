using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sauattack : MonoBehaviour
{
    public int atk;
    public float invisibletime;
    public bool active;
    public Saushot saushot;
    //public float x;
    //public float y;
    //public float z;

    private string enemycolltag = "Enemycoll";
    private string bosstag = "Boss";
    private int xscale;

    //Vector3 xyz;
    
    // Start is called before the first frame update
    void Start()
    {
        //xyz = this.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {

        //this.transform.localPosition = xyz;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == enemycolltag&&active)
        {
            collision.gameObject.GetComponent<Enemycollision>().calldamage(atk, invisibletime);
        }
        if (saushot != null && !saushot.penetrate) {
            Destroy(gameObject);
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == enemycolltag&&active)
        {
            collision.gameObject.GetComponent<Enemycollision>().calldamage(atk, invisibletime);
        }
        if (saushot != null && !saushot.penetrate)
        {
            Destroy(gameObject);
        }

    }
}
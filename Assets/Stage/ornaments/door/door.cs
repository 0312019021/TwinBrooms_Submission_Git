using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    public Sau sau;
    public GameObject enterendpoint;
    public vcammanager vcam;
    public GameObject warningimg;

    public GameObject[] enemys;
    public GameObject[] secondenemys;

    private Animator anim;
    private bool beforeboss = true;
    private bool beforebroken = true;
    private bool opening=false;
    private bool sauentering=false;
    private bool enterstart = true;
    private bool warningdirection=false;
    private bool warningstart = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (opening)
        {
            sau.stand();
        }
        else if (sauentering)
        {
            if (enterstart)
            {
                vcam.changefollow(1);
                enterstart = false;
                sau.anim.SetTrigger("walk");
            }
            sau.eventwalk(1, 1000000);
            if (sau.transform.position.x > enterendpoint.transform.position.x)
            {
                sauentering = false;
                anim.SetTrigger("close");
                sau.eventstand();
                warningdirection = true;
                warningstart = true;
            }
        }else if (warningdirection)
        {
            if (warningstart)
            {
                warningimg.SetActive(true);

                //çsÇ´ìπíÜÇÃìGÇè¡ãé
                for(int i =0;i<enemys.Length ;i++)
                {
                    if (enemys[i] != null)
                    {
                        enemys[i].SetActive(false);
                    }
                }

                //ãAÇËìπÇÃìGèoåª
                for(int i = 0; i < secondenemys.Length; i++)
                {
                    if (secondenemys[i] != null)
                    {
                        secondenemys[i].SetActive(true);
                    }
                }

                warningstart = false;
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "sau" && beforeboss)
        {
            collision.gameObject.GetComponent<Sau>().eventstart();
            anim.SetTrigger("open");
            sau.eventstand();
            beforeboss = false;
            opening = true;
        }else if (collision.tag == "breakpoint" && beforebroken)
        {
            anim.SetTrigger("broken");
            beforebroken = false;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "sau" && beforeboss)
        {
            collision.gameObject.GetComponent<Sau>().eventstart();
            anim.SetTrigger("open");
            sau.eventstand();
            beforeboss = false;
            opening = true;

        }
        else if (collision.tag == "breakpoint" && beforebroken)
        {
            anim.SetTrigger("broken");
            beforebroken = false;
        }
    }

    public void openperiod()
    {
        sauentering = true;
        enterstart = true;
        opening = false;
    }

    public void setidle()
    {
        anim.SetTrigger("idle");
    }

    public void periodbroken()
    {
        Destroy(gameObject);
    }
}

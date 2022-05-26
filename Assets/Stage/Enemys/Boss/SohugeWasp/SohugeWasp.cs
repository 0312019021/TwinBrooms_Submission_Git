using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SohugeWasp : Boss
{
    public int assault2max;
    public int summon2max;
    public int summon3max;
    public int summon4max;
    public int event1_1_1max;
    public int actionfordebug;
    public float event1_3time;
    public float summonfrequency;
    public float[] idletimes;
    public float speed1_2;
    public float speed;

    public GameObject sting;
    public GameObject sprinkler;
    public GameObject larvae;
    public GameObject shotpoint;
    public GameObject sprinklerpoint;
    public GameObject summonpoint;
    public GameObject ev1_2marker;
    public GameObject saudefaultmarker;
    public GameObject sausecondmarker;
    public GameObject breakpoint;
    public GameObject clearimg;
    public GameObject clearevmanager;
    public Transform[] actmarker;
    public int[] acts;
    public vcammanager vcam;
    public float tolerance;
    public float walkspeed;

    private bool idling = false;
    private bool summoning = false;
    private bool ev1_2 = false;
    private bool beforeprogress2 = true;
    private bool beforedeleted = true;
    private bool saumovestart = true;
    private bool saumoveend = false;
    private int evno = 0;
    private int assault2count = 0;
    private int summon2count = 0;
    private int summon3count = 0;
    private int summon4count = 0;
    private int event1_1_1count = 0;
    private int nextactmarker;
    private float summoncount = 0;
    private float idlecount = 0;
    private int nextmotion;//0:assault 1:summon 2:shot
    private GameObject nowlarvae;
    private GameObject nowsprinkler;
    private Rigidbody2D rb;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        idling = true;
        nextmotion = (int)Random.Range(0, 3);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!deleted)
        {

            if (ev)
            {
                if (progress == 2)
                {
                    if (ev1_2)
                    {
                        if (transform.position.x < ev1_2marker.transform.position.x)
                        {
                            setidle();
                            ev1_2 = false;
                            rb.velocity = new Vector2(0, 0);
                            transform.localScale = new Vector3(-1, 1, 1);
                            evno = 1;
                            vcam.changefollow(2);
                        }
                    }

                    if (beforeprogress2)
                    {
                        anim.SetTrigger("event1_1");
                        beforeprogress2 = false;
                        breakpoint.SetActive(true);
                        summoning = false;
                    }
                    else
                    {
                        if (evno == 0)
                        {
                            if (saumovestart)
                            {
                                saumovestart = false;
                                sau.anim.SetTrigger("walk");
                            }
                            if (sau.transform.position.x > saudefaultmarker.transform.position.x + tolerance)
                            {
                                sau.eventwalk(-1, -walkspeed);
                            }
                            else if (sau.transform.position.x < saudefaultmarker.transform.position.x - tolerance)
                            {
                                sau.eventwalk(1, walkspeed);
                            }
                            else
                            {
                                if (!saumoveend)
                                {
                                    sau.eventstand();
                                    saumoveend = true;
                                }
                                
                            }
                        }
                        else if (evno == 1)
                        {
                            if (sau.transform.position.x > sausecondmarker.transform.position.x + tolerance)
                            {
                                if (saumoveend)
                                {
                                    saumoveend = false;
                                    sau.anim.SetTrigger("walk");
                                }
                                sau.eventwalk(-1, -walkspeed);
                            }
                            else
                            {
                                evno = 2;
                                if (!saumoveend)
                                {
                                    sau.eventstand();
                                    saumoveend = true;
                                }
                                anim.SetTrigger("event1_3");
                            }
                        }

                    }

                }
            }
            else
            {
                switch (progress)
                {
                    case 0: break;
                    case 1: pr1(); break;
                    case 2: pr2(); break;
                    case 3: pr3(); break;
                }
            }
        }
        else//倒された時の処理
        {
            if (beforedeleted)
            {
                beforedeleted = false;
                rb.velocity = new Vector2(0, 0);
                clearevmanager.SetActive(true);
            }
        }
    }

    private void pr1()
    {
        if (summoning)
        {
            summoncount += Time.deltaTime;
            if (summoncount >= summonfrequency)
            {
                summon();
                summoncount = 0;
            }
        }
        else if (idling)
        {
            idlecount += Time.deltaTime;
            if (idlecount >= idletimes[nextmotion])
            {
                switch (nextmotion)
                {
                    case 0: setassault1(); break;
                    case 1: setsummon1(); break;
                    case 2: setshot1(); break;
                }
                if (actionfordebug >= 3)//次の行動を決定
                {
                    nextmotion = (int)Random.Range(0, 3);
                }
                else//デバッグ用行動指定
                {
                    nextmotion = actionfordebug;
                }

                idling = false;
                idlecount = 0;
            }
        }
    }

    private void pr2()
    {
        if (summoning)
        {
            summoncount += Time.deltaTime;
            if (summoncount >= summonfrequency)
            {
                summon();
                summoncount = 0;
            }
        }
        else if (idling)
        {
            if (transform.position.x < actmarker[nextactmarker].position.x)
            {
                rb.velocity = new Vector2(0, 0);
                nextmotion = acts[nextactmarker];
                switch (nextmotion)
                {
                    case 0: setassault1(); break;
                    case 1: setsummon1(); break;
                    case 2: setshot1(); break;
                }
                nextactmarker += 1;
                idling = false;
                if (nextactmarker >= actmarker.Length)
                {
                    progress = 3;
                }
            }




        }
    }

    private void pr3()
    {
        if (summoning)
        {
            summoncount += Time.deltaTime;
            if (summoncount >= summonfrequency)
            {
                summon();
                summoncount = 0;
            }
        }
        else if (idling)
        {
            idlecount += Time.deltaTime;
            if (idlecount >= idletimes[nextmotion])
            {
                switch (nextmotion)
                {
                    case 0: setassault1(); break;
                    case 1: setsummon1(); break;
                    case 2: setshot1(); break;
                }
                nextmotion = (int)Random.Range(0, 3);
                idling = false;
                idlecount = 0;
            }
        }
    }

    public void periodassault2()
    {
        assault2count++;
        if (assault2count >= assault2max)
        {
            assault2count = 0;
            setassault3();
        }
    }

    public void periodsummon2()
    {
        summon2count++;
        if (summon2count >= summon2max)
        {
            summon2count = 0;
            setsummon3();
            summoning = true;
        }
    }

    public void periodsummon3()
    {
        summon3count++;
        if (summon3count >= summon3max)
        {
            summon3count = 0;
            setsummon4();
            summoning = false;
        }
    }

    public void periodsummon4()
    {
        summon4count++;
        if (summon4count >= summon4max)
        {
            summon4count = 0;
            setidle();
        }
    }

    public void periodevent1_1_1()
    {
        event1_1_1count++;
        if (event1_1_1count >= event1_1_1max)
        {
            event1_1_1count = 0;
            setevent1_2();
            ev1_2 = true;
        }
    }

    public void event1_2movestart()
    {
        rb.velocity = new Vector2(speed1_2, 0);
    }

    public void periodevent1_3()
    {
        setidle();
        ev = false;
        sau.eventmotion = false;
    }

    public override void perioddeletedanim()
    {
        base.perioddeletedanim();
        clearimg.SetActive(true);
    }

    public void setidle()
    {
        Debug.Log("idle");
        anim.SetTrigger("idle");
        idling = true;
        if (progress == 2)
        {
            rb.velocity = new Vector2(speed, 0);
        }
    }

    public void setassault1()
    {
        Debug.Log("assault1");
        anim.SetTrigger("assault1");
    }

    public void setassault2()
    {
        Debug.Log("assault2");
        anim.SetTrigger("assault2");
    }

    public void setassault3()
    {
        Debug.Log("assault3");
        anim.SetTrigger("assault3");
    }

    public void setsummon1()
    {
        Debug.Log("summon1");
        anim.SetTrigger("summon1");
    }

    public void setsummon2()
    {
        Debug.Log("summon2");
        anim.SetTrigger("summon2");
    }

    public void setsummon3()
    {
        Debug.Log("summon3");
        anim.SetTrigger("summon3");
    }

    public void setsummon4()
    {
        Debug.Log("summon4");
        anim.SetTrigger("summon4");
    }

    public void setshot1()
    {
        Debug.Log("shot1");
        anim.SetTrigger("shot1");
    }

    public void setevent1_1()
    {
        anim.SetTrigger("event1_1");
    }
    public void setevent1_1_1()
    {
        anim.SetTrigger("event1_1_1");
    }

    public void setevent1_2()
    {
        anim.SetTrigger("event1_2");
    }


    public void shot()
    {
        Vector3 shotangle = shotpoint.transform.rotation.eulerAngles;
        if (progress == 2 || progress == 3)
        {
            shotangle.z += 180;
        }
        Instantiate(sting, shotpoint.transform.position, Quaternion.Euler(shotangle));

    }

    public void setsprinkler()
    {
        nowsprinkler = Instantiate(sprinkler, sprinklerpoint.transform.position, Quaternion.identity);
        nowsprinkler.transform.localScale = this.transform.localScale;
    }

    public void summon()
    {
        nowlarvae = Instantiate(larvae, summonpoint.transform.position, Quaternion.identity);
        nowlarvae.transform.localScale = this.transform.localScale;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warning : MonoBehaviour
{
    public int frequency;

    public Sau sau;
    public Boss boss;
    public GameObject bosshpbar;
    private int count=0;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void periodawake()
    {
        anim.SetTrigger("idle");
    }

    public void periodidle()
    {
        count++;
        if (count >= frequency)
        {
            anim.SetTrigger("end");
        }
    }

    public void periodend()
    {
        Debug.Log("periodend");
        
        sau.eventmotion = false;
        boss.ev = false;
        boss.progress = 1;
        bosshpbar.SetActive(true);
        this.gameObject.SetActive(false);
    }

    
}

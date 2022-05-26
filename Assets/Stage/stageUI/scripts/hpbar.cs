using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hpbar : MonoBehaviour
{
    public GameObject[] valueimgs;

    private int oldhp;
    public Sau Sau;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (oldhp != Sau.hp)
        {
            if (oldhp > Sau.hp)//hp‚ªŒ¸­‚µ‚½‚Æ‚«
            {
                for (int i = oldhp; i > Sau.hp; i--)
                {
                    valueimgs[i-1].SetActive(false);
                }
            }
            
            oldhp = Sau.hp;
        }
    }
}

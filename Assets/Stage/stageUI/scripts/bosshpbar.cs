using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bosshpbar : MonoBehaviour
{
    public Boss boss;
    private int oldhp;
    public Slider bar;

    // Start is called before the first frame update
    void Start()
    {
        bar.maxValue = boss.hp;
        bar.value = boss.hp;
    }

    // Update is called once per frame
    void Update()
    {
        if (oldhp != boss.hp)
        {
            bar.value = boss.hp;
            oldhp = boss.hp;
        }
    }

    public void startsecondprogress()
    {
        bar.maxValue = boss.secondhp;
        bar.value = boss.secondhp;
    }
}

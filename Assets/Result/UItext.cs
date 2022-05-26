using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UItext : MonoBehaviour
{
    public Text texts = null;
    private int oldtrspeed;
    private int oldcombo;
    private int oldhp;
    public Sau Sau;
    // Start is called before the first frame update
    void Start()
    {
        texts.text = "HP"+Sau.hp+"\ntrspeed" + Sau.diskspace+"\ncombo"+Sau.combo;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (oldhp != Sau.hp || oldtrspeed != Sau.diskspace || oldcombo != Sau.combo)
        {
            texts.text= "HP" + Sau.hp + "\ntrspeed" + Sau.diskspace + "\ncombo" + Sau.combo;
            oldhp = Sau.hp;
            oldtrspeed = Sau.diskspace;
            oldcombo = Sau.combo;
        }
    }
}

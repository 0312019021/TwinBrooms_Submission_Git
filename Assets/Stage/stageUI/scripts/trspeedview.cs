using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class trspeedview : MonoBehaviour
{
    public Text texts = null;
    public GameObject tb;
    private int oldtrspeed;
    public Sau Sau;
    // Start is called before the first frame update
    void Start()
    {
        texts.text = "";
        tb.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Sau.combo == 0)
        {
            texts.text = "";
            tb.SetActive(false);
        }else if(oldtrspeed != Sau.diskspace)
        {
            texts.text = Sau.diskspace.ToString();
            oldtrspeed = Sau.diskspace;
            tb.SetActive(true);
        }
    }
}

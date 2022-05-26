using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bgmconfig : MonoBehaviour
{
    public float initialsoundvol_bgm;
    public Slider bgmslider;
    // Start is called before the first frame update
    void Start()
    {
        bgmslider.value = Gmanager.instance.soundvol_bgm;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setsoundvol_bgm()
    {
        Gmanager.instance.soundvol_bgm=bgmslider.value;
    }

    public void Resetsoundvol_bgm()
    {
        bgmslider.value = initialsoundvol_bgm;
        Setsoundvol_bgm();
    }
}

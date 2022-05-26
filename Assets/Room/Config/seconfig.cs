using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class seconfig : MonoBehaviour
{
    public float initialsoundvol_se;
    public Slider seslider;
    // Start is called before the first frame update
    void Start()
    {
        seslider.value = Gmanager.instance.soundvol_se;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setsoundvol_se()
    {
        Gmanager.instance.soundvol_se = seslider.value;
    }

    public void Resetsoundvol_se()
    {
        seslider.value = initialsoundvol_se;
        Setsoundvol_se();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class swipelengthconfig : MonoBehaviour
{
    public float initialswipelengthcoefficient;
    public Slider swipelengthslider;
    // Start is called before the first frame update
    void Start()
    {
        swipelengthslider.value = Gmanager.instance.sensitivity_swipelengthcoefficient;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setswipelengthcoefficient()
    {
        Gmanager.instance.sensitivity_swipelengthcoefficient = swipelengthslider.value;
    }

    public void Resetswipelengthcoefficient()
    {
        swipelengthslider.value = initialswipelengthcoefficient;
        Setswipelengthcoefficient();
    }
}

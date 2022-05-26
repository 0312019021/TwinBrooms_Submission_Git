using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class longtapconfig : MonoBehaviour
{
    public float initiallongtapcoefficient;
    public Slider longtapslider;
    // Start is called before the first frame update
    void Start()
    {
        longtapslider.value = Gmanager.instance.sensitivity_longtapcoefficient;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Setlongtapcoefficient()
    {
        Gmanager.instance.sensitivity_longtapcoefficient = longtapslider.value;
    }

    public void Resetlongtapcoefficient()
    {
        longtapslider.value = initiallongtapcoefficient;
        Setlongtapcoefficient();
    }
}

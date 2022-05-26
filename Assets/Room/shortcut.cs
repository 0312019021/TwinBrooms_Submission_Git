using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shortcut : MonoBehaviour
{
    private bool firstpush = false;

    public GameObject destination;
    public GameObject close;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (firstpush)
        {
            firstpush = false;
        }
    }

    public void open()
    {
        if (!firstpush)
        {
            firstpush = true;
            if (destination != null)
            {
                destination.SetActive(true);
            }
            if (close != null)
            {
                close.SetActive(false);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class finpoint : MonoBehaviour
{
    private Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        var c = image.color;
        if (!Input.GetMouseButton(0))
        {
            
            image.color = new Color(c.r,c.g,c.b,0);
        }
        else
        {
            transform.position = Input.mousePosition;
            
            image.color = new Color(c.r, c.g, c.b, 0.5f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillview : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject sau;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void animstart()
    {
        if (this.transform.localScale.x != sau.transform.localScale.x)
        {
            this.transform.localScale = new Vector3(sau.transform.localScale.x,1,1);
        }
    }
}

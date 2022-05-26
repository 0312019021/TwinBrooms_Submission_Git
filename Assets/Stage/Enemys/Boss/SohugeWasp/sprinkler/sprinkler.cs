using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sprinkler : MonoBehaviour
{
   
    public float frequency;
    public float[] rad1;
    public GameObject poison;

    private int no=0;
    private float frequencycount=0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        frequencycount += Time.deltaTime;
        if (frequencycount > frequency)
        {
            if (no >= rad1.Length)
            {
                Destroy(gameObject);
            }
            else
            {
                Instantiate(poison, this.transform.position, Quaternion.Euler(0, 0, transform.localScale.x * rad1[no]));
                no += 1;
                frequencycount = 0;
            }

        }
        
    }
}

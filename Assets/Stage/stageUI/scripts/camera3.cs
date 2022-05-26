using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera3 : MonoBehaviour
{
    public GameObject sau;
    public float y;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector2(sau.transform.position.x, y);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private string groundTag = "ground";
    private bool isGround = false;
    private bool isGroundEnter, isGroundStay, isGroundExit;

    //Vector3 xyz;
    // Start is called before the first frame update
    void Start()
    {
        //xyz = this.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.localPosition = xyz;
    }

    public bool IsGround()
    {
        /*
        if (isGroundEnter || isGroundStay)
        {
            isGround = true;
        }*/
        if (isGroundEnter||isGroundStay)
        {
            isGround = true;
        }
        else if (isGroundExit)
        {
            isGround = false;
        }



        isGroundEnter = false;
        isGroundStay = false;
        isGroundExit = false;
        return isGround;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == groundTag)
        {
            isGroundEnter = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == groundTag)
        {
            isGroundStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == groundTag)
        {
            isGroundExit = true;
        }
    }
}

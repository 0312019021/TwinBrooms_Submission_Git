using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 “Gmob‚ªŒ‚”j‚³‚ê‚½‚Æ‚«‹¤’Ê‚Ì‰æ‘œ‚Ì“®ì‚ğw’è‚·‚éƒNƒ‰ƒX
 */


public class recycleimgrotation : MonoBehaviour
{
    public float add;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 nowro = transform.localRotation.eulerAngles;
        nowro.z += add;
        transform.localRotation = Quaternion.Euler(nowro);
    }
}

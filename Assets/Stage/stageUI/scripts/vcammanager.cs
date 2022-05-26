using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class vcammanager : MonoBehaviour
{
    

    public bool flag=false;
    public Transform[] follow;

    public CinemachineVirtualCamera _virtualCamera;//vcam1
    
     
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (flag)
        {
            changefollow(1);

        }
        else
        {
            changefollow(0);
        }
        */
    }

    public void changefollow(int i)
    {
        _virtualCamera.Follow = follow[i];
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sauspritemaster : MonoBehaviour
{
    public Sau sau;
    public void attackcutrelay()
    {
        sau.attackanimcut();
        Debug.Log("cutrelay");
    }

    public void attackendrelay()
    {
        sau.attackanimend();
        Debug.Log("endrelay");
    }
}

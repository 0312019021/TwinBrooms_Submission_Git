using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cleareventmanager : MonoBehaviour
{
    public Sau sau;

    // Start is called before the first frame update
    void Start()
    {
        sau.resetanim();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        sau.stand();
    }
}
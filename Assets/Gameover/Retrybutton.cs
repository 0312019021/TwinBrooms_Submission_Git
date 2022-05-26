using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Retrybutton : MonoBehaviour
{
    private bool firstpush = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pushed()
    {
        if (!firstpush)
        {
            SceneManager.LoadScene(Gmanager.instance.playingstage);
            firstpush = true;
        }
    }
}

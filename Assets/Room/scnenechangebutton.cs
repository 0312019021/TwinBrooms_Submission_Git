using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scnenechangebutton : MonoBehaviour
{
    private bool firstpush = false;

    public string scenename;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void scenechange()
    {
        if (!firstpush)
        {
            firstpush = true;
            SceneManager.LoadScene(scenename);
        }
    }
}

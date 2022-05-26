using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class clearimg : MonoBehaviour
{
    public Text texts;
    public string message;
    public float addtextfrequency;
    public float toresulttime;

    private int textnum=0;
    private int len;
    private float elapsedtime = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        len = message.Length;
        texts.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        elapsedtime += Time.deltaTime;
        if (elapsedtime > addtextfrequency&&len>textnum)
        {
            textupdate();
            elapsedtime = 0.0f;
        }
        else if(elapsedtime > toresulttime && len <= textnum)
        {
            toresult();
        }
    }

    public void textupdate()
    {
        texts.text = texts.text + message[textnum];
        textnum += 1;
    }

    public void toresult()
    {
        SceneManager.LoadScene("Result");
    }

    
}

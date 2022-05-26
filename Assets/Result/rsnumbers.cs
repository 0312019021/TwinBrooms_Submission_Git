using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rsnumbers : MonoBehaviour
{
    public Text texts = null;
    // Start is called before the first frame update
    void Start()
    {
        texts.text = Gmanager.instance.deleten+"\n"
            + Gmanager.instance.clearmin+","+(int)Gmanager.instance.clearsec+"\n"
            + Gmanager.instance.damagen+"\n"
            + Gmanager.instance.maxcombo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

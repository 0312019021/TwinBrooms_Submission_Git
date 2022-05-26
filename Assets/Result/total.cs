using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class total : MonoBehaviour
{
    public Text texts = null;
    private string rank;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void totalscoreview(int totalscore)
    {
        if (totalscore == 100)
        {
            rank = "SS";
        }else if (totalscore >= 90)
        {
            rank = "S";
        }else if (totalscore >= 80)
        {
            rank = "A";
        }else if (totalscore >=60)
        {
            rank = "B";
        }else if (totalscore >= 50)
        {
            rank = "C";
        }
        else
        {
            rank = "D";
        }
        texts.text = totalscore + "\n" + rank;
    }
}

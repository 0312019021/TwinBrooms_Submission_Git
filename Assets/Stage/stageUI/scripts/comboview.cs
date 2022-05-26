using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class comboview : MonoBehaviour
{
    public Text texts = null;
    public GameObject hits;
    private int oldcombo;
    public Sau Sau;
    // Start is called before the first frame update
    void Start()
    {
        texts.text = "";
        hits.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (oldcombo != Sau.combo)
        {
            
            if (Sau.combo == 0)
            {
                texts.text = "";
                hits.SetActive(false);
            }
            else
            {
                texts.text = Sau.combo.ToString();
                oldcombo = Sau.combo;
                hits.SetActive(true);
            }
        }
    }
}

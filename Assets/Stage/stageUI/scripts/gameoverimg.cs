using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameoverimg : MonoBehaviour
{
    public GameObject me;
    public GameObject canvas;
    public float next;
    public float xmove;
    public float ymove;
    public float ylim;
    public float ystart;
    public float xadd;
    public float xlim;
    private Vector3 nextpos;
    private bool last = false;
    private float timer=0;
    // Start is called before the first frame update
    void Start()
    {
        if (transform.position.x < xlim)
        {
            last = true;
            SceneManager.LoadScene("gameover");
            //ゲームオーバー画面を表示
        }
        else
        {
            nextpos = transform.position;
            nextpos.x += xmove;
            nextpos.y += ymove;
            if (nextpos.y < ylim)
            {
                nextpos.x += xadd;
                nextpos.y = ystart;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < next)
        {
            timer += Time.deltaTime;
            if (timer > next&&!last)
            {
                var parent = canvas.transform;
                Instantiate(me,nextpos, Quaternion.identity,parent);
            }
        }
    }
}

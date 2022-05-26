using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 敵の各スプライトオブジェクトに付けるクラス。
 複数パーツの画像を含む場合、その全てに付ける。
 画像一枚のキャラの場合、Enemyクラスと同じ場所につけてもよい。
 <表示切り替え処理>親オブジェクトの状態を参照し表示/非表示を切り替える。主に無敵時間の点滅に使用。
 */

public class enemyparts : MonoBehaviour
{
    private GameObject rootob;
    private Enemy enemysc;

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        rootob = transform.root.gameObject;
        enemysc = rootob.GetComponent<Enemy>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemysc.partsvisible)
        {
            sr.enabled = true;
        }
        else
        {
            sr.enabled = false;
        }
    }

    
}

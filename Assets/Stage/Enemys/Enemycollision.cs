using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 敵オブジェクトの当たり判定をもつパーツのクラス
 画像がパーツ分けされていないキャラの場合、Enemyクラスをもつオブジェクトに同時に付けてもよい
 <被弾処理>プレイヤーキャラの攻撃判定に触れたとき、Enemyクラスの被ダメージ処理を呼び出す。
 */

public class Enemycollision : MonoBehaviour
{
    private GameObject rootob;
    private Enemy damageco;
    // Start is called before the first frame update
    void Start()
    {
        rootob = transform.root.gameObject;
        damageco = rootob.GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void calldamage(int atk, float invisibletime)
    {
        damageco.Damage(atk, invisibletime);
    }
}

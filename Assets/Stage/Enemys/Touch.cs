using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 敵MOBにプレイヤーキャラが触れた際のダメージ発生処理を行うクラス。
 障害物など、触れてもダメージが発生しないオブジェクトには付けない。
 複数パーツの画像で構成される敵オブジェクトの場合、
 当たり判定を付けたいボーンの子オブジェクトを作成し、
 Colliderと一緒にaddcomponentする。

 <接触ダメージ処理>触れたプレイヤーキャラの被ダメージ処理を呼び出す。
 */

public class Touch : MonoBehaviour
{
    public int touchatk;
    public Enemy me;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "sau" && !me.deleted)
        {
            collision.gameObject.GetComponent<Sau>().damage(touchatk);
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "sau" && !me.deleted)
        {
            collision.gameObject.GetComponent<Sau>().damage(touchatk);

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 敵オブジェクト共通のクラス。各敵固有のクラスに継承して使用。
 最も上の親オブジェクトに付けないとEnemycollisionから参照できない点に注意。
<被弾処理>プレイヤーキャラの攻撃を受けた際hp減少、各スコアの追加、無敵時間の開始。
<無敵時間中処理>パーツ画像の点滅をさせる。
 */


public class Enemy : MonoBehaviour
{
    public bool dontadddeleten;//撃破数スコアを加算しない
    public bool dontadddiskspace;//被弾時プレイヤーのmpを加算させない
    public bool dontaddcombo;//被弾時連続攻撃数を加算させない
    public int hp;
    public bool deleted = false;
    public bool partsvisible = true;//キャラクター画像の表示/非表示　無敵時間の点滅に使用

    protected bool invisible = false;//無敵時間かどうか
    protected bool sleep = true;//画面に入るまで待機状態
    protected bool beforesleep = true;//sleep解除した瞬間特定の処理をさせる
    protected float invisibletime = 0.0f;//無敵開始からの経過時間
    protected float endinvisibletime = 0.0f;//無敵終了時間　受けた攻撃によって設定
    protected float blinktime = 0.0f;//点滅時使用するカウンタ

    protected Animator anim = null;
    protected GameObject sauob;
    protected Sau sau;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        partsvisible = true;
        anim = GetComponent<Animator>();
        sauob = GameObject.FindGameObjectWithTag("sau");
        sau = sauob.GetComponent<Sau>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!deleted)//撃破されていない時のみ動作
        {
            if (invisible)//無敵時
            {
                invisibletime += Time.deltaTime;
                if (invisibletime < endinvisibletime)
                {
                    if (blinktime > 0.1f)//明滅　表示されている時
                    {

                        partsvisible = true;

                        blinktime = 0.0f;
                    }
                    else if (blinktime > 0.05f)//明滅　消えている時
                    {
                        partsvisible = false;
                    }
                    blinktime += Time.deltaTime;
                }
                else
                {
                    invisible = false;
                    blinktime = 0;
                    partsvisible = true;
                }
            }
        }
    }

    protected virtual void FixedUpdate()//変更に備えて定義のみ
    {
        
    }

    public virtual void Damage(int atk, float invisibletime)//被弾時の処理　攻撃側から呼び出す
    {
        if (!invisible)//無敵時間中でなければ処理
        {
            hp -= atk;
            invisible = true;
            if (!dontaddcombo)
            {
                sau.combo += 1;
            }
            if (sau.combo > Gmanager.instance.maxcombo)//連続ヒット数の最大値を更新
            {
                Gmanager.instance.maxcombo = sau.combo;
            }
            if (!dontadddiskspace)
            {
                sau.diskspace += 1;
            }
            sau.combotime = 0;//
            if (hp <= 0)
            {
                partsvisible = true;
                deletestart();
            }
            else
            {
                endinvisibletime = invisibletime + 0.01f;
                this.invisibletime = 0;
            }
        }
    }

    //撃破された時の処理
    public void deletestart()
    {
        anim.Play("deleted");
        deleted = true;
        if (!dontadddeleten)//撃破数スコアの加算
        {
            Gmanager.instance.deleten += 1;
        }
    }

    //animationからオブジェクト削除処理
    public virtual void perioddeletedanim()
    {
        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "activearea")
        {
            sleep = false;
        }
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "activearea")
        {
            sleep = false;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "activearea")
        {
            sleep = true;
            beforesleep = true;
        }
    }
}

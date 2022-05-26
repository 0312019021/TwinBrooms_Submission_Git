using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 ボス敵オブジェクト共通のクラス。各敵固有のクラスに継承して使用。
 最も上の親オブジェクトに付けないとEnemycollisionから参照できない点に注意。
 <被弾処理>
 Enemyクラスの処理に加え、
 hpが0になった際第一形態であれば形態移行イベントの開始、
 第二形態であれば撃破イベントの開始。

 <無敵時間中処理>
 Enemyクラスと共通。
 */


public class Boss : Enemy
{
    public bool ev;
    public int progress;//0:ボス戦前　1:第一形態　2:第二形態
    public int secondhp;

    public bosshpbar hpbar;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Damage(int atk, float invisibletime)
    {
        if (!invisible && !ev)
        {
            hp -= atk;
            invisible = true;
            if (!dontaddcombo)
            {
                sau.combo += 1;
            }
            if (sau.combo > Gmanager.instance.maxcombo)
            {
                Gmanager.instance.maxcombo = sau.combo;
            }
            if (!dontadddiskspace)
            {
                sau.diskspace += 1;
            }
            sau.combotime = 0;
            if (hp <= 0)
            {
                partsvisible = true;
                if (progress == 1)
                {

                    sau.eventstart();
                    hp = secondhp;
                    progress = 2;
                    ev = true;
                    hpbar.startsecondprogress();
                }
                else
                {
                    sau.eventstart();
                    deleted = true;
                    deletestart();

                }
            }
            else
            {
                endinvisibletime = invisibletime + 0.01f;
                base.invisibletime = 0;

            }
        }
    }
}

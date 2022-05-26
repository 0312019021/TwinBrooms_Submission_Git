using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 プレイヤーキャラに付けるクラス。
 ステータス管理と動作の決定を行う。
 ステージ開始時Gmanagerがもつスコア関連の値のリセットも行う。

・待機：入力がされていない時。地上なら立ちモーション、空中なら落下モーションの再生。
・歩行：左右スワイプで歩行。スワイプの長さに応じ移動速度変化。
・ダッシュ：フリックした方向に高速で移動。ジャンプや急降下も可能。空中でのダッシュは一度のみ。
・通常攻撃(地上)：画面タップで攻撃。3連続まで可能。攻撃動作中は入力を受け付けない。硬直時間の終了後、入力に応じた状態に遷移。
・通常攻撃(空中)：空中で画面タップで空中攻撃。着地までモーションを再生し続ける。
※攻撃時は横方向の速度が0になる。ダッシュ中の急停止や狙った場所への着地に利用可能。
・溜め：diskspace(MP)が5以上のときに画面長押しで溜め動作に入る。指を離すと、離した位置に応じたスキルをdiskspaceを消費し発動。
・スキル(複数種):溜め終了時にメソッドが呼び出される。呼び出されるメソッドはint型配列chargemethod内の番号により決まる。
　開発進行による種類の追加が可能。プレイヤーによる使用スキルの選択機能をメインメニューに実装予定。
・怯み：攻撃を受けた際は少しの間操作不能になる。


（初学レベルの頃に書いたコードの中に、animationeventを用い制御した方が簡潔に済み変更も楽になる部分が多々あるため、要改良）
 */



[System.Serializable]
public class int2
{
    public int[] array;
}


public class Sau : MonoBehaviour
{
    //インスペクタ上設定変数
    #region
    public int hp;
    public int combo;//連続攻撃回数のカウント
    public int diskspace;//攻撃時増加、被ダメ時とスキル使用時減少　MPのようなもの
    public float walkcoefficient;//歩行速度定数
    public float maxwalkspeed;
    public float longtaptime;//長押し判定までの時間
    public float slidelength;//スワイプ・フリック判定されるタップ開始位置から現在位置までの長さ
    public float dashspeed;
    public float dashtimelim;
    public float enddashspeed_x;
    public float fallspeed;
    public float maxfallspeed;
    public float ga;//重力加速度
    public float endsqueezetime;//被弾時怯みモーションの長さ
    public float endinvisibletime;//無敵時間の長さ
    public int[] atk;//各攻撃のダメージ 0:通常1　1:通常2　2:通常3
    public float[] inv;//各攻撃ヒット時の相手の無敵時間
    public float[] attackendtime;//各攻撃の動作時間　1:通常1 2:通常2　3:通常3
    public float[] chargesteptime;//チャージ段階が進む時間
    public float beforegameover;//hpが0になってからゲームオーバー演出までの時間
    public int[] chargeatkcost;//チャージ攻撃に必要なdiskspace数
    public int2[] chargemethod;//方向、溜め段階による発動スキルの指定
    public int2[] chargeatkmove;//各スキル動作中の運動量の指定
    [Header("イベント進行状況　編集しない")] public int eventno;
    public float attackmotionendtime;//攻撃時の、攻撃判定を非アクティブにしてからモーションを自動解除するまでの時間
    [Header("コンボ開始からの時間　編集しない")] public float combotime;
    public float comboendtime;//combo値リセットまでの猶予時間
    public float maxchargetime;
    [Header("イベント中かの判定　編集しない")] public bool eventmotion;
    public bool isgroundforev;//接地判定　イベント用にpublic

    #endregion

    //スクリプト内変数
    #region
    private bool aftertaped = false;
    private bool isGround = true;
    private bool beforeisGround = true;
    private bool falling = false;
    private bool invisible = false;
    private bool gameover = false;
    private bool rigor = false;//硬直
    private bool tapaftercharge = false;//チャージ後の長押し
    private bool attackcut=false;
    private bool attackend = false;
    private bool inchargeL = false;
    private float x_speed = 0;
    private float y_speed = 0;
    private float x_dashspeed = 0;
    private float y_dashspeed = 0;
    private float x_angle = 0;
    private float y_angle = 0;
    private float walkspeed = 0;
    private float tappingtime = 0;
    private float poslength;
    private float swiperad = 0;
    private float dashtime;
    private float falltime;
    private float squeezetime;
    private float invisibletime;
    private float blinktime = 0;
    private float attacklim = 0;
    private float attacktime = 0;
    private float chargetime = 0;
    private float gameovertime = 0;
    private int dashcount = 1;

    private int doing = 0;//状態 0:立ち　1:歩き　2:ダッシュ 3:攻撃硬直中 4:攻撃後モーション中 5:チャージ 6:スキル 7:のけぞり
    private int attackdoing = 0;//どの攻撃か　0:攻撃してない　1:1段　2:2段　3:3段　4:空中　5:gsmash
    private int direction = 0;//チャージ攻撃時の方向判定　0:N 1:上　2:右　3:左　4:下
    private int chargestep = 0;//チャージ段階

    private int lr = 1;//向き　-1:左　1:右

    private bool taped = false;
    Vector2 startPos;//クリック時座標
    Vector2 nowPos;//クリック・ドラッグ中座標
    Vector2 endPos;//解クリック時座標
    Vector2 relativePos;//相対座標


    #endregion

    //インスタンス取得用変数
    #region
    public Animator anim = null;
    private Rigidbody2D rb = null;
    public GroundCheck ground;
    //public GroundCheck front;
    public Sauattack broom_R;
    public Sauattack broomair_R;
    public Sauattack broomair_L;
    //public Sauattack at1sc;
    //public Sauattack at2sc;
    //public Sauattack at3sc;
    //public Sauattack atairsc;
    public Sauattack gsmashsc;
    public Sauattack pencilsc;
    public GameObject tebbuttprefab;
    public Sauattack jupitersc;
    public GameObject gameoverimg;
    private GameObject nowteb;
    public GameObject sprite;
    public GameObject chargeparticle;
    public GameObject invisibleparticle;
    public Animator skilviewanim;
    //private SpriteRenderer sr = null;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //インスタンス取得
        #region
        //anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //sr = GetComponent<SpriteRenderer>();

        #endregion

        //攻撃判定を非アクティブに
        broom_R.active = false;
        broomair_L.active = false;
        broomair_R.active = false;
        

        eventmotion = false;
        eventno = 0;

        Gmanager.instance.playingstage = SceneManager.GetActiveScene().name;
        Gmanager.instance.deleten = 0;
        Gmanager.instance.clearsec = 0;
        Gmanager.instance.clearmin = 0;
        Gmanager.instance.damagen = 0;
        Gmanager.instance.maxcombo = 0;

    }

    private void Update()
    {
        if (gameover && gameovertime < beforegameover)
        {
            //sr.enabled = true;
            gameovertime += Time.deltaTime;
            if (gameovertime > beforegameover)
            {
                gameoverimg.SetActive(true);
            }
        }
        
        if (!eventmotion)//タイム計測
        {
            Gmanager.instance.clearsec += Time.deltaTime;
            if (Gmanager.instance.clearsec >= 60)
            {
                Gmanager.instance.clearmin++;
                Gmanager.instance.clearsec -= 60;

            }

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //接地判定の更新
        beforeisGround = isGround;
        isGround = ground.IsGround();
        isgroundforev = isGround;

        if (isGround&&dashcount!=1)//接地している場合ダッシュ回数をリセット
        {
            dashcount = 1;
        }

        if (invisible)//無敵時間中処理
        {
            invisibletime += Time.deltaTime;
            if (invisibletime > endinvisibletime)
            {
                invisible = false;
                invisibleparticle.SetActive(false);
            }
        }

        if (eventmotion)//イベント中なら操作をリセット
        {
            taped = false;
            aftertaped = false;
            tappingtime = 0;
        }
        else//イベント中でない場合
        {
            if (combo != 0 && doing != 5)//コンボ時間の加算
            {
                combotime += Time.deltaTime;
                if (combotime > comboendtime)
                {
                    combo = 0;
                    diskspace = 0;
                }
            }

            checkoperation();//操作状態の更新

            //操作に応じステートを変更
            if (doing == 1)
            {
                if (!taped)//歩行中に指が離れた
                {
                    transstand();
                    Debug.Log("walkおわり");
                }
                
            }else if (doing == 5)
            {
                if (!taped)
                {
                    chargeend();
                }
            }
            else if (!rigor&&!tapaftercharge)
            {
                if (taped)//指が触れている
                {
                    if (tappingtime > longtaptime)
                    {
                        if ((poslength > slidelength) && isGround)//スワイプかつ接地
                        {
                            transwalk();
                        }
                        else if (diskspace >= chargeatkcost[0])//単長押しかつ溜め攻撃可能
                        {
                            chargestart();
                        }
                        else//diskspaceが不足時に長押し
                        {

                        }
                    }
                    else//押されてから短時間
                    {
                        
                    }
                }
                else if (aftertaped)//指離した直後
                {
                    if (tappingtime > longtaptime)//diskspaceが不足しているときに長押しした場合ここを通る
                    {
                        
                    }
                    else if (poslength > slidelength && dashcount > 0)//フリックかつダッシュ回数がある
                    {
                        transdash();
                    }
                    else//タップ
                    {
                        transattack();
                        /*
                        if (attackdoing<3)
                        {
                            transattack();
                        }
                        */
                    }
                }
                else//操作なし
                {
                    
                }
            }
            else
            {
                
            }
            switch (doing)
            {
                case 0: stand(); break;
                case 1: walk(); break;
                case 2: dash(); break;
                case 3: attack(); break;
                case 4: attackafter(); break;
                case 5: charge(); break;
                case 6: skill(); break;
                case 7: attacked(); break;
            }
        }
    }

    public void transstand()
    {
        doing = 0;
        if (isGround)
        {
            anim.SetTrigger("stand");
        }
        else
        {
            anim.SetTrigger("fall");
        }
    }

    public void stand()
    {
        if (!isGround)//否接地時落下モーション
        {
            if (beforeisGround)
            {
                anim.SetTrigger("fall");
            }
            rb.velocity = new Vector2(rb.velocity.x, getfallspeed());
        }
        else//接地時x方向の慣性をリセット
        {
            if (!beforeisGround)
            {
                anim.SetTrigger("stand");
            }
            falling = false;
            rb.velocity = new Vector2(0, 0);
        }
    }

    

    private float getfallspeed()
    {
        float n;
        if (!falling)//直前まで重力に影響されない行動中だった場合
        {
            falltime = 0;//落下開始　速度を初期化
            falling = true;
        }
        falltime += Time.deltaTime;
        n = falltime * ga + fallspeed;
        if (n < maxfallspeed)
        {
            n = maxfallspeed;
        }

        return n;
    }

    

    public void transwalk()
    {
        doing = 1;
        if (attackdoing != 0)
        {
            attackdoing = 0;
        }
        //この下いらないかも　要確認
        if (startPos.x > nowPos.x)//左スワイプ
        {
            lr = -1;
        }
        else//右スワイプ
        {
            lr = 1;
        }
        transform.localScale = new Vector3(lr, 1, 1);//向きの設定
        anim.SetTrigger("walk");
        Debug.Log("transwalk");
    }

    public void walk()
    {
        if (startPos.x > nowPos.x)//左スワイプ
        {
            lr = -1;
        }
        else//右スワイプ
        {
            lr = 1;
        }
        transform.localScale = new Vector3(lr, 1, 1);//向きの設定
        walkspeed = (nowPos.x - startPos.x) * walkcoefficient;//スワイプ長さ*定数
                                                              //上限を超えたら
        if (walkspeed > maxwalkspeed)
        {
            walkspeed = maxwalkspeed;
        }
        else if (0 > walkspeed && -walkspeed > maxwalkspeed)
        {
            walkspeed = -maxwalkspeed;
        }
        rb.velocity = new Vector2(walkspeed, getfallspeed());
    }

    private void chargestart()
    {
        Debug.Log("cst");
        if (attackdoing != 0)
        {
            attackdoing = 0;
        }

        if (diskspace >= chargeatkcost[0])
        {
            doing = 5;
            chargetime = 0;
            rb.velocity = new Vector2(0, 0);
            anim.SetTrigger("charging");
            chargeparticle.SetActive(true);
            skilviewanim.SetTrigger("center");
            if (transform.localScale.x == 1&& inchargeL)
            {
                inchargeL = false;
            }
            else if(transform.localScale.x == -1 && !inchargeL)
            {
                inchargeL = true;
            }
            Debug.Log(inchargeL);
        }
        else
        {
            //doing = 0;
        }
    }

    private void charge()
    {
        chargetime += Time.deltaTime;
        if (chargetime < maxchargetime)
        {
            checkchargedirection(true);
        }
        if (chargetime > maxchargetime)
        {
            tapaftercharge = true;
            chargeend();
        }
    }

    private void chargeend()
    {
        chargeparticle.SetActive(false);
        checkchargedirection(false);
        skilviewanim.SetTrigger("idle");

        //横方向スキルの場合使用する向きを設定
        if (direction == 2)
        {
            if (inchargeL)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }

        //チャージ段階の判定
        if (chargetime > chargesteptime[1] && diskspace >= chargeatkcost[2])
        {
            chargestep = 2;
            diskspace -= chargeatkcost[2];
        }
        else if (chargetime > chargesteptime[0] && diskspace >= chargeatkcost[1])
        {
            chargestep = 1;
            diskspace -= chargeatkcost[1];
        }
        else
        {
            chargestep = 0;
            diskspace -= chargeatkcost[0];
        }

        //攻撃メソッドの呼び出し
        switch (chargemethod[direction].array[chargestep])
        {
            case 1: gsmashst(); break;
            case 2: pencilst(); break;
            case 3: tebbuttst(); break;
            case 4: jupiterst(); break;
            default: break;
        }
    }

    private void checkchargedirection(bool animset)//チャージ中のスワイプ向き判定
    {
        if (poslength < slidelength)//N
        {
            if (direction != 0)
            {
                skilviewanim.SetTrigger("center");
                direction = 0;
            }
        }
        else
        {
            getflickangle();
            x_angle = Mathf.Cos(swiperad);
            y_angle = Mathf.Sin(swiperad);
            if (y_angle > 0.5)//上向き
            {
                if (direction != 1)
                {
                    if (animset)
                    {
                        skilviewanim.SetTrigger("up");
                    }
                    direction = 1;
                }
            }
            else if (y_angle > -0.5)//横向き
            {
                if (direction != 2)//他方向から横向きになった瞬間
                {
                    direction = 2;
                    if (x_angle > 0)//右向き
                    {
                        if (inchargeL)
                        {
                            inchargeL = false;
                        }
                        if (animset)
                        {
                            skilviewanim.SetTrigger("right");
                        }
                    }
                    else//左向き
                    {
                        if (!inchargeL)
                        {
                            inchargeL = true;
                        }
                        if (animset)
                        {
                            skilviewanim.SetTrigger("left");
                        }
                    }
                }
                else//前から横向きだったとき(左から右、右から左を含む)
                {
                    if (x_angle > 0 && inchargeL)//左から右向き
                    {
                        inchargeL = false;
                        if (animset)
                        {
                            skilviewanim.SetTrigger("right");
                        }
                    }
                    else if (x_angle <= 0 && !inchargeL)//右から左向き
                    {
                        inchargeL = true;
                        if (animset)
                        {
                            skilviewanim.SetTrigger("left");
                        }
                    }
                }
            }
            else//下向き
            {
                if (direction != 3)
                {
                    if (animset)
                    {
                        skilviewanim.SetTrigger("down");
                    }
                    direction = 3;
                }
            }
        }
    }

    private void transdash()
    {
        falling = false;
        doing = 2;
        if (attackdoing != 0)
        {
            attackdoing = 0;
        }
        dashtime = 0;
        if (!isGround)
        {
            dashcount -= 1;
        }
        getflickangle();
        x_dashspeed = dashspeed * Mathf.Cos(swiperad);
        y_dashspeed = dashspeed * Mathf.Sin(swiperad);
        if (y_dashspeed > dashspeed / 2)//上向き
        {
            anim.SetTrigger("dash0");
        }
        else if (y_dashspeed > -(dashspeed / 2))//横向き
        {
            anim.SetTrigger("dash3");
        }
        else//下向き
        {
            anim.SetTrigger("dash6");
        }
        if (x_dashspeed > 0)//右向き
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else//左向き
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void dash()
    {
        if (dashtime < dashtimelim)//ダッシュの時間を過ぎていなければ
        {
            if (!beforeisGround && isGround)//着地した瞬間ダッシュ中止
            {
                transstand();
            }
            else
            {
                dashtime += Time.deltaTime;//経過時間
                rb.velocity = new Vector2(x_dashspeed, y_dashspeed);//ダッシュ速度を代入
            }
        }
        else//一定時間移動していれば
        {

            //最後にダッシュ直後の速度を代入して終了
            if (isGround)
            {
                rb.velocity = new Vector2(0.0f, 0.0f);
            }
            else
            {
                rb.velocity = new Vector2(enddashspeed_x * x_dashspeed, 0);
            }
            transstand();
        }
    }

    private void transattack()
    {
        doing = 3;
        rigor = true;
        if (attackcut) { 
            attackcut = false; 
        }
        if (attackend)
        {
            attackend = false;
        }
        if (!isGround)//空中
        {
            attackdoing = 4;
        }
        else
        {
            if (attackdoing != 3)
            {
                attackdoing += 1;
                attacklim = attackendtime[attackdoing];
            }
            else
            {
                attackdoing = 1;
            }
        }
        attacktime = 0;
        rb.velocity = new Vector2(0, 0);
        broom_R.atk = atk[attackdoing-1];
        broom_R.invisibletime = inv[attackdoing-1];

        switch (attackdoing)
        {
            case 1: anim.SetTrigger("attack1"); broom_R.active = true; break;
            case 2: anim.SetTrigger("attack2"); broom_R.active = true; break;
            case 3: anim.SetTrigger("attack3"); broom_R.active = true; break;
            case 4: anim.SetTrigger("attackair"); broomair_L.active = true; broomair_R.active = true; break;
            default: break;
        }
    }


    private void attack()
    {
        if (attackdoing == 4)//空中攻撃中
        {
            rb.velocity = new Vector2(rb.velocity.x, getfallspeed());
            if (isGround)
            {
                broomair_L.active = false;
                broomair_R.active = false;
                attackdoing = 0;
                rigor = false;
                transstand();
            }
            else//操作受付？
            {

            }
        }
        else//攻撃中
        {
            if (attackcut)//攻撃硬直の終わり
            {
                Debug.Log("attackcut");
                switch (attackdoing)//攻撃判定を消す
                {
                    case 1: broom_R.active = false; break;
                    case 2: broom_R.active = false; break;
                    case 3: broom_R.active = false; break;

                    case 5: attackdoing = 0; break;
                    case 6: pencilsc.active = false;  attackdoing = 0; break;
                    case 7:  attackdoing = 0; break;
                    case 8: jupitersc.active = false;  attackdoing = 0; break;
                }
                tappingtime = 0.0f;
                rigor = false;
                doing = 4;//攻撃後ステートへ
            }
        }
    }

    private void attackafter()
    {
        if (attackend)
        {
            Debug.Log("attackend");
            attackdoing = 0;
            transstand();
        }
    }

    public void attackanimcut()//攻撃animationeventから攻撃判定の削除
    {
        attackcut = true;
    }

    public void attackanimend()//攻撃animationeventから攻撃モーションの終了
    {
        attackend = true;
    }

    public void getflickangle()//スワイプ時角度計算
    //https://zenn.dev/daichi_gamedev/articles/9c34ffbf52d056
    {
        relativePos = nowPos - startPos;
        swiperad = Mathf.Atan2(relativePos.y, relativePos.x);
    }

    private void attacked()//被弾中に呼び出される処理
    {
        taped = false;
        if (squeezetime < endsqueezetime)//のけぞり時間を超過していない場合
        {
            if (!isGround)//否接地時落下
            {
                rb.velocity = new Vector2(rb.velocity.x, getfallspeed());
            }
            else//接地時x方向の慣性をリセット
            {
                falling = false;
                rb.velocity = new Vector2(0, 0);
            }
            squeezetime += Time.deltaTime;
        }
        else
        {
            if (!eventmotion)
            {
                rigor = false;
                transstand();
            }
        }

    }

    public void resetanim()//animのパラメータをリセット
    {
        anim.SetTrigger("stand");
    }

    private void resetattack()
    {
        attackdoing = 0;
        broom_R.active = false;
        broomair_L.active = false;
        broomair_R.active = false;
        pencilsc.active = false;
        jupitersc.active = false;
    }

    public void damage(int at)
    {
        if (!invisible && !eventmotion)
        {
            if (doing == 5)
            {
                chargeparticle.SetActive(false);
            }
            doing = 7;
            rigor = true;
            hp -= at;
            Gmanager.instance.damagen -= at;
            combo = 0;
            diskspace = 0;
            resetattack();
            anim.SetTrigger("damage");
            squeezetime = 0;
            rb.velocity = new Vector2(0, 0);
            if (hp > 0)
            {
                invisible = true;
                invisibleparticle.SetActive(true);
                invisibletime = 0;
            }
            else//ゲームオーバー処理
            {
                hp = 0;
                gameover = true;
                endsqueezetime = 1000000;
            }
        }

    }

    private void beforeskil()//スキル開始時のアニメーションリセット、硬直時間の設定、攻撃時間の初期化、行動フラグをスキル中に
    {
        if (!eventmotion)
        {
            doing = 6;
        }
        attacklim = attackendtime[attackdoing];
        attacktime = 0;
    }

    private void skill()
    {
        switch (attackdoing)
        {
            case 5: gsmash(); break;
            case 6: pencil(); break;
            case 7: tebbutt(); break;
            case 8: jupiter(); break;
        }
    }

    //この下しばらくスキル群
    /*
     スキルのアニメーションパラメータをtrueにしたフレームで
    同時にchargingをfalseにしてしまうと
    遷移がうまくいかないため、
    スキルアニメーションをfalseにするタイミングでchargingをfalseに
     */

    private void gsmashst()
    {
        attackdoing = 5;
        beforeskil();
        anim.SetTrigger("gsmash");
        setrbroom();
    }

    private void gsmash()
    {
        attacktime += Time.deltaTime;
        if (attacktime > attacklim)
        {
            attackdoing = 0;
            broom_R.active = false;
            transstand();
        }
    }

    private void pencilst()
    {
        attackdoing = 6;
        beforeskil();
        anim.SetTrigger("pencil");
        pencilsc.active = true;
    }

    private void pencil()
    {
        attacktime += Time.deltaTime;
        rb.velocity = new Vector2(chargeatkmove[2].array[0] * transform.localScale.x, chargeatkmove[2].array[1]);
        if (attacktime > attacklim)
        {
            pencilsc.active = false;
            attackdoing = 0;
            transstand();
        }
    }

    private void tebbuttst()
    {
        attackdoing = 7;
        beforeskil();
        anim.SetTrigger("tebbutt");
        nowteb = Instantiate(tebbuttprefab, this.transform.position, Quaternion.identity);
        nowteb.transform.localScale = this.transform.localScale;//生成後に向きを変更
    }

    private void tebbutt()
    {
        attacktime += Time.deltaTime;
        if (attacktime > attacklim)
        {
            attackdoing = 0;
            transstand();
        }
    }

    private void jupiterst()
    {
        attackdoing = 8;
        beforeskil();
        anim.SetTrigger("jupiter");
        jupitersc.active = true;
    }

    private void jupiter()
    {
        attacktime += Time.deltaTime;
        if (attacktime > attacklim)
        {
            jupitersc.active = false;
            attackdoing = 0;
            transstand();
        }
    }

    public void eventstart()
    {
        doing = 0;
        resetattack();
        rigor = false;
        //rb.velocity = new Vector2(0, 0);
        //anim.SetTrigger("stand");
        eventno += 1;
        eventmotion = true;
    }

    public void eventplaying()
    {

    }

    public void eventwalk(int a, float b)
    {
        attackdoing = 0;
        transform.localScale = new Vector3(a, 1, 1);
        walkspeed = b * walkcoefficient;
        if (walkspeed > maxwalkspeed)
        {
            walkspeed = maxwalkspeed;
        }
        else if (0 > walkspeed && -walkspeed > maxwalkspeed)
        {
            walkspeed = -maxwalkspeed;
        }
        rb.velocity = new Vector2(walkspeed, getfallspeed());//x速度をリセット
    }

    public void eventstand()
    {
        if (isGround)
        {
            anim.SetTrigger("stand");
        }
        else
        {
            anim.SetTrigger("fall");
        }
        rb.velocity = new Vector2(0, getfallspeed());
    }



    private void checkoperation()
    {
        if (Input.GetMouseButton(0))//画面に指が触れている
        {
            if (!taped)//タップされた瞬間
            {
                taped = true;
                aftertaped = false;
                tappingtime = 0;
                startPos = Input.mousePosition;
                nowPos = Input.mousePosition;
            }
            else//前fixedからタップ
            {
                tappingtime += Time.deltaTime;
                nowPos = Input.mousePosition;
                poslength = (startPos.x - nowPos.x) * (startPos.x - nowPos.x) + (startPos.y - nowPos.y) * (startPos.y - nowPos.y);
            }
        }
        else//画面に指が触れていない
        {
            if (taped)//離した瞬間
            {
                nowPos = Input.mousePosition;
                //タップ位置と離した位置の距離
                poslength = (startPos.x - nowPos.x) * (startPos.x - nowPos.x) + (startPos.y - nowPos.y) * (startPos.y - nowPos.y);
                if (tapaftercharge)//溜め後指を離したら操作受付再開
                {
                    tapaftercharge = false;
                }
                taped = false;
                aftertaped = true;
            }
            else
            {
                aftertaped = false;
            }
        }
    }

    private void setrbroom() {
        broom_R.active = true;
        broom_R.atk = atk[attackdoing - 1];
        broom_R.invisibletime = inv[attackdoing - 1];
    }

}
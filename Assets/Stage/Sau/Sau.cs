using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 �v���C���[�L�����ɕt����N���X�B
 �X�e�[�^�X�Ǘ��Ɠ���̌�����s���B
 �X�e�[�W�J�n��Gmanager�����X�R�A�֘A�̒l�̃��Z�b�g���s���B

�E�ҋ@�F���͂�����Ă��Ȃ����B�n��Ȃ痧�����[�V�����A�󒆂Ȃ痎�����[�V�����̍Đ��B
�E���s�F���E�X���C�v�ŕ��s�B�X���C�v�̒����ɉ����ړ����x�ω��B
�E�_�b�V���F�t���b�N���������ɍ����ňړ��B�W�����v��}�~�����\�B�󒆂ł̃_�b�V���͈�x�̂݁B
�E�ʏ�U��(�n��)�F��ʃ^�b�v�ōU���B3�A���܂ŉ\�B�U�����쒆�͓��͂��󂯕t���Ȃ��B�d�����Ԃ̏I����A���͂ɉ�������ԂɑJ�ځB
�E�ʏ�U��(��)�F�󒆂ŉ�ʃ^�b�v�ŋ󒆍U���B���n�܂Ń��[�V�������Đ���������B
���U�����͉������̑��x��0�ɂȂ�B�_�b�V�����̋}��~��_�����ꏊ�ւ̒��n�ɗ��p�\�B
�E���߁Fdiskspace(MP)��5�ȏ�̂Ƃ��ɉ�ʒ������ŗ��ߓ���ɓ���B�w�𗣂��ƁA�������ʒu�ɉ������X�L����diskspace����������B
�E�X�L��(������):���ߏI�����Ƀ��\�b�h���Ăяo�����B�Ăяo����郁�\�b�h��int�^�z��chargemethod���̔ԍ��ɂ�茈�܂�B
�@�J���i�s�ɂ���ނ̒ǉ����\�B�v���C���[�ɂ��g�p�X�L���̑I���@�\�����C�����j���[�Ɏ����\��B
�E���݁F�U�����󂯂��ۂ͏����̊ԑ���s�\�ɂȂ�B


�i���w���x���̍��ɏ������R�[�h�̒��ɁAanimationevent��p�����䂵�������Ȍ��ɍςݕύX���y�ɂȂ镔�������X���邽�߁A�v���ǁj
 */



[System.Serializable]
public class int2
{
    public int[] array;
}


public class Sau : MonoBehaviour
{
    //�C���X�y�N�^��ݒ�ϐ�
    #region
    public int hp;
    public int combo;//�A���U���񐔂̃J�E���g
    public int diskspace;//�U���������A��_�����ƃX�L���g�p�������@MP�̂悤�Ȃ���
    public float walkcoefficient;//���s���x�萔
    public float maxwalkspeed;
    public float longtaptime;//����������܂ł̎���
    public float slidelength;//�X���C�v�E�t���b�N���肳���^�b�v�J�n�ʒu���猻�݈ʒu�܂ł̒���
    public float dashspeed;
    public float dashtimelim;
    public float enddashspeed_x;
    public float fallspeed;
    public float maxfallspeed;
    public float ga;//�d�͉����x
    public float endsqueezetime;//��e�����݃��[�V�����̒���
    public float endinvisibletime;//���G���Ԃ̒���
    public int[] atk;//�e�U���̃_���[�W 0:�ʏ�1�@1:�ʏ�2�@2:�ʏ�3
    public float[] inv;//�e�U���q�b�g���̑���̖��G����
    public float[] attackendtime;//�e�U���̓��쎞�ԁ@1:�ʏ�1 2:�ʏ�2�@3:�ʏ�3
    public float[] chargesteptime;//�`���[�W�i�K���i�ގ���
    public float beforegameover;//hp��0�ɂȂ��Ă���Q�[���I�[�o�[���o�܂ł̎���
    public int[] chargeatkcost;//�`���[�W�U���ɕK�v��diskspace��
    public int2[] chargemethod;//�����A���ߒi�K�ɂ�锭���X�L���̎w��
    public int2[] chargeatkmove;//�e�X�L�����쒆�̉^���ʂ̎w��
    [Header("�C�x���g�i�s�󋵁@�ҏW���Ȃ�")] public int eventno;
    public float attackmotionendtime;//�U�����́A�U��������A�N�e�B�u�ɂ��Ă��烂�[�V������������������܂ł̎���
    [Header("�R���{�J�n����̎��ԁ@�ҏW���Ȃ�")] public float combotime;
    public float comboendtime;//combo�l���Z�b�g�܂ł̗P�\����
    public float maxchargetime;
    [Header("�C�x���g�����̔���@�ҏW���Ȃ�")] public bool eventmotion;
    public bool isgroundforev;//�ڒn����@�C�x���g�p��public

    #endregion

    //�X�N���v�g���ϐ�
    #region
    private bool aftertaped = false;
    private bool isGround = true;
    private bool beforeisGround = true;
    private bool falling = false;
    private bool invisible = false;
    private bool gameover = false;
    private bool rigor = false;//�d��
    private bool tapaftercharge = false;//�`���[�W��̒�����
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

    private int doing = 0;//��� 0:�����@1:�����@2:�_�b�V�� 3:�U���d���� 4:�U���ヂ�[�V������ 5:�`���[�W 6:�X�L�� 7:�̂�����
    private int attackdoing = 0;//�ǂ̍U�����@0:�U�����ĂȂ��@1:1�i�@2:2�i�@3:3�i�@4:�󒆁@5:gsmash
    private int direction = 0;//�`���[�W�U�����̕�������@0:N 1:��@2:�E�@3:���@4:��
    private int chargestep = 0;//�`���[�W�i�K

    private int lr = 1;//�����@-1:���@1:�E

    private bool taped = false;
    Vector2 startPos;//�N���b�N�����W
    Vector2 nowPos;//�N���b�N�E�h���b�O�����W
    Vector2 endPos;//���N���b�N�����W
    Vector2 relativePos;//���΍��W


    #endregion

    //�C���X�^���X�擾�p�ϐ�
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
        //�C���X�^���X�擾
        #region
        //anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //sr = GetComponent<SpriteRenderer>();

        #endregion

        //�U��������A�N�e�B�u��
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
        
        if (!eventmotion)//�^�C���v��
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
        //�ڒn����̍X�V
        beforeisGround = isGround;
        isGround = ground.IsGround();
        isgroundforev = isGround;

        if (isGround&&dashcount!=1)//�ڒn���Ă���ꍇ�_�b�V���񐔂����Z�b�g
        {
            dashcount = 1;
        }

        if (invisible)//���G���Ԓ�����
        {
            invisibletime += Time.deltaTime;
            if (invisibletime > endinvisibletime)
            {
                invisible = false;
                invisibleparticle.SetActive(false);
            }
        }

        if (eventmotion)//�C�x���g���Ȃ瑀������Z�b�g
        {
            taped = false;
            aftertaped = false;
            tappingtime = 0;
        }
        else//�C�x���g���łȂ��ꍇ
        {
            if (combo != 0 && doing != 5)//�R���{���Ԃ̉��Z
            {
                combotime += Time.deltaTime;
                if (combotime > comboendtime)
                {
                    combo = 0;
                    diskspace = 0;
                }
            }

            checkoperation();//�����Ԃ̍X�V

            //����ɉ����X�e�[�g��ύX
            if (doing == 1)
            {
                if (!taped)//���s���Ɏw�����ꂽ
                {
                    transstand();
                    Debug.Log("walk�����");
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
                if (taped)//�w���G��Ă���
                {
                    if (tappingtime > longtaptime)
                    {
                        if ((poslength > slidelength) && isGround)//�X���C�v���ڒn
                        {
                            transwalk();
                        }
                        else if (diskspace >= chargeatkcost[0])//�P�����������ߍU���\
                        {
                            chargestart();
                        }
                        else//diskspace���s�����ɒ�����
                        {

                        }
                    }
                    else//������Ă���Z����
                    {
                        
                    }
                }
                else if (aftertaped)//�w����������
                {
                    if (tappingtime > longtaptime)//diskspace���s�����Ă���Ƃ��ɒ����������ꍇ������ʂ�
                    {
                        
                    }
                    else if (poslength > slidelength && dashcount > 0)//�t���b�N���_�b�V���񐔂�����
                    {
                        transdash();
                    }
                    else//�^�b�v
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
                else//����Ȃ�
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
        if (!isGround)//�ېڒn���������[�V����
        {
            if (beforeisGround)
            {
                anim.SetTrigger("fall");
            }
            rb.velocity = new Vector2(rb.velocity.x, getfallspeed());
        }
        else//�ڒn��x�����̊��������Z�b�g
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
        if (!falling)//���O�܂ŏd�͂ɉe������Ȃ��s�����������ꍇ
        {
            falltime = 0;//�����J�n�@���x��������
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
        //���̉�����Ȃ������@�v�m�F
        if (startPos.x > nowPos.x)//���X���C�v
        {
            lr = -1;
        }
        else//�E�X���C�v
        {
            lr = 1;
        }
        transform.localScale = new Vector3(lr, 1, 1);//�����̐ݒ�
        anim.SetTrigger("walk");
        Debug.Log("transwalk");
    }

    public void walk()
    {
        if (startPos.x > nowPos.x)//���X���C�v
        {
            lr = -1;
        }
        else//�E�X���C�v
        {
            lr = 1;
        }
        transform.localScale = new Vector3(lr, 1, 1);//�����̐ݒ�
        walkspeed = (nowPos.x - startPos.x) * walkcoefficient;//�X���C�v����*�萔
                                                              //����𒴂�����
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

        //�������X�L���̏ꍇ�g�p���������ݒ�
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

        //�`���[�W�i�K�̔���
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

        //�U�����\�b�h�̌Ăяo��
        switch (chargemethod[direction].array[chargestep])
        {
            case 1: gsmashst(); break;
            case 2: pencilst(); break;
            case 3: tebbuttst(); break;
            case 4: jupiterst(); break;
            default: break;
        }
    }

    private void checkchargedirection(bool animset)//�`���[�W���̃X���C�v��������
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
            if (y_angle > 0.5)//�����
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
            else if (y_angle > -0.5)//������
            {
                if (direction != 2)//���������牡�����ɂȂ����u��
                {
                    direction = 2;
                    if (x_angle > 0)//�E����
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
                    else//������
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
                else//�O���牡�����������Ƃ�(������E�A�E���獶���܂�)
                {
                    if (x_angle > 0 && inchargeL)//������E����
                    {
                        inchargeL = false;
                        if (animset)
                        {
                            skilviewanim.SetTrigger("right");
                        }
                    }
                    else if (x_angle <= 0 && !inchargeL)//�E���獶����
                    {
                        inchargeL = true;
                        if (animset)
                        {
                            skilviewanim.SetTrigger("left");
                        }
                    }
                }
            }
            else//������
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
        if (y_dashspeed > dashspeed / 2)//�����
        {
            anim.SetTrigger("dash0");
        }
        else if (y_dashspeed > -(dashspeed / 2))//������
        {
            anim.SetTrigger("dash3");
        }
        else//������
        {
            anim.SetTrigger("dash6");
        }
        if (x_dashspeed > 0)//�E����
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else//������
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void dash()
    {
        if (dashtime < dashtimelim)//�_�b�V���̎��Ԃ��߂��Ă��Ȃ����
        {
            if (!beforeisGround && isGround)//���n�����u�ԃ_�b�V�����~
            {
                transstand();
            }
            else
            {
                dashtime += Time.deltaTime;//�o�ߎ���
                rb.velocity = new Vector2(x_dashspeed, y_dashspeed);//�_�b�V�����x����
            }
        }
        else//��莞�Ԉړ����Ă����
        {

            //�Ō�Ƀ_�b�V������̑��x�������ďI��
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
        if (!isGround)//��
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
        if (attackdoing == 4)//�󒆍U����
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
            else//�����t�H
            {

            }
        }
        else//�U����
        {
            if (attackcut)//�U���d���̏I���
            {
                Debug.Log("attackcut");
                switch (attackdoing)//�U�����������
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
                doing = 4;//�U����X�e�[�g��
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

    public void attackanimcut()//�U��animationevent����U������̍폜
    {
        attackcut = true;
    }

    public void attackanimend()//�U��animationevent����U�����[�V�����̏I��
    {
        attackend = true;
    }

    public void getflickangle()//�X���C�v���p�x�v�Z
    //https://zenn.dev/daichi_gamedev/articles/9c34ffbf52d056
    {
        relativePos = nowPos - startPos;
        swiperad = Mathf.Atan2(relativePos.y, relativePos.x);
    }

    private void attacked()//��e���ɌĂяo����鏈��
    {
        taped = false;
        if (squeezetime < endsqueezetime)//�̂����莞�Ԃ𒴉߂��Ă��Ȃ��ꍇ
        {
            if (!isGround)//�ېڒn������
            {
                rb.velocity = new Vector2(rb.velocity.x, getfallspeed());
            }
            else//�ڒn��x�����̊��������Z�b�g
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

    public void resetanim()//anim�̃p�����[�^�����Z�b�g
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
            else//�Q�[���I�[�o�[����
            {
                hp = 0;
                gameover = true;
                endsqueezetime = 1000000;
            }
        }

    }

    private void beforeskil()//�X�L���J�n���̃A�j���[�V�������Z�b�g�A�d�����Ԃ̐ݒ�A�U�����Ԃ̏������A�s���t���O���X�L������
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

    //���̉����΂炭�X�L���Q
    /*
     �X�L���̃A�j���[�V�����p�����[�^��true�ɂ����t���[����
    ������charging��false�ɂ��Ă��܂���
    �J�ڂ����܂������Ȃ����߁A
    �X�L���A�j���[�V������false�ɂ���^�C�~���O��charging��false��
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
        nowteb.transform.localScale = this.transform.localScale;//������Ɍ�����ύX
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
        rb.velocity = new Vector2(walkspeed, getfallspeed());//x���x�����Z�b�g
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
        if (Input.GetMouseButton(0))//��ʂɎw���G��Ă���
        {
            if (!taped)//�^�b�v���ꂽ�u��
            {
                taped = true;
                aftertaped = false;
                tappingtime = 0;
                startPos = Input.mousePosition;
                nowPos = Input.mousePosition;
            }
            else//�Ofixed����^�b�v
            {
                tappingtime += Time.deltaTime;
                nowPos = Input.mousePosition;
                poslength = (startPos.x - nowPos.x) * (startPos.x - nowPos.x) + (startPos.y - nowPos.y) * (startPos.y - nowPos.y);
            }
        }
        else//��ʂɎw���G��Ă��Ȃ�
        {
            if (taped)//�������u��
            {
                nowPos = Input.mousePosition;
                //�^�b�v�ʒu�Ɨ������ʒu�̋���
                poslength = (startPos.x - nowPos.x) * (startPos.x - nowPos.x) + (startPos.y - nowPos.y) * (startPos.y - nowPos.y);
                if (tapaftercharge)//���ߌ�w�𗣂����瑀���t�ĊJ
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
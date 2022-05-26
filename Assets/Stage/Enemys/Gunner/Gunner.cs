using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner : Enemy
{
    public float speed;
    public float ga;
    public float fallspeed;
    public float maxfallspeed;
    public float waitendtime;
    public float idleendtime;
    public float walkendtime;
    public bool distantsleep;

    private int state;//0:shot 1:walk
    private bool falling;
    private bool between = true;
    private float falltime;
    private bool isground;
    private bool isside;
    private bool isntedge;
    private float xspeed;
    private float yspeed;
    private bool left;
    private int nowbulletno;

    private float waittime;
    private bool walkstart;
    private float walktime;
    private float idletime;

    private Rigidbody2D rb = null;
    public GroundCheck ground;
    public GroundCheck egde;
    public GroundCheck side;
    public GameObject shotpoint;
    [Header("bullets�̗v�f�� ������������")] public int bulletssize;
    public GameObject bullet0;
    public GameObject bullet1;
    public GameObject bullet2;
    public GameObject bullet3;
    public GameObject bullet4;
    public GameObject bullet5;
    public GameObject bullet6;
    private GameObject nowbullet;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed, 0);
        state = 0;
        nowbulletno = 0;
        walkstart = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        isground = ground.IsGround();
        isside = side.IsGround();
        isntedge = egde.IsGround();

        if (!deleted)
        {
            if (!isground)//�󒆂ɂ����痎�����x���o��
            {
                yspeed = getfallspeed();
            }
            else
            {
                yspeed = 0;
            }


            if (sleep && distantsleep && between)
            {
                if (beforesleep)
                {
                    beforesleep = false;
                    resetstatus();
                }
            }
            else
            {
                between = false;
                switch (state)
                {
                    case 0: shot(); break;
                    case 1: walk(); break;
                }
            }
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    private void shot()
    {
        if (waittime > waitendtime)
        {
            if (nowbulletno > bulletssize)//�S�����˂�����ҋ@�A�I���������s�X�e�[�g
            {
                idletime += Time.deltaTime;
                if (idletime > idleendtime)
                {
                    state = 1;
                    walkstart = true;
                    between = true;
                }
            }
            else
            {
                switch (nowbulletno)
                {
                    case 0: nowbullet = Instantiate(bullet0, shotpoint.transform.position, Quaternion.identity); break;
                    case 1: nowbullet = Instantiate(bullet1, shotpoint.transform.position, Quaternion.identity); break;
                    case 2: nowbullet = Instantiate(bullet2, shotpoint.transform.position, Quaternion.identity); break;
                    case 3: nowbullet = Instantiate(bullet3, shotpoint.transform.position, Quaternion.identity); break;
                    case 4: nowbullet = Instantiate(bullet4, shotpoint.transform.position, Quaternion.identity); break;
                    case 5: nowbullet = Instantiate(bullet5, shotpoint.transform.position, Quaternion.identity); break;
                    case 6: nowbullet = Instantiate(bullet6, shotpoint.transform.position, Quaternion.identity); break;
                }
                nowbullet.transform.localScale = -this.transform.localScale;//������Ɍ�����ύX
                waittime = 0.0f;
                nowbulletno += 1;
            }

        }
        else//�ˌ���͂��΂炭�ҋ@
        {
            waittime += Time.deltaTime;

        }

        rb.velocity = new Vector2(0, yspeed);
    }

    private void walk()
    {
        if (walkstart)//�J�n���T�E�̕�������
        {
            if (sau.transform.position.x > this.transform.position.x)
            {
                this.transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                this.transform.localScale = new Vector3(1, 1, 1);
            }
            walkstart = false;
            walktime = 0;
            anim.SetBool("walk", true);
        }


        if (isntedge && !isside)//�R��ǍۂłȂ���Ό��
        {
            xspeed = speed * this.transform.localScale.x;

        }
        else
        {
            xspeed = 0;
        }
        rb.velocity = new Vector2(xspeed, yspeed);

        walktime += Time.deltaTime;
        if (walktime > walkendtime)
        {
            state = 0;
            anim.SetBool("walk", false);
            nowbulletno = 0;
            between = true;
            idletime = 0.0f;
        }

    }

    private void resetstatus()
    {
        rb.velocity = new Vector2(0, 0);
        nowbulletno = 0;
        waittime = 0.0f;
        walkstart = true;
        walktime = 0.0f;
        idletime = 0.0f;
        anim.SetBool("walk", false);
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
        n = (falltime * ga) + fallspeed;
        if (n < maxfallspeed)
        {
            n = maxfallspeed;
        }

        return n;
    }
}

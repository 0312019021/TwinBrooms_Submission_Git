using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspLarvae : Enemy
{
    public float startx;
    public float starty;
    public float speed;
    public float ga;
    public float fallspeed;
    public float maxfallspeed;
    public bool flameoutbreak;
    public bool flameoutrun;
    public bool firstsleep;

    private bool firstfixed = true;
    private bool falling = false;
    private float falltime = 0;
    private bool beforeisground = false;
    private bool isground = false;
    private bool isside = false;
    private bool isedge = false;
    private float xspeed = 0;
    private float yspeed = 0;
    private bool up = false;

    private Rigidbody2D rb = null;
    public GroundCheck ground;
    public GroundCheck side;
    public GroundCheck edge;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        sleep = firstsleep;
        beforesleep = false;
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (firstfixed)//�Ăяo����Ĉ�ԍŏ��ɑ��x�ݒ�@Start�ł��悳����������Gwasp�ł̌����ύX����ɌĂяo�����m�؂������Ȃ������̂�
        {
            rb.velocity = new Vector2(startx * transform.localScale.x, starty);
            firstfixed = false;
        }

        beforeisground = isground;
        isground = ground.IsGround();
        isside = side.IsGround();
        isedge = edge.IsGround();

        if (sleep && !flameoutrun)//�J�����O�̏ꍇ���삵�Ȃ�
        {
            if (beforesleep && flameoutbreak)//�J��������o���u�ԍ폜
            {
                Destroy(gameObject);
            }
            else
            {
                rb.velocity = new Vector2(0, 0);
            }
        }
        else
        {
            if (!deleted)
            {
                if (!up && !isground)//�󒆂ɂ����痎�����x���o��
                {
                    xspeed = rb.velocity.x;
                    yspeed = getfallspeed();
                    if (!beforeisground)
                    {
                        anim.SetTrigger("fall");
                    }

                }
                else
                {

                    if (beforeisground)
                    {
                        anim.SetTrigger("walk");
                    }

                    if (up)//�Ǐ�蒆
                    {
                        if (!isedge)//�オ������
                        {
                            transform.localRotation = Quaternion.Euler(0, 0, 0);
                            up = false;

                        }
                        else if (isside)//�V��ɂ����ꍇ
                        {

                        }
                    }
                    else if (isside)//�Փˎ��Ǐ��
                    {

                        transform.localRotation = Quaternion.Euler(0, 0, 90 * transform.localScale.x * -1);
                        up = true;
                    }

                    if (up)
                    {
                        xspeed = 0;
                        yspeed = speed;
                    }
                    else
                    {
                        xspeed = -speed * transform.localScale.x;
                        yspeed = 0;
                    }

                }


            }
            else
            {
                xspeed = 0;
                yspeed = 0;
            }
            rb.velocity = new Vector2(xspeed, yspeed);
        }
    }

    private float getfallspeed()
    {
        float n = 0;
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

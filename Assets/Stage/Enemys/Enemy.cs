using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 �G�I�u�W�F�N�g���ʂ̃N���X�B�e�G�ŗL�̃N���X�Ɍp�����Ďg�p�B
 �ł���̐e�I�u�W�F�N�g�ɕt���Ȃ���Enemycollision����Q�Ƃł��Ȃ��_�ɒ��ӁB
<��e����>�v���C���[�L�����̍U�����󂯂���hp�����A�e�X�R�A�̒ǉ��A���G���Ԃ̊J�n�B
<���G���Ԓ�����>�p�[�c�摜�̓_�ł�������B
 */


public class Enemy : MonoBehaviour
{
    public bool dontadddeleten;//���j���X�R�A�����Z���Ȃ�
    public bool dontadddiskspace;//��e���v���C���[��mp�����Z�����Ȃ�
    public bool dontaddcombo;//��e���A���U���������Z�����Ȃ�
    public int hp;
    public bool deleted = false;
    public bool partsvisible = true;//�L�����N�^�[�摜�̕\��/��\���@���G���Ԃ̓_�łɎg�p

    protected bool invisible = false;//���G���Ԃ��ǂ���
    protected bool sleep = true;//��ʂɓ���܂őҋ@���
    protected bool beforesleep = true;//sleep���������u�ԓ���̏�����������
    protected float invisibletime = 0.0f;//���G�J�n����̌o�ߎ���
    protected float endinvisibletime = 0.0f;//���G�I�����ԁ@�󂯂��U���ɂ���Đݒ�
    protected float blinktime = 0.0f;//�_�Ŏ��g�p����J�E���^

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
        if (!deleted)//���j����Ă��Ȃ����̂ݓ���
        {
            if (invisible)//���G��
            {
                invisibletime += Time.deltaTime;
                if (invisibletime < endinvisibletime)
                {
                    if (blinktime > 0.1f)//���Ł@�\������Ă��鎞
                    {

                        partsvisible = true;

                        blinktime = 0.0f;
                    }
                    else if (blinktime > 0.05f)//���Ł@�����Ă��鎞
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

    protected virtual void FixedUpdate()//�ύX�ɔ����Ē�`�̂�
    {
        
    }

    public virtual void Damage(int atk, float invisibletime)//��e���̏����@�U��������Ăяo��
    {
        if (!invisible)//���G���Ԓ��łȂ���Ώ���
        {
            hp -= atk;
            invisible = true;
            if (!dontaddcombo)
            {
                sau.combo += 1;
            }
            if (sau.combo > Gmanager.instance.maxcombo)//�A���q�b�g���̍ő�l���X�V
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

    //���j���ꂽ���̏���
    public void deletestart()
    {
        anim.Play("deleted");
        deleted = true;
        if (!dontadddeleten)//���j���X�R�A�̉��Z
        {
            Gmanager.instance.deleten += 1;
        }
    }

    //animation����I�u�W�F�N�g�폜����
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

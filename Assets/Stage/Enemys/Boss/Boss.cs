using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 �{�X�G�I�u�W�F�N�g���ʂ̃N���X�B�e�G�ŗL�̃N���X�Ɍp�����Ďg�p�B
 �ł���̐e�I�u�W�F�N�g�ɕt���Ȃ���Enemycollision����Q�Ƃł��Ȃ��_�ɒ��ӁB
 <��e����>
 Enemy�N���X�̏����ɉ����A
 hp��0�ɂȂ����ۑ��`�Ԃł���Ό`�Ԉڍs�C�x���g�̊J�n�A
 ���`�Ԃł���Ό��j�C�x���g�̊J�n�B

 <���G���Ԓ�����>
 Enemy�N���X�Ƌ��ʁB
 */


public class Boss : Enemy
{
    public bool ev;
    public int progress;//0:�{�X��O�@1:���`�ԁ@2:���`��
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

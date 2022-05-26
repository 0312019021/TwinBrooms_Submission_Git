using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 �G�̊e�X�v���C�g�I�u�W�F�N�g�ɕt����N���X�B
 �����p�[�c�̉摜���܂ޏꍇ�A���̑S�Ăɕt����B
 �摜�ꖇ�̃L�����̏ꍇ�AEnemy�N���X�Ɠ����ꏊ�ɂ��Ă��悢�B
 <�\���؂�ւ�����>�e�I�u�W�F�N�g�̏�Ԃ��Q�Ƃ��\��/��\����؂�ւ���B��ɖ��G���Ԃ̓_�łɎg�p�B
 */

public class enemyparts : MonoBehaviour
{
    private GameObject rootob;
    private Enemy enemysc;

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        rootob = transform.root.gameObject;
        enemysc = rootob.GetComponent<Enemy>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemysc.partsvisible)
        {
            sr.enabled = true;
        }
        else
        {
            sr.enabled = false;
        }
    }

    
}

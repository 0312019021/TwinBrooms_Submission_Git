using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 �G�I�u�W�F�N�g�̓����蔻������p�[�c�̃N���X
 �摜���p�[�c��������Ă��Ȃ��L�����̏ꍇ�AEnemy�N���X�����I�u�W�F�N�g�ɓ����ɕt���Ă��悢
 <��e����>�v���C���[�L�����̍U������ɐG�ꂽ�Ƃ��AEnemy�N���X�̔�_���[�W�������Ăяo���B
 */

public class Enemycollision : MonoBehaviour
{
    private GameObject rootob;
    private Enemy damageco;
    // Start is called before the first frame update
    void Start()
    {
        rootob = transform.root.gameObject;
        damageco = rootob.GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void calldamage(int atk, float invisibletime)
    {
        damageco.Damage(atk, invisibletime);
    }
}

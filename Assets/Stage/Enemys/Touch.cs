using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 �GMOB�Ƀv���C���[�L�������G�ꂽ�ۂ̃_���[�W�����������s���N���X�B
 ��Q���ȂǁA�G��Ă��_���[�W���������Ȃ��I�u�W�F�N�g�ɂ͕t���Ȃ��B
 �����p�[�c�̉摜�ō\�������G�I�u�W�F�N�g�̏ꍇ�A
 �����蔻���t�������{�[���̎q�I�u�W�F�N�g���쐬���A
 Collider�ƈꏏ��addcomponent����B

 <�ڐG�_���[�W����>�G�ꂽ�v���C���[�L�����̔�_���[�W�������Ăяo���B
 */

public class Touch : MonoBehaviour
{
    public int touchatk;
    public Enemy me;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "sau" && !me.deleted)
        {
            collision.gameObject.GetComponent<Sau>().damage(touchatk);
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "sau" && !me.deleted)
        {
            collision.gameObject.GetComponent<Sau>().damage(touchatk);

        }
    }
}

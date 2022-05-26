using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//�I�u�W�F�N�g��������z��


public class Gmanager : MonoBehaviour
{
    public static Gmanager instance = null;

    //�X�R�A�p�ϐ�
    public int deleten;
    public int clearmin;
    public float clearsec;
    public int damagen;
    public int maxcombo;
    public string playingstage;

    //�R���t�B�O�p�ϐ�
    public float soundvol_bgm;
    public float soundvol_se;
    public float sensitivity_longtapcoefficient;
    public float sensitivity_swipelengthcoefficient;
    public bool camera_zoom;
    public float sensitivity_camerazoomcoefficient;
    public bool camera_effect;

    // Start is called before the first frame update

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }



        //�Z�[�u�f�[�^����ϐ���ݒ�@���̓e�X�g�p�ɒ萔�w��
        //�R���t�B�O�ϐ�
        soundvol_bgm = 1.0f;
        soundvol_se = 1.0f;
        sensitivity_longtapcoefficient = 1.0f;
        sensitivity_swipelengthcoefficient = 1.0f;
        camera_zoom = true;
        sensitivity_camerazoomcoefficient = 1.0f;
        camera_effect = true;

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

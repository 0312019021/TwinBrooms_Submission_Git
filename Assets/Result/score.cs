using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class score : MonoBehaviour
{
    public int[] deleteconstant;
    public int[] timequota;
    private int playstageno;
    private int deletescore,timescore,damagescore,comboscore;
    private float cleartimeallsec;
    public Text texts = null;
    public total total;

    // Start is called before the first frame update
    void Start()
    {
        switch (Gmanager.instance.playingstage)
        {
            case "teststage":playstageno = 0; break;
            case "stage1": playstageno = 1; break;
            default:Debug.Log("score.cs��switch���̕���������Ⴄ");break;
        }

        //�G���j���X�R�A�@deleteconstant�͈�̂�����̃X�R�A
        deletescore = Gmanager.instance.deleten * deleteconstant[playstageno];
        if (deletescore < 0) { deletescore = 0; }else if (deletescore > 30) { deletescore = 30; }

        //�N���A�^�C���X�R�A�@timequota��3�b���߂��Ƃ�1���_
        cleartimeallsec = Gmanager.instance.clearmin * 60 + Gmanager.instance.clearsec;
        timescore = 30 - (int)((cleartimeallsec-timequota[playstageno])/3);
        if (timescore < 0) { timescore = 0; }else if (timescore > 30) { timescore = 30; }

        //�_���[�W�ɂ��X�R�A�@4�_���[�W�܂ł͌��_�Ȃ��@5�_���ȍ~��1�_�����Ƃ�4���_
        damagescore = 20 - (-Gmanager.instance.damagen - 4) * 4;
        if (damagescore < 0) { damagescore = 0; }else if (damagescore > 20) { damagescore = 20; }

        //�ő�R���{���ɂ��X�R�A�@1�R���{�ɂ�1�_���Z
        comboscore = Gmanager.instance.maxcombo;
        if (comboscore < 0) { comboscore = 0; }else if (comboscore > 20) { comboscore = 20; }

        texts.text = deletescore + "\n" + timescore + "\n" + damagescore + "\n" + comboscore + "\n";

        //������total�ɒl��n���ă��\�b�h�Ă�
        total.totalscoreview(deletescore + timescore + damagescore + comboscore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

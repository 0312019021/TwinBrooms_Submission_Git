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
            default:Debug.Log("score.csのswitch文の分岐条件が違う");break;
        }

        //敵撃破数スコア　deleteconstantは一体あたりのスコア
        deletescore = Gmanager.instance.deleten * deleteconstant[playstageno];
        if (deletescore < 0) { deletescore = 0; }else if (deletescore > 30) { deletescore = 30; }

        //クリアタイムスコア　timequotaを3秒超過ごとに1減点
        cleartimeallsec = Gmanager.instance.clearmin * 60 + Gmanager.instance.clearsec;
        timescore = 30 - (int)((cleartimeallsec-timequota[playstageno])/3);
        if (timescore < 0) { timescore = 0; }else if (timescore > 30) { timescore = 30; }

        //ダメージによるスコア　4ダメージまでは減点なし　5ダメ以降は1ダメごとに4減点
        damagescore = 20 - (-Gmanager.instance.damagen - 4) * 4;
        if (damagescore < 0) { damagescore = 0; }else if (damagescore > 20) { damagescore = 20; }

        //最大コンボ数によるスコア　1コンボにつき1点加算
        comboscore = Gmanager.instance.maxcombo;
        if (comboscore < 0) { comboscore = 0; }else if (comboscore > 20) { comboscore = 20; }

        texts.text = deletescore + "\n" + timescore + "\n" + damagescore + "\n" + comboscore + "\n";

        //ここでtotalに値を渡してメソッド呼ぶ
        total.totalscoreview(deletescore + timescore + damagescore + comboscore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 敵mobが撃破されたとき共通の画像の動作を指定するクラス
 */


public class recycleimgrotation : MonoBehaviour
{
    public float add;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 nowro = transform.localRotation.eulerAngles;
        nowro.z += add;
        transform.localRotation = Quaternion.Euler(nowro);
    }
}

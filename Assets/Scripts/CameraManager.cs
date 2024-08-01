using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float leftLimit = 0.0f;      // 左スクロールリミット
    public float rightLimit = 0.0f;     // 右スクロールリミット 
    public float topLimit = 0.0f;       // 上スクロールリミット 
    public float bottomLimit = 0.0f;    // 下スクロールリミット

    public GameObject subScreen; //サブスクリーン

    //bool型は強制スクロールをするかどうかのフラグ
    //float型は強制スクロールをした際のスピードと方向
    public bool isForceScrollX = false;
    public float forceScrollSpeedX = 0.5f;
    public bool isForceScrollY = false;
    public float forceScrollSpeedY = 0.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // プレイヤーを探す
        if (player != null)
        {
            // カメラの更新座標
            float x = player.transform.position.x;
            float y = player.transform.position.y;
            float z = transform.position.z;

            // 横同期させる
            //強制スクロールフラグが立っていたら
            if (isForceScrollX)
            {
                //プレイヤーの位置は無視して、カメラの位置にスクロールスピードを毎フレーム足すだけのxにする
                //Time.deltaTimeを掛け算することでフレーム処理速度に反比例して動きをおさえる
                x = transform.position.x + (forceScrollSpeedX * Time.deltaTime);
            }

            // 両端に移動制限を付ける
            if (x < leftLimit)
            {
                x = leftLimit;
            }
            else if (x > rightLimit)
            {
                x = rightLimit;
            }

            // 縦同期させる
            //強制スクロールフラグが立っていたら
            if (isForceScrollY)
            {
                //プレイヤーの位置は無視して、カメラの位置にスクロールスピードを毎フレーム足すだけのxにする
                y = transform.position.y + (forceScrollSpeedY * Time.deltaTime);
            }

            // 上下に移動制限を付ける
            if (y < bottomLimit)
            {
                y = bottomLimit;
            }
            else if (y > topLimit)
            {
                y = topLimit;
            }
            // カメラ位置のVector3を作る
            Vector3 v3 = new Vector3(x, y, z);
            transform.position = v3;

            if (subScreen != null)
            {
                //サブスクリーンの動きを決める
                //カメラの動きは直前に決め終わっているのでそこでつかっていた変数x、y、zは使い回し
                y = subScreen.transform.position.y;
                z = subScreen.transform.position.z;
                Vector3 v = new Vector3(x / 2.0f, y, z);
                subScreen.transform.position = v;
            }
        }
    }
}

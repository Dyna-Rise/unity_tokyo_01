using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject objPrefab;        // 発生させる Prefab データ 
    public float delayTime = 3.0f;      // 遅延時間
    public float fireSpeed = 4.0f;    // 発射速度
    public float length = 8.0f;　//範囲

    GameObject player;                  // プレイヤー
    Transform gateTransform;     // 発射口のTransformコンポーネント
    float passedTimes = 0;              // 経過時間


    // 距離チェック　自作メソッド
    bool CheckLength(Vector2 targetPos)
    {
        bool ret = false;
        float d = Vector2.Distance(transform.position, targetPos);
        if (length >= d)
        {
            ret = true;
        }
        return ret;
    }

    // Start is called before the first frame update
    void Start()
    {
        // 発射口オブジェクトを取得
        gateTransform = transform.Find("gate");
        // プレイヤー
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // 待機時間加算
        passedTimes += Time.deltaTime;

        // プレイヤーと砲台の距離チェック
        if (CheckLength(player.transform.position))
        {
            //待機時間経過
            if (passedTimes > delayTime)
            {
                // 発射!!
                passedTimes = 0; //時間を0にリセット
                // 発射位置の取得
                Vector2 pos = new Vector2(gateTransform.position.x,gateTransform.position.y);
                // Prefab から 砲弾GameObject を作る
                GameObject obj = Instantiate(objPrefab, pos, Quaternion.identity);
                // 砲身が向いている方に発射する
                Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>();
                float angleZ = transform.localEulerAngles.z;
                float x = Mathf.Cos(angleZ * Mathf.Deg2Rad);
                float y = Mathf.Sin(angleZ * Mathf.Deg2Rad);
                Vector2 v = new Vector2(x,y) * fireSpeed;
                rbody.AddForce(v, ForceMode2D.Impulse);
            }
        }
    }

    //範囲表示
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, length);
    }

}

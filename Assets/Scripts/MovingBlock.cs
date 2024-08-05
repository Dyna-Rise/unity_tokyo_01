using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public float moveX = 0.0f;          //X移動距離
    public float moveY = 0.0f;          //Y移動距離
    public float times = 0.0f;          //時間
    public float weight = 0.0f;         //停止時間
    public bool isMoveWhenOn = false;   //乗った時に動くフラグ

    public bool isCanMove = true;       //動くフラグ
    Vector3 startPos; //初期位置
    Vector3 endPos; //移動位置
    bool isReverse = false; //反転フラグ

    float movep = 0; //移動補完値

    // Start is called before the first frame update
    void Start()
    {
        //初期位置
        startPos = transform.position;
        endPos = new Vector2(startPos.x + moveX, startPos.y + moveY); //移動位置

        if (isMoveWhenOn)
        {
            //乗った時に動くので最初は動かさない
            isCanMove = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (isCanMove)
        {
            //移動中
            float distance = Vector2.Distance(startPos, endPos); //移動距離
            float ds = distance / times; //1秒あたりの移動距離 ※timesはエディタで任意設定
            float df = ds * Time.deltaTime;
            //1フレームごとの移動距離
            movep += (df / distance); //Lerpメソッドに指定する移動補完値

                
            if (isReverse)
            {
                //逆方向移動中...
                transform.position = Vector2.Lerp(endPos, startPos, movep);

            }
            else
            {
                //正方向移動中...
                transform.position = Vector2.Lerp(startPos, endPos, movep);

            }

            if (movep >= 1.0f)
            {
                movep = 0.0f; //移動補完値リセット
                isReverse = !isReverse; //移動を逆転
                isCanMove = false; //移動停止
                if(isMoveWhenOn == false)
                {
                    //乗った時に動くフラグOFF
                    //Invoke("Move", wait);
                }
            }
        }
    }

    //移動フラグを立てる
    public void Move()
    {
        isCanMove = true;
    }

    //移動フラグを下ろす
    public void Stop()
    {
        isCanMove = false;
    }

    //接触開始
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //接触したのがプレイヤーなら移動床の子にする
            collision.transform.SetParent(transform);
            if (isMoveWhenOn)
            {
                //乗った時に動くフラグON
                isCanMove = true;   //移動フラグを立てる
            }
        }
    }
    //接触終了
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //接触したのがプレイヤーなら移動床の子から外す
            collision.transform.SetParent(null);
        }
    }

    //移動範囲表示
    void OnDrawGizmosSlecected()
    {
        Vector2 fromPos;
        if(startPos == Vector3.zero)
        {
            fromPos = transform.position;
        }
        else
        {
            fromPos = startPos;
        }
        //移動せん
        //Gizmos.DrawLine()
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public float moveX = 0.0f;          //X移動距離
    public float moveY = 0.0f;          //Y移動距離
    public float times = 0.0f;          //何秒で目的地に移動するか
    public float wait = 0.0f;         //停止時間
    public bool isMoveWhenOn = false;   //乗った時にはじめて動くようにするかどうかフラグ

    public bool isCanMove = true;   //動いている最中はtrue

    Vector3 startPos; //初期位置
    Vector3 endPos; //移動位置
    bool isReverse = false; //反転フラグ

    float movep = 0; //移動補完値

    // Start is called before the first frame update
    void Start()
    {

        startPos = transform.position; //初期位置の取得
        endPos = new Vector2(startPos.x + moveX, startPos.y + moveY); //目的の位置の取得

        if (isMoveWhenOn) //乗った時にはじめて動くフラグがtrueなら動きを止めとく
        {
            //乗った時に動くので最初は動かさない
            isCanMove = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (isCanMove)//動くフラグがたってないと何もしない
        {
            //移動中

            float distance = Vector2.Distance(startPos, endPos); //移動距離
            float ds = distance / times; //1秒あたりどれくらい移動しなければいけないのか ※timesはエディタで任意設定
            float df = ds * Time.deltaTime;  //1フレームごとの移動距離

            //1フレームあたりの移動距離dfを全体の距離distanceで割る
            //その1フレームが全体の何%の距離分なのかを計算
            movep += (df / distance); //Lerpメソッドに指定する移動補完値(進捗率)を計算

                
            //Lerpメソッドを使ってブロックを動かす
            //Lerpメソッド ※始点、終点を指定、そのうち今どの進捗率の位置にいるべきかを指定
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

            //進捗率が100％に届いた時
            if (movep >= 1.0f)
            {
                movep = 0.0f; //移動補完値リセット
                isReverse = !isReverse; //移動を逆転
                isCanMove = false; //移動を一旦停止
                if(isMoveWhenOn == false) //プレイヤーが乗って動くタイプじゃない時
                {
                    //自動で逆に動きださなければいけない
                    Invoke("Move", wait); //時間差で動き出すためのMoveメソッドの発動

                    //Invoke→第2引数の秒数待ってから、第1引数のメソッド発動
                }
            }
        }
    }

    //移動フラグを立てる　自作メソッド
    public void Move()
    {
        isCanMove = true;
    }

    //移動フラグを下ろす 自作メソッド　※スイッチプログラムの時使う
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
    void OnDrawGizmosSelected()
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
        //移動線
        Gizmos.DrawLine(fromPos, new Vector2(fromPos.x + moveX, fromPos.y + moveY));

        //スプライトのサイズ
        Vector2 size = GetComponent<SpriteRenderer>().size;
        //初期位置
        Gizmos.DrawWireCube(fromPos, new Vector2(size.x, size.y));
        //移動位置
        Vector2 toPos = new Vector3(fromPos.x + moveX, fromPos.y + moveY);
        Gizmos.DrawWireCube(toPos, new Vector2(size.x, size.y));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3.0f;          // 移動速度
    public bool isToRight = false;   // 向き right or left 
    public float revTime = 0.0f;          // 反転までの時間
    public LayerMask groundLayer;        // 地面レイヤー

    float time = 0; //経過時間

    // Start is called before the first frame update
    void Start()
    {
        if (isToRight)
        {
            transform.localScale = new Vector2(-1, 1);// 向きの変更
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (revTime > 0)　//もし反転時間が定められていたら
        {
            time += Time.deltaTime; //変数timeに経過時間を蓄積
            if (time >= revTime) //経過時間が定められた時間(revTime)を上回ったら
            {
                isToRight = !isToRight; //反転フラグ
                time = 0; //経過時間をゼロにリセット

                if (isToRight)//右向きフラグの場合
                {
                    //※元の絵が左向きのためマイナスは右向きになる
                    transform.localScale = new Vector2(-1, 1); //右向きの絵にする
                }
                else//左向きフラグの場合
                {
                    transform.localScale = new Vector2(1, 1); //左向きの絵にする
                }
            }
        }
    }

    void FixedUpdate()
    {
        //地上判定 onGroundがtrueかfalseか
        bool onGround = Physics2D.CircleCast(
            transform.position,
            0.5f,
            Vector2.down,
            0.5f,
            groundLayer);

        //地面にいる時実際に動かす
        if (onGround)
        {
            // 速度を更新する
            // Rigidbody2D を取ってくる
            Rigidbody2D rbody = GetComponent<Rigidbody2D>();

            //経過時間に関係なく常に左右どちらかに動いている
            if (isToRight) //右向きフラグの場合
            {
                rbody.velocity = new Vector2(speed, rbody.velocity.y);
            }
            else　//左向きフラグの場合
            {
                rbody.velocity = new Vector2(-speed, rbody.velocity.y);
            }
        }

    }

    // 何かに接触した場合
    void OnTriggerEnter2D(Collider2D collision)
    {
        isToRight = !isToRight; //経過時間にかかわらず反転 
        time = 0; //経過時間はリセット
        if (isToRight)//右向きフラグの場合
        {
            //※元の絵が左向きのためマイナスは右向きになる
            transform.localScale = new Vector2(-1, 1); //右向きの絵にする
        }
        else//左向きフラグの場合
        {
            transform.localScale = new Vector2(1, 1); //左向きの絵にする
        }
    }
}

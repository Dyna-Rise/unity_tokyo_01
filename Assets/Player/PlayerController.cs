using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody; //Rigidbody2D型の変数
    float axisH = 0.0f; //入力の力
    public float speed = 3.0f; //プレイヤーの移動速度

    public float jump = 9.0f; //ジャンプ力
    public LayerMask groundLayer; //判定対象のレイヤー
    bool goJump = false; //ジャンプ開始フラグ


    // Start is called before the first frame update
    void Start()
    {
        //PlayerについているRigidbody2Dコンポーネントがrbodyということにする
        rbody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //水平方向にまつわるキー（左右キー・ AとD）がおされている場合、左方向なら-1、右方向なら1、何も押されていなければ0の値を返すメソッド→GetAixsRaw
        axisH = Input.GetAxisRaw("Horizontal");
        if (axisH > 0.0f) //axisHがプラスの値の時＝右が押された時
        {
            Debug.Log("右移動");
            transform.localScale = new Vector2(1, 1); //イラストの向きを右向き
        }
        else if (axisH < 0.0f) //axisHがマイナスの値の時＝左が押された時
        {
            Debug.Log("左移動");
            transform.localScale = new Vector2(-1, 1); //イラストの向きを左向き
        }
    }

    void FixedUpdate()
    {
        bool onGround = Physics2D.CircleCast(transform.position,
            0.2f,
            Vector2.down,
            0.0f,
            groundLayer);
        if (onGround || axisH != 0)
        {
            //rbody.velocity.y　→　y方向の力に関しては成り行き
            rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);
        }
        if (onGround && goJump)
        {
            Vector2 jumpPw = new Vector2(0, jump);
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);
            goJump = false;
        }
    }

    public void Jump()
    {
        goJump = true;
    }
}

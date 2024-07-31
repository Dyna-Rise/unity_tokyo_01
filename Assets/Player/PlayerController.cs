using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //ゲームの状態※staticなので、あらゆるシーンに内容を引き継ぐ変数
    public static string gameState = "playing"; 

    Rigidbody2D rbody; //Rigidbody2D型の変数
    float axisH = 0.0f; //入力の力
    public float speed = 3.0f; //プレイヤーの移動速度

    public float jump = 9.0f; //ジャンプ力
    public LayerMask groundLayer; //判定対象のレイヤー
    bool goJump = false; //ジャンプ開始フラグ

    Animator animator; //アニメーターコンポーネント
    public string stopAnime = "PlayerStop";
    public string moveAnime = "PlayerMove";
    public string jumpAnime = "PlayerJump";
    public string goalAnime = "PlayerGoal";
    public string deadAnime = "PlayerOver";
    string nowAnime = ""; //初期値
    string oldAnime = "";　//初期値

    // Start is called before the first frame update
    void Start()
    {
        //PlayerについているRigidbody2Dコンポーネントがrbodyということにする
        rbody = this.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        nowAnime = stopAnime;
        oldAnime = stopAnime;

        gameState = "playing"; //ゲームの状態をplayingに戻す
    }

    // Update is called once per frame
    void Update()
    {
        if(gameState != "playing")
        {
            return;
        }

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

        if (Input.GetButtonDown("Jump")) //もしジャンプに関するボタンがおされたら
        {
            Jump(); //自作したJumpメソッドの発動 変数goJumpがtrueになる
        }
    }

    void FixedUpdate()
    {
        if(gameState != "playing")
        {
            return;
        }

        //CircleCast()メソッド→基準から出ている特定の形のセンサー（光線）が特定のレイヤーに触れていればtrue、触れていなければfalseを返す
        bool onGround = Physics2D.CircleCast(
            transform.position, //発信源の指定どこから？
            0.2f, //円の半径
            Vector2.down, //発射方向は下
            0.0f, //設置距離
            groundLayer //どのレイヤーに引っかかった時にtrueにするか（レイヤーの指名）
            );

        if (onGround || axisH != 0)　// 移動キーが押されている時 or（または） 地面にいる時
        {
            //rbody.velocity.y　→　y方向の力に関しては成り行き
            rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);
        }

        if (onGround && goJump)　//ジャンプボタンがおされた時 and（かつ） 地面にいる時
        {
            Vector2 jumpPw = new Vector2(0, jump);//①次のAddForce()メソッドの材料として、力がかかる方向を作っています。水平は0、垂直はプラス方向にjumpの数字
            //作った方向の数字を変数jumpPwに格納している
            rbody.AddForce(jumpPw, ForceMode2D.Impulse); //②AddForce()メソッドを使って指定した方向に押し出している
            goJump = false; //ジャンプ済み フラグをfalseに戻す
        }

        if (onGround)
        {
            if(axisH == 0)
            {
                nowAnime = stopAnime;
            }
            else
            {
                nowAnime = moveAnime;
            }
        }
        else
        {
            nowAnime = jumpAnime;
        }
        if (nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime);
        }
    }

    //自作したメソッド
    public void Jump()
    {
        //ジャンプ開始フラグ 初期値false→trueに　→ジャンプスタート
        goJump = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Goal")
        {
            Goal(); //自作メソッドの発動
        }
        else if(collision.gameObject.tag == "Dead")
        {
            GameOver(); //自作メソッドの発動
        }
    }

    public void Goal()
    {
        animator.Play(goalAnime);
        gameState = "gameclear";
        GameStop(); //ゲーム停止の自作メソッドの発動
    }

    public void GameOver()
    {
        animator.Play(deadAnime);
        gameState = "gameover";
        GameStop(); //ゲーム停止の自作メソッドの発動

        //ゲームオーバーの演出部分
        GetComponent<CapsuleCollider2D>().enabled = false; //コライダーの機能をカット
        rbody.AddForce(new Vector2(0,5),ForceMode2D.Impulse);//瞬発的に上方向に跳ね上げる
    }

    void GameStop()
    {
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        rbody.velocity = new Vector2(0, 0);
        Debug.Log("ストップ");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public bool isCountDown = true; // true= 時間をカウントダウン計測する
    public float gameTime = 0;      // ゲームの最大時間 ※基準値
    public bool isTimeOver = false; // true= タイマー停止するためのフラグ
    public float displayTime = 0;   // 表示時間

    float times = 0;                // ゲームが開始してからの経過時間　※内部計算に使う

    // Start is called before the first frame update
    void Start()
    {
        if (isCountDown)
        {
            // カウントダウン
            displayTime = gameTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimeOver == false)　//タイマーを止めるフラグがfalseだったら
        {
            times += Time.deltaTime; //ゲームの経過時間を蓄積 ※1フレームの処理時間=Time.deltaTime
            if (isCountDown) //カウントダウンのフラグがONの時
            {
                // カウントダウン
                displayTime = gameTime - times; //基準時間 - ゲーム経過時間 =　残時間
                if (displayTime <= 0.0f)
                {
                    displayTime = 0.0f; //ユーザーに見せる時間は0に留める
                    isTimeOver = true; //タイマーの処理自体を止める
                }
            }
            else //カウントダウンのフラグがONではなかった時＝カウントアップ
            {
                // カウントアップ
                displayTime = times;
                if (displayTime >= gameTime) //カウントアップが基準時間を越えたら
                {
                    displayTime = gameTime; //ユーザーに見せる時間は基準時間ぴったしに留める
                    isTimeOver = true; //タイマーの処理自体を止める
                }
            }
            Debug.Log("TIMES: " + displayTime);
        }
    }
}

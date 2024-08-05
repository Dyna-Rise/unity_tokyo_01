using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UIを使うのに必要
using TMPro;

public class GameManager : MonoBehaviour
{

    public GameObject mainImage; //画像を持つGameObject
    public Sprite gameOverSpr; //GAMEOVER画像
    public Sprite gameClearSpr; //GAMECLEAR画像
    public GameObject panel; //パネル
    public GameObject restartButton; //リスタートボタン
    public GameObject nextButton; //ネクストボタン

    Image titleImage; //ImageオブジェクトにひっついているImageコンポーネント

    public GameObject timeBar; //時間表示イメージ
    public GameObject timeText; //時間テキスト
    TimeController timeCnt; //TimeControllerクラス


    public GameObject scoreText; //スコアのUI
    public static int totalScore; //全編通して引き継いでいくスコア
    public int stageScore = 0; //そのステージの合計得点※次のステージに移る頃には消滅

    // Start is called before the first frame update
    void Start()
    {
        //時間差で「ゲームスタートを非表示にするメソッド」を発動
        //第1引数→メソッド名
        Invoke("InactiveImage", 1.0f);

        //パネルは即非表示
        panel.SetActive(false);


        //++時間制限追加+++
        //TimeControllerを取得
        timeCnt = GetComponent<TimeController>();
        if (timeCnt != null)
        {
            //カウントダウン前提で、カウントダウンする気がない時
            if (timeCnt.gameTime == 0.0f)
            {
                timeBar.SetActive(false); //時間制限なしなら隠す
            }

        }

        //自作メソッド ※まずはUIにその面がスタートした時のスコアを表示
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.gameState == "gameclear")
        {
            mainImage.SetActive(true); //非表示にしていた画像を表示に戻す
            panel.SetActive(true);//非表示にしていたパネルを表示に戻す

            //リスタートボタンを無効化する
            //CanvasではないRestartButtonが持っているButtonコンポーネントを取得して変数btに入れる
            Button bt = restartButton.GetComponent<Button>();
            bt.interactable = false;

            //絵の差し替え
            mainImage.GetComponent<Image>().sprite = gameClearSpr;

            PlayerController.gameState = "gameend"; //gameendにすることで、以後どこにもひっかからない※無駄に何回も処理を繰り返さない


            //++時間制限追加+++
            if (timeCnt != null)
            {
                timeCnt.isTimeOver = true; //カウント停止

                //スコアも確定
                //残時間を整数化して変数timeに保管
                int time = (int)timeCnt.displayTime;
                totalScore += time * 10; //残時間x10をタイムボーナスとして加算
            }

            //stageScoreもトータルに加算
            totalScore += stageScore;
            stageScore = 0; //二重にならないようにstageScoreはリセット
            UpdateScore(); //UIに反映

        }

        else if (PlayerController.gameState == "gameover")
        {
            mainImage.SetActive(true); //非表示にしていた画像を表示に戻す
            panel.SetActive(true);//非表示にしていたパネルを表示に戻す

            //ネクストボタンを無効化する
            //CanvasではないRestartButtonが持っているButtonコンポーネントを取得して変数btに入れる
            Button bt = nextButton.GetComponent<Button>();
            bt.interactable = false;

            //絵の差し替え
            mainImage.GetComponent<Image>().sprite = gameOverSpr;

            PlayerController.gameState = "gameend"; //gameendにすることで、以後どこにもひっかからない※無駄に何回も処理を繰り返さない


            //++時間制限追加+++
            if (timeCnt != null)
            {
                timeCnt.isTimeOver = true; //カウント停止
            }

        }
        else if (PlayerController.gameState == "playing")
        {
            //ゲーム中のUI
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            //PlayerControllerを取得
            PlayerController playerCnt = player.GetComponent<PlayerController>();

            if (timeCnt != null)
            {
                if (timeCnt.gameTime > 0.0f)
                {
                    //整数に代入することで小数を切り捨てる
                    int time = (int)timeCnt.displayTime;
                    //タイム更新
                    timeText.GetComponent<TextMeshProUGUI>().text = time.ToString();

                    //タイムオーバー
                    if (time == 0)
                    {
                        playerCnt.GameOver(); //ゲームオーバーにする
                    }
                }
            }

            //スコアの追加
            if(playerCnt.score != 0) //プレイヤーがアイテムを取った時
            {
                stageScore += playerCnt.score;
                playerCnt.score = 0;
                UpdateScore(); //表に現スコアを表示
            }

        }
    }

    //画像を非表示にする役割をもった自作メソッド
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }


    void UpdateScore()
    {
        //そのステージで入手したスコア+前ステージまでの確定スコアを足す
        int score = stageScore + totalScore;
        scoreText.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }
}

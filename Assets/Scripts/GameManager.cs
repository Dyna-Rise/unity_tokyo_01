using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UIを使うのに必要

public class GameManager : MonoBehaviour
{

    public GameObject mainImage; //画像を持つGameObject
    public Sprite gameOverSpr; //GAMEOVER画像
    public Sprite gameClearSpr; //GAMECLEAR画像
    public GameObject panel; //パネル
    public GameObject restartButton; //リスタートボタン
    public GameObject nextButton; //ネクストボタン

    Image titleImage; //ImageオブジェクトにひっついているImageコンポーネント

    // Start is called before the first frame update
    void Start()
    {
        //時間差で「ゲームスタートを非表示にするメソッド」を発動
        //第1引数→メソッド名
        Invoke("InactiveImage",1.0f);

        //パネルは即非表示
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.gameState == "gameclear")
        {

        }
        else if(PlayerController.gameState == "gameover")
        {

        }
        else if(PlayerController.gameState == "playing")
        {
            //ゲーム中のUI
        }
    }

    //画像を非表示にする役割をもった自作メソッド
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }
}

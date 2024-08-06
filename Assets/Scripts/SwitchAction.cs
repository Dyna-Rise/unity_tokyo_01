using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchAction : MonoBehaviour
{
    public GameObject targetMoveBlock;
    public Sprite imageOn;
    public Sprite imageOff;
    public bool on = false; // スイッチの状態(true:押されている false:押されていない)

    // Start is called before the first frame update
    void Start()
    {
        if (on) //最初から変数onがtrueだった場合
        {
            GetComponent<SpriteRenderer>().sprite = imageOn; //レバーオンの絵にする
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = imageOff;//レバーオフの絵にする
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 接触開始
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (on) //もしスイッチがオンだったらオフにしなければならない
            {
                on = false;
                GetComponent<SpriteRenderer>().sprite = imageOff;

                //他人であるMovingBlockスクリプトの情報を取得
                MovingBlock movBlock = targetMoveBlock.GetComponent<MovingBlock>();
                movBlock.Stop();
            }
            else //逆の事をするだけ
            {
                on = true;
                GetComponent<SpriteRenderer>().sprite = imageOn;
                MovingBlock movBlock = targetMoveBlock.GetComponent<MovingBlock>();
                movBlock.Move();
            }
        }
    }
}

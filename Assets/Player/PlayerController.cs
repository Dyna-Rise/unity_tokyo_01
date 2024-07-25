using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody; //Rigidbody2D型の変数
    float axisH = 0.0f; //入力の力

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
    }

    void FixedUpdate()
    {
        //rbody.velocity.y　→　y方向の力に関しては成り行き
        rbody.velocity = new Vector2(axisH * 3.0f,rbody.velocity.y);
    }
}

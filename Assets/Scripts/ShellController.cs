using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    public float deleteTime = 3.0f; // 削除する時間指定

    // Start is called before the first frame update
    void Start()
    {
        //Destroyに第二引数を与えると第二引数の秒数だけまってから消滅する
        Destroy(gameObject, deleteTime); // 削除設定
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject); // 何かに接触したら消す
    }
}

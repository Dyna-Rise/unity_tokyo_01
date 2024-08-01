using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //切り替え命令が用意されている名前空間

public class ChangeScene : MonoBehaviour
{
    public string sceneName; //目的の（切り替え先の)シーン名が入る

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //シーン切替の自作メソッド
    public void Load()
    {
        SceneManager.LoadScene(sceneName);　//LoadSceneメソッドで目的のシーン名を指定
    }
}

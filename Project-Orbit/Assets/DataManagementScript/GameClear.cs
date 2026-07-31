using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameClear : SingletonMonoBehaviour<GameClear>
{
    [Header("クリア時のリザルトUI")]
    [SerializeField] private TextMeshProUGUI resultText = null;

    [Header("UserDataHolder")]
    [SerializeField] private UserDataHolder userDataHolder = null;

    [Header("リザルトパネル")]
    [SerializeField] private GameObject resultPanel = null;

    [Header("次の操作案内Text")]
    [SerializeField] private TMP_Text nextText = null;

    /// <summary>
    /// リザルト表示中かどうか
    /// </summary>
    private bool isResult = false;

    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {
        //リザルト表示中でなければ終了
        if (!isResult) return;

        //右クリックでタイトル画面へ戻る
        if (Input.GetMouseButtonDown(1))
        {
            SceneLoadManager.Instance.LoadScene(SceneType.GameTitleScene, FadeType.None);
        }
    }

    public void GetUserData(string name, float clearTime)
    {
        resultText.text = "プレイヤー名：" + name + "クリアタイム：" + clearTime.ToString("F2") + "秒";
    }

    // クリアデータを実際にデータ処理して確かめる
    public void GameClearResult()
    {
        //リザルトパネルを表示
        resultPanel.SetActive(true);

        // リザルト表示
        GetUserData(userDataHolder.userName, userDataHolder.clearTime);

        // ランキングへ登録
        RankingManager.Instance.AddPlayer(userDataHolder.userName, userDataHolder.clearTime);

        //次の操作を表示
        nextText.text = "右クリックでタイトルへ戻る";

        //リザルト表示状態にする
        isResult = true;
    }
}
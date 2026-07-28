using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameClear : SingletonMonoBehaviour<GameClear>
{
    [Header("クリア時のリザルトUI")]
    [SerializeField] private TextMeshProUGUI resultText = null;

    [Header("UserDataHolder")]
    [SerializeField] private UserDataHolder userDataHolder = null;

    private void Update()
    {

    }

    public void GetUserData(string name, float clearTime)
    {
        resultText.text = "プレイヤー名：" + name + "クリアタイム：" + clearTime.ToString("F2") + "秒";
    }

    // クリアデータを実際にデータ処理して確かめる
    public void GameClearResult()
    {
        // リザルト表示
        GetUserData(userDataHolder.userName, userDataHolder.clearTime);

        // ランキングへ登録
        RankingManager.Instance.AddPlayer(userDataHolder.userName, userDataHolder.clearTime);
    }


}
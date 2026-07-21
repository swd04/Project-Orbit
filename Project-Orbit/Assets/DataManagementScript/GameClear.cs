using UnityEngine;
using TMPro;

public class GameClear : SingletonMonoBehaviour<GameClear>
{
    [Header("クリア時のリザルトUI")]
    [SerializeField] private TextMeshProUGUI resultText = null;

    public void GetUserData(string name, float clearTime)
    {
        resultText.text = "ゲームクリア！" + "プレイヤー名：" + name + "クリアタイム：" + clearTime.ToString("F2") + "秒";
    }

    // クリアデータを実際にデータ処理して確かめる1
}

using UnityEngine;
using TMPro;
using System.Collections;

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

    [Header("操作案内を表示するまでの時間")]
    [SerializeField] private float nextTextDelay = 1.5f;

    /// <summary>
    /// リザルト表示中かどうか
    /// </summary>
    private bool isResult = false;

    /// <summary>
    /// 操作案内テキストが表示されているか
    /// </summary>
    private bool isNextTextVisible = false;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        isResult = false;
        isNextTextVisible = false;

        if (nextText != null)
        {
            nextText.text = "";
        }

        //シーン開始から一定時間後に操作可能にする
        StartCoroutine(ShowNextTextRoutine());
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {
        //リザルト表示中でなければ終了
        //if (!isResult) return;

        //左クリックでタイトル画面へ戻る
        if (Input.GetMouseButtonDown(0))
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
        //リザルト表示状態にする
        isResult = true;

        //リザルトパネルを表示
        resultPanel.SetActive(true);

        // リザルト表示
        GetUserData(userDataHolder.userName, userDataHolder.clearTime);

        // ランキングへ登録
        RankingManager.Instance.AddPlayer(userDataHolder.userName, userDataHolder.clearTime);
    }

    /// <summary>
    /// 操作案内テキストを遅れて表示する処理
    /// </summary>
    private IEnumerator ShowNextTextRoutine()
    {
        //最初は非表示
        isNextTextVisible = false;

        //最初はテキスト無し
        if(nextText != null)
        {
            nextText.text = "";
        }

        //指定秒数待つ
        yield return new WaitForSeconds(nextTextDelay);

        //テキスト表示
        if (nextText != null)
        {
            nextText.text = "左クリックでタイトルへ戻る";
        }

        //左クリック受付開始
        isNextTextVisible = true;

        Debug.Log("nextText : " + nextText.text);
    }
}
using UnityEngine;

/// <summary>
/// ボタン用シーン遷移クラス
/// </summary>
public class SceneChangeButton : MonoBehaviour
{
    [Header("遷移先シーン")]
    [SerializeField] private SceneType sceneType = SceneType.GameTitleScene;

    [Header("フェードタイプ")]
    [SerializeField] private FadeType fadeType = FadeType.None;

    /// <summary>
    /// ボタン押下時の処理
    /// </summary>
    public void OnClickLoadScene()
    {
        SceneLoadManager.Instance.LoadScene(sceneType, fadeType);
    }
}
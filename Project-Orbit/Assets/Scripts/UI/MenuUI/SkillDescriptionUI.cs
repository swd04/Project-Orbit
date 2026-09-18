using UnityEngine;
using TMPro;

/// <summary>
/// スキル説明表示UIクラス
/// </summary>
public class SkillDescriptionUI : MonoBehaviour
{
    [Header("スキル説明表示Text")]
    [SerializeField] private TMP_Text skillDescriptionText = null;

    [Header("スキル説明UIの位置")]
    [SerializeField] private RectTransform descriptionRect = null;

    [Header("コアからの表示x距離")]
    [SerializeField] private float displayDistance = 500.0f;

    [Header("画面端からの余白距離")]
    [SerializeField] private float screenMargin = 50.0f;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Awake()
    {
        //説明UIを非表示にする
        gameObject.SetActive(false);
    }

    /// <summary>
    /// スキル説明を表示する処理
    /// </summary>
    public void Show(SkillDate skill, Vector3 corePosition)
    {
        //スキルが設定されていなければ処理を終了
        if (skill == null)
        {
            return;
        }

        //スキル説明を表示
        skillDescriptionText.text = skill.skillDescription;

        //説明UIを表示
        gameObject.SetActive(true);

        //コアの位置に応じて説明UIを左右に表示する
        Vector3 offset = corePosition.x < Screen.width / 2.0f ? Vector3.right : Vector3.left;

        //コアの位置から指定した距離だけ離して説明UIを配置する
        descriptionRect.position = corePosition + offset * displayDistance;

        //説明UIの位置を画面内に収める
        KeepInsideScreen();
    }

    /// <summary>
    /// 説明UIを画面内に収める処理
    /// </summary>
    private void KeepInsideScreen()
    {
        //説明UIの位置を画面座標に変換する
        Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint(null, descriptionRect.position);

        //説明UIの大きさを取得する
        Vector2 size = descriptionRect.rect.size;

        //X座標を画面内に制限する
        screenPosition.x = Mathf.Clamp(
            screenPosition.x, size.x / 2.0f + screenMargin,
            Screen.width - size.x / 2.0f - screenMargin);

        //Y座標を画面内に制限する
        screenPosition.y = Mathf.Clamp(
            screenPosition.y, size.y / 2.0f + screenMargin,
            Screen.height - size.y / 2.0f - screenMargin);

        //画面座標をUIのワールド座標に変換する
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            descriptionRect.parent as RectTransform,
            screenPosition,
            null,
            out Vector3 worldPosition
        );

        //制限した位置を説明UIに設定する
        descriptionRect.position = worldPosition;
    }

    /// <summary>
    /// スキル説明を非表示にする処理
    /// </summary>
    public void Hide()
    {
        //説明UIを非表示にする
        gameObject.SetActive(false);
    }
}
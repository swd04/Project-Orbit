using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

/// <summary>
/// コア一覧1行分の表示UI
/// </summary>
public class CoreItemUI : MonoBehaviour,
    IPointerClickHandler
{
    [Header("コア名表示Text")]
    [SerializeField] private TMP_Text coreNameText = null;

    [Header("所持数表示Text")]
    [SerializeField] private TMP_Text countText = null;

    /// <summary>
    /// このUIが表すコア
    /// </summary>
    private SoulCore soulCore;

    /// <summary>
    /// このUIを管理しているコア一覧UI
    /// </summary>
    private MenuCoreCollectionUI coreListUI;

    /// <summary>
    /// コア一覧UIを設定する
    /// </summary>
    public void Initialize(MenuCoreCollectionUI listUI)
    {
        coreListUI = listUI;
    }

    /// <summary>
    /// コア情報表示処理
    /// </summary>
    public void SetData(SoulCore core)
    {
        //コア情報保存
        soulCore = core;

        //コア名表示
        coreNameText.text = core.name;

        //所持数表示
        countText.text = $"Lv.{core.soulLevel}";
    }

    /// <summary>
    /// このUIが保持しているコアを取得する処理
    /// </summary>
    public SoulCore GetSoulCore()
    {
        return soulCore;
    }

    /// <summary>
    /// コアを左クリックした時の処理
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        //左クリック以外は無視
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }

        //クリックされたコアを装備する
        coreListUI.OnClickItem(soulCore);
    }
}
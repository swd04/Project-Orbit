using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// コア一覧1行分の表示UI
/// </summary>
public class CoreItemUI : MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler
{
    [Header("コア名表示Text")]
    [SerializeField] private TMP_Text coreNameText = null;

    [Header("所持数表示Text")]
    [SerializeField] private TMP_Text countText = null;

    [Header("RectTransform")]
    [SerializeField] private RectTransform rectTransform = null;

    [Header("CanvasGroup")]
    [SerializeField] private CanvasGroup canvasGroup = null;

    //
    private Vector2 startPosition;

    //このUIが表すコア
    private SoulCore soulCore;

    /// <summary>
    /// コア情報表示処理
    /// </summary>
    public void SetData(SoulCore core, int count)
    {
        //
        soulCore = core;

        //コア名表示
        coreNameText.text = core.name;

        //所持数表示
        countText.text = $"×{count}";
    }

    /// <summary>
    /// 
    /// </summary>
    public SoulCore GetSoulCore()
    {
        return soulCore;
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = rectTransform.anchoredPosition;

        canvasGroup.blocksRaycasts = false;
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = eventData.position;
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = startPosition;

        canvasGroup.blocksRaycasts = true;
    }
}
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
    [Header("親Canvas")]
    private Canvas parentCanvas = null;

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

    //元の親
    private Transform originalParent = null;

    //このUIが表すコア
    private SoulCore soulCore;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Awake()
    {
        parentCanvas = GetComponentInParent<Canvas>();
    }

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
    /// ドラッグ開始時処理
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        //元の位置を保存
        startPosition = rectTransform.anchoredPosition;

        //元の親を保存
        originalParent = transform.parent;

        //Canvas直下へ移動
        transform.SetParent(parentCanvas.transform);

        //一番前に表示
        transform.SetAsLastSibling();

        //ドロップ判定できるようRaycastを無効化
        canvasGroup.blocksRaycasts = false;
    }

    /// <summary>
    /// ドラッグ中処理
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        //マウス位置へ移動
        rectTransform.position = eventData.position;
    }

    /// <summary>
    /// ドラッグ終了時処理
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        //元の親へ戻す
        transform.SetParent(originalParent);

        //元の位置へ戻す
        rectTransform.anchoredPosition = startPosition;

        //Raycastを元に戻す
        canvasGroup.blocksRaycasts = true;
    }
}
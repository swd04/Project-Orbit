using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// コアをセットするスロットUIクラス
/// </summary>
public class CoreSlotUI : MonoBehaviour,IDropHandler
{
    [Header("")]
    [SerializeField] private Image slotImage = null;

    [Header("")]
    [SerializeField] private Color normalColor = Color.white;

    [Header("")]
    [SerializeField] private Color equippedColor = Color.red;

    /// <summary>
    /// 現在セットされているコア
    /// </summary>
    private SoulCore currentCore = null;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        slotImage.color = normalColor;
    }

    /// <summary>
    /// コアをドロップした時の処理
    /// </summary>
    public void OnDrop(PointerEventData eventData)
    {


        //ドラッグ中のコアUI取得
        CoreItemUI item = eventData.pointerDrag.GetComponent<CoreItemUI>();

        //取得できなければ終了
        if (item == null)
        {
            return;
        }

        //コアをセット
        currentCore = item.GetSoulCore();

        //色を赤に
        slotImage.color = equippedColor;

        Debug.Log(currentCore.name + " をセット");
    }
}
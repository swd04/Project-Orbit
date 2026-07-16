using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// コアをセットするスロットUIクラス
/// </summary>
public class CoreSlotUI : MonoBehaviour, IPointerClickHandler
{
    [Header("スキルスロット")]
    [SerializeField] private Image slotImage = null;

    [Header("未セット時の色")]
    [SerializeField] private Color normalColor = Color.white;

    [Header("セット時の色")]
    [SerializeField] private Color equippedColor = Color.red;

    [Header("スロット管理")]
    [SerializeField] private CoreSlotManager slotManager = null;

    /// <summary>
    /// 現在セットされているコア
    /// </summary>
    private SoulCore currentCore = null;

    /// <summary>
    /// スロットが空いているか
    /// </summary>
    public bool IsEmpty => currentCore == null;

    /// <summary>
    /// 現在セットされているコア
    /// </summary>
    public SoulCore CurrentCore => currentCore;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        //スロットを未セット状態の色にする
        slotImage.color = normalColor;
    }

    /// <summary>
    /// コアをスロットへセットする処理
    /// </summary>
    public bool SetCore(SoulCore core)
    {
        //コアが存在しない場合
        if (core == null)
        {
            return false;
        }

        //既にコアがセットされている場合
        if (currentCore != null)
        {
            return false;
        }

        //コアをセット
        currentCore = core;

        //スロット色をセット状態に変更
        slotImage.color = equippedColor;

        Debug.Log(currentCore.name + " をセット");

        return true;
    }

    /// <summary>
    /// スロット右クリック時の処理
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        //右クリック以外は無視
        if (eventData.button != PointerEventData.InputButton.Right)
        {
            return;
        }

        //何もセットされていなければ終了
        if (currentCore == null)
        {
            return;
        }

        Debug.Log(currentCore.name + " を外しました");

        //スロットを空にする
        Clear();

        //スロットを前詰めする
        slotManager.SortSlots();
    }

    /// <summary>
    /// スロットを空にする
    /// </summary>
    public void Clear()
    {
        //セット中のコアを削除
        currentCore = null;

        //未装備状態の色へ戻す
        slotImage.color = normalColor;
    }
}
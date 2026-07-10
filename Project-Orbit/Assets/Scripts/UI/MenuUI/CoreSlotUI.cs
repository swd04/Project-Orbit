using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// コアをセットするスロットUIクラス
/// </summary>
public class CoreSlotUI : MonoBehaviour,IDropHandler, IPointerClickHandler
{
    [Header("")]
    [SerializeField] private Image slotImage = null;

    [Header("")]
    [SerializeField] private Color normalColor = Color.white;

    [Header("")]
    [SerializeField] private Color equippedColor = Color.red;

    [Header("スキル管理")]
    [SerializeField] private SkillActivation skillActivation = null;

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
        Debug.Log("OnDrop");

        //既にセット済みなら置けない
        if (currentCore != null)
        {
            Debug.Log("このスロットには既にコアがセットされています");
            return;
        }

        //ドラッグ中のコアUI取得
        CoreItemUI item = eventData.pointerDrag.GetComponent<CoreItemUI>();

        //取得できなければ終了
        if (item == null)
        {
            return;
        }

        //コアをセット
        currentCore = item.GetSoulCore();

        Debug.Log(currentCore);
        Debug.Log(currentCore.coreType);
        Debug.Log(currentCore.Skill);
        Debug.Log(skillActivation);

        //
        if (currentCore.coreType ==CoreType.AttackMotion)
        {
            //
            skillActivation.EquipSkill(currentCore.Skill);
        }

        //色を赤に
        slotImage.color = equippedColor;

        Debug.Log(currentCore.name + " をセット");
    }

    /// <summary>
    /// 
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

        //攻撃モーションコアならスキル解除
        if (currentCore.coreType == CoreType.AttackMotion)
        {
            skillActivation.UnequipSkill(currentCore.Skill);
        }

        Debug.Log(currentCore.name + " を外しました");

        //コアを外す
        currentCore = null;

        //スロット色を戻す
        slotImage.color = normalColor;
    }
}
using System.Collections.Generic;
using UnityEngine;

public class SkillActivation : MonoBehaviour
{
    [Header("プレイヤーのスキルリスト")]
    [SerializeField] private List<SkillDate> playerSkillList = new List<SkillDate>();

    [Header("ホイールの回転する値を取得")]
    [SerializeField] private float mouseScrollWheelValue = 0.0f;

    [Header("現在選んでいるスキルナンバー")]
    [SerializeField] private int currentSelectSkillNumber = 0;

    [Header("スキルホイールUI")]
    [SerializeField] private SkillWheelUI skillWheelUI = null;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        //スキルホイールUIを初期状態で表示
        skillWheelUI.Initialize(playerSkillList, 0);
    }

    private void Update()
    {
        mouseScrollWheelValue = Input.GetAxis("Mouse ScrollWheel");


        SelectSkill();
        ActivationSkill();
    }

    private void SelectSkill()
    {
        if (mouseScrollWheelValue > 0)
        {
            //ホイールを上に動かした場合
            //currentSelectSkillNumber = (currentSelectSkillNumber + 1) % playerSkillList.Count;

            //UIに選択変更を通知
            skillWheelUI.RotateCounterClockwise();
        }
        else if (mouseScrollWheelValue < 0)
        {
            //ホイールを下に動かした場合
            //currentSelectSkillNumber = (currentSelectSkillNumber - 1 + playerSkillList.Count) % playerSkillList.Count;

            //UIに選択変更を通知
            skillWheelUI.RotateClockwise();
        }
    }

    private void ActivationSkill()
    {
        //スキルの発動処理
        if (Input.GetMouseButtonDown(1))
        {
            SkillDate skill = skillWheelUI.CurrentSkill;

            if (skill == null) return;

            //右クリックでスキル発動
            Debug.Log("スキル " + skill.skillName + " を発動しました！");
        }
    }
}
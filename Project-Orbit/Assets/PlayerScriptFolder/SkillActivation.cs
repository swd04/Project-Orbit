using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillActivation : MonoBehaviour
{
    [Header("プレイヤーのスキルリスト")]
    [SerializeField] private List<SkillDate> playerSkillList = new List<SkillDate>();

    [Header("ホイールの回転する値を取得")]
    [SerializeField] private float mouseScrollWheelValue = 0.0f;

    [Header("現在選んでいるスキルナンバー")]
    [SerializeField] private int currentSelectSkillNumber = 0;



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
            // ホイールを上に動かした場合

            currentSelectSkillNumber = (currentSelectSkillNumber + 1) % playerSkillList.Count;
        }
        else if (mouseScrollWheelValue < 0)
        {
            // ホイールを下に動かした場合
            currentSelectSkillNumber = (currentSelectSkillNumber - 1 + playerSkillList.Count) % playerSkillList.Count;
        }
    }


    private void ActivationSkill()
    {
        // スキルの発動処理
        if (Input.GetMouseButtonDown(1))
        {
            // 右クリックでスキル発動
            Debug.Log("スキル " + playerSkillList[currentSelectSkillNumber].skillName + " を発動しました！");
        }
    }

}

using UnityEngine;

[CreateAssetMenu(fileName = "SkillDate", menuName = "Scriptable Objects/SkillDate")]
public class SkillDate : ScriptableObject
{
    [Header("スキル名")]
    [SerializeField] public string skillName = "";

    [Header("スキルアイコン")]
    //スキルホイールUIなどで表示
    public Sprite icon = null;

    [Header("スキルのクールタイム")]
    [SerializeField] public float skillCoolTime = 0.0f;

    //[Header("スキル範囲")]
    //[SerializeField] public GameObject skillRange = null;

    [Header("スキルの火力")]
    [SerializeField] public float skillPower = 0.0f;

}
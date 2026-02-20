using UnityEngine;

/// <summary>
/// プレイヤーの走る判定を処理するクラス
/// </summary>
public class PlayerDash : MonoBehaviour
{
    [Header("プレイヤーのダッシュ判定")]
    [SerializeField] public bool isDashPlayer = false;

    private void Start()
    {
        isDashPlayer = false;
    }

    
    private void Update()
    {
        // プレイヤーのダッシュ判定を行うメソッドを呼び出す処理
        PlayerDashJudge();
    }

    /// <summary>
    /// プレイヤーの走る判定を行うメソッド
    /// </summary>
    private void PlayerDashJudge()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isDashPlayer = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isDashPlayer = false;
        }
    }
}
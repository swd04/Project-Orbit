using UnityEngine;

/// <summary>
/// プレイヤーのカメラ操作を処理するクラス
/// </summary>
public class PlayerCameraController : MonoBehaviour
{
    // 追尾対象となるプレイヤーオブジェクト
    [Header("追尾設定")]
    [SerializeField] private GameObject playerObject = default;

    // マウス移動に対するカメラ回転速度（X:横回転, Y:縦回転）
    [Header("回転設定")]
    [SerializeField] private Vector2 rotationSpeed = Vector2.zero;

    // 下向き最大角度
    // 上向き最大角度
    [Header("縦回転制限")]
    [SerializeField] private float minPitch = 0.0f;
    [SerializeField] private float maxPitch = 0.0f;

    // 前フレームのプレイヤー座標
    [SerializeField] private Vector3 lastTargetPosition = Vector3.zero;

    // マウスホイール入力値（ズーム量）
    //[SerializeField] private float zoom = 0.0f;

    [SerializeField] private float currentPitch = 0.0f;

    private void Start()
    {
        // カーソルをロックする
        Cursor.lockState = CursorLockMode.Locked;

        // カーソルを非表示にする
        Cursor.visible = false;

        // zoom = 0.0f;
       
        lastTargetPosition = playerObject.transform.position;

        // 初期の縦角度を取得
        currentPitch = transform.eulerAngles.x;

        if (currentPitch > 180f)
        {
            currentPitch -= 360f;
        }
    }

    private void Update()
    {
        Rotate();
        //Zoom();
    }

    private void Rotate()
    {
        // プレイヤー移動分だけカメラも追従
        transform.position += playerObject.transform.position - lastTargetPosition;
        lastTargetPosition = playerObject.transform.position;

        // マウス移動量を取得
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // 横回転
        float yaw = rotationSpeed.x * mouseX;
        transform.RotateAround(playerObject.transform.position, Vector3.up, yaw);

        // 縦回転
        float pitchDelta = -rotationSpeed.y * mouseY;

        // 制限をかける
        float targetPitch = Mathf.Clamp(currentPitch + pitchDelta, minPitch, maxPitch);

        // 現在の縦角度から目標の縦角度への差分を計算して適用
        float applyDelta = targetPitch - currentPitch;
        currentPitch = targetPitch;

        // プレイヤーを中心にして回転
        transform.RotateAround(playerObject.transform.position, transform.right, applyDelta);
    }

    //// ズーム処理
    //private void Zoom()
    //{
    //    zoom = Input.GetAxis("Mouse ScrollWheel");

    //    Vector3 offset = Vector3.zero;
    //    Vector3 pos = playerObject.transform.position - transform.position;

    //    if (zoom > 0)
    //    {
    //        // ズームイン
    //        offset = pos.normalized * 1;
    //    }
    //    else if (zoom < 0)
    //    {
    //        // ズームアウト
    //        offset = -pos.normalized * 1;
    //    }

    //    transform.position += offset;
    //}
}

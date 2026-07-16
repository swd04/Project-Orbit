using UnityEngine;
using UnityEngine.UI;

public class NameInputManager : MonoBehaviour
{
    [Header("確定ボタンの取得")]
    [SerializeField] private Button enterButton = null;

    private PlayerData player = new PlayerData();

    private void Start()
    {
        if (enterButton != null)
        {
            Debug.LogError("EnterButtonがアタッチされていません。");
        }


    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (player.playerNameData == "")
            {
                return;
            }
            else
            {
                OnEnterButtonClicked();
            }
        }
    }

    private void OnEnterButtonClicked()
    {

    }
}

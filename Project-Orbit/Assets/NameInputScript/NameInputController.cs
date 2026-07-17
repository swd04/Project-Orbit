using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NameInputController : SingletonBehaviour<NameInputController>
{
    [Header("入力した名前の取得")]
    [SerializeField] private TMP_InputField nameInputField = null;

    [Header("名前の長さ制限")]
    [SerializeField] private int maxNameLength = 0;



    private void Start()
    {
        // 入力フィールドの初期化
        nameInputField.text = "";

        nameInputField.characterLimit = maxNameLength;


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GetPlayerName();

            SceneManager.LoadScene("TimeAttackMainScene");
        }
    }


    /// <summary>
    /// 入力した名前を取得するメソッド
    /// </summary>
    public string GetPlayerName()
    {
        if (nameInputField.text == "")
        {
            // UIで名前の入力を促す

            Debug.Log("名前を入力して");

            return null;
        }
        else
        {
            Debug.Log("入力された名前: " + nameInputField.text);

            // 入力された名前を取得
            PlayerData playerData = new PlayerData();
            playerData.playerNameData = nameInputField.text;

            return playerData.playerNameData;
        }
       
    }
}

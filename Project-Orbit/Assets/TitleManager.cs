using UnityEngine;

public class TitleManager : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        //　データ消す
        if (Input.GetKeyDown(KeyCode.F1))
        {
            RankingManager.Instance.DeleteSaveData();
        }
    }
}

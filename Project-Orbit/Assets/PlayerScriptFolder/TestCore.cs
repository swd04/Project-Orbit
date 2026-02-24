using UnityEngine;

/// <summary>
/// コア取得のテスト用クラス
/// 
/// </summary>
public class TestCore : MonoBehaviour
{
    [Header("コアのプレハブ")]
    [SerializeField] private GameObject corePrefab = null;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Instantiate(corePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }



}

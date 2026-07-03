using UnityEngine;

/// <summary>
/// メニューのコア所持一覧表示クラス
/// </summary>
public class MenuCoreCollectionUI : MonoBehaviour
{
    [Header("コア管理")]
    [SerializeField] private CoreCollection coreCollection = null;

    [Header("コア一覧Prefab")]
    [SerializeField] private CoreItemUI itemPrefab = null;

    [Header("一覧生成先")]
    [SerializeField] private Transform content = null;

    /// <summary>
    /// UI有効化時処理
    /// </summary>
    private void OnEnable()
    {
        Refresh();
    }

    /// <summary>
    /// コア一覧更新処理
    /// </summary>
    public void Refresh()
    {
        //以前生成した一覧を削除
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        //全種類のコアを確認
        foreach (SoulCore.ActionType type in System.Enum.GetValues(typeof(SoulCore.ActionType)))
        {
            //Noneは表示しない
            if (type == SoulCore.ActionType.None)
            {
                continue;
            }

            //種類ごとの所持数取得
            int count = coreCollection.GetCoreCount(type);

            //所持していない場合は表示しない
            if (count <= 0)
            {
                continue;
            }

            //一覧項目生成
            CoreItemUI item = Instantiate(itemPrefab, content);

            //種類に対応するコアを取得
            SoulCore core = coreCollection.GetSoulCore(type);

            //表示内容設定
            item.SetData(core, count);
        }
    }
}
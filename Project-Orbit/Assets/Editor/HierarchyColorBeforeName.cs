#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

/// <summary>
/// 階層の深さに応じたカラーバーを表示するエディタ拡張
/// </summary>
[InitializeOnLoad]
public class HierarchyColorBeforeName
{
    /// <summary>
    /// エディタ起動時に一度だけ呼ばれる静的コンストラクタ
    /// </summary>
    static HierarchyColorBeforeName()
    {
        //Hierarchyの各要素が描画されるタイミングで呼ばれるイベントに登録
        EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemOnGUI;
    }

    /// <summary>
    /// Hierarchyの各アイテムが描画されるたびに呼ばれるコールバック処理
    /// </summary>
    private static void OnHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        //InstanceIDからUnityEngine.Objectを取得し、GameObjectにキャスト
        GameObject gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

        //GameObjectでない場合は処理しない
        if (gameObject != null)
        {
            //Hierarchy上での階層の深さを計算
            int depth = CalculateDepth(gameObject);

            //1階層あたりのインデント幅
            int indentWidth = 2;

            //階層の深さに応じたインデント量
            int totalIndent = indentWidth * depth;

            //階層が深くなるほど少し左にずらすための補正値
            int offset = depth * 2;

            //オブジェクト名の左側に描画するカラーバーのRect
            Rect colorRect = new Rect(
                selectionRect.x + totalIndent - offset,
                selectionRect.y,
                3,
                selectionRect.height);

            //階層の深さに応じて色相が変わるカラーを生成
            Color color = Color.HSVToRGB(
                (depth * 0.1f) % 1.0f,
                0.4f,
                0.95f);

            //カラーバーをHierarchy上に描画
            EditorGUI.DrawRect(colorRect, color);
        }
    }

    /// <summary>
    ///  GameObjectの親をたどって、Hierarchy上の階層の深さを計算する処理
    /// </summary>
    private static int CalculateDepth(GameObject obj)
    {
        int depth = 0;

        //親が存在する限りたどる
        while (obj.transform.parent != null)
        {
            depth++;
            obj = obj.transform.parent.gameObject;
        }

        return depth;
    }
}
#endif
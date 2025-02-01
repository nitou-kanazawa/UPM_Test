#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.AssetImporters;

namespace Nitou.Tools.CodeGeneration{

    [ScriptedImporter(1, "cstmp")]   // ※バージョン番号と拡張子を指定
    public class CodeTemplateImporter : ScriptedImporter {

        public override void OnImportAsset(AssetImportContext ctx) {

            // インポート対象のファイルパス
            string filePath = ctx.assetPath;
            string fileName = PathUtils.GetFileName(filePath);

            // アセットをインポートする処理（例: テキストファイルの内容を読み込む）
            string fileContent = System.IO.File.ReadAllText(filePath);

            // 読み込んだ内容を保持する ScriptableObject の生成
            CodeTemplateAsset asset = ScriptableObject.CreateInstance<CodeTemplateAsset>();
            asset.data = fileContent;

            // アセットをコンテキストに登録し、Unityに認識させる
            ctx.AddObjectToAsset(fileName, asset);
            ctx.SetMainObject(asset);

        }

    }


}
#endif
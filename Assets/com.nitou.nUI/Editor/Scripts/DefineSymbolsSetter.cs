#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.Build;

// [REF]
//  はなちる: Player SettingsのScriptingDefineSymbolsをスクリプトから取得・設定する方法 https://www.hanachiru-blog.com/entry/2024/06/03/120000

namespace Nitou.EditorScripts {

    /// <summary>
    /// 
    /// </summary>
    [InitializeOnLoad]
    internal static class DefineSymbolsSetter{

        // 定義したいシンボル
        private const string SYMBOL_USN_ASYNC = "USN_USE_ASYNC_METHODS";
        private const string SYMBOL_UNITASK_DOTWEEN = "UNITASK_DOTWEEN_SUPPORT";


        /// <summary>
        /// コンストラクタ
        /// </summary>
        static DefineSymbolsSetter() {

            // ビルドターゲット
            var buildTarget = NamedBuildTarget.FromBuildTargetGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            if (buildTarget == NamedBuildTarget.Unknown) return;

            // パッケージがインストールされたときに実行される処理

            AddDefineSymbolsIfNeeded(buildTarget, SYMBOL_USN_ASYNC);
            AddDefineSymbolsIfNeeded(buildTarget, SYMBOL_UNITASK_DOTWEEN);
            //AddDefineSymbolsIfNeeded(buildTarget, "UNITASK_DOTWEEN_SUPPORT");
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        /// <summary>
        /// シンボルを定義する
        /// </summary>
        private static void AddDefineSymbolsIfNeeded(NamedBuildTarget buildTarget, string symbol) {

            // ビルドターゲットの定義済みのシンボルを取得
            string defines = PlayerSettings.GetScriptingDefineSymbols(buildTarget);

            // シンボルがすでに定義されていないかチェック
            if (!defines.Contains(symbol)) {
                // シンボルが未定義なら追加
                defines += ";" + symbol;

                // シンボルを設定
                PlayerSettings.SetScriptingDefineSymbols(buildTarget, symbol);

                // デバッグ用ログ
                Debug.Log($"Added define symbol: {symbol}");
            } else {
                //Debug.Log($"{SYMBOL_TO_DEFINE} is already defined.");
            }
        }
    }
}
#endif

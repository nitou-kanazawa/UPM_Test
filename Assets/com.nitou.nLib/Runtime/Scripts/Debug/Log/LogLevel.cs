using UnityEngine;

namespace Nitou
{
    /// <summary>
    /// ログの分類．
    /// </summary>
    public enum LogLevel
    {
        // 開発中の Debug 用ログ表示に使用
        Debug,
        // 通常のログ表示に使用
        Info, 
        // 警告表示に使用
        Warning,
        // エラー表示に使用
        Error,
    }


    /// <summary>
    /// Logger 用のタグ種類。
    /// </summary>
    public enum LoggerTag {
        GENERAL,
        AUDIO,
        ANIMATION,
    }
}

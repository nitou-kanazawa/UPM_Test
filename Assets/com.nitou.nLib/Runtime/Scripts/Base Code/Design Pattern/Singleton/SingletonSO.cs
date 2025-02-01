using UnityEngine;

// [REF]
//  youtube: The BEST Unity Feature You Don't Know About - Scriptable Object Singletons Tutorial https://www.youtube.com/watch?v=6kWUGEQiMUI&t=100s
//  github: ciwolsey/ScriptableObjectSingleton.cs https://gist.github.com/ciwolsey/3bd0189a8bbc76e3f7242b51473ff3f6
//  _: シングルトンなScriptableObjectを実装する https://mackysoft.net/singleton-scriptableobject/

namespace Nitou.DesignPattern.Singltons {

    /// <summary>
    /// グローバルアクセスを持つシングルトンのScriptable Object
    /// ※唯一性はコードで担保されていないのに注意
    /// </summary>
    public class SingletonSO<T> : ScriptableObject 
        where T : SingletonSO<T> {

        private static T _instance;
        public static T Instance {
            get {
                if (_instance == null) {
                    T[] assets = Resources.LoadAll<T>("");

                    if (assets == null || assets.Length < 1) {
                        throw new System.Exception($"Resoucesフォルダ内に{typeof(T)}型のアセットが見つかりませんでした");

                    } else if (assets.Length > 1) {
                        Debug.LogWarning($"{typeof(T)}型のアセットが複数存在しています");
                    }
                    _instance = assets[0];
                }

                return _instance;
            }
        }
    }
}

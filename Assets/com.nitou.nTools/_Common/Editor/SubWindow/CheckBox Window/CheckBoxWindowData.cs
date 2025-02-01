#if UNITY_EDITOR
namespace Nitou.Tools.Shared {

    public interface ICheckBoxWindowData {

        /// <summary>
        /// データ名
        /// </summary>
        string Name { get; }

        /// <summary>
        /// チェックの有無
        /// </summary>
        bool IsChecked { get; set; }
    }


    public sealed class CheckBoxWindowData : ICheckBoxWindowData {

        public string Name { get; set; }
        public bool IsChecked { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CheckBoxWindowData(string name, bool isChecked) {
            Name = name;
            IsChecked = isChecked;
        }
    }
}

#endif
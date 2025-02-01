
namespace Nitou {

    /// <summary>
    /// UnityProject内のデータ、またはディレクトリを指すパスのインターフェース
    /// </summary>
    public interface IUnityProjectPath{

        /// <summary>
        /// Convert to project path that start with "Assets/".
        /// </summary>
        string ToProjectPath();

        /// <summary>
        /// Covert to absolute path.
        /// </summary>
        string ToAbsolutePath();
    }
}

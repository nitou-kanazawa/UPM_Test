
namespace Nitou.Tools.ProjectWindow {

    /// <summary>
    /// OutGame用のスクリプトフォルダ構成
    /// </summary>
    [System.Serializable]
    public class ScriptsDirectoryFlags : IFlagContainer {
        public bool Composition = true;
        public bool Model = true;
        public bool UseCase = false;
        public bool View = true;
        public bool Presentation = true;
        public bool Foundation = true;
        public bool APIGateway = false;
    }

}

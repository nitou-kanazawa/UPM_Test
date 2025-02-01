
namespace Nitou.Tools.ProjectWindow {

    /// <summary>
    /// 
    /// </summary>
    [System.Serializable]
    public class ProjectDirectoryFlags : IFlagContainer{
        public bool Scritpts = true;
        public bool Editor = true;
        public bool Prefabs = true;
        public bool Resources = false;
        public bool ScriptableObjects = false;
        public bool UI = false;
        public bool Audios = false;
        public bool Textures = false;
    }


}

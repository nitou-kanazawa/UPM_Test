
namespace Nitou.BachProcessor{
    
    public interface ISystemBase{
        
        /// <summary>
        /// Procesing order.
        /// </summary>
        int Order { get; }
    }


    public interface IEarlyUpdate : ISystemBase {
        void OnUpdate();
    }

    public  interface IPostUpdate : ISystemBase {
        void OnLateUpdate();
    }
}

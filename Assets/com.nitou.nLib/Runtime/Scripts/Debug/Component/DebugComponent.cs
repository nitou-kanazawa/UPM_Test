using UnityEngine;

namespace Nitou.DebugInternal{

    [DisallowMultipleComponent]
    public abstract class DebugComponent: MonoBehaviour{
    }


    [DisallowMultipleComponent]
    public abstract class DebugComponent<TComponent> : MonoBehaviour 
        where TComponent : Component{
    }
}

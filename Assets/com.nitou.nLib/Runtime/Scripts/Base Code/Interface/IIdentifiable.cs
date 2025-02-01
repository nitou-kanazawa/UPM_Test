using System;

namespace Nitou{

    /// <summary>
    /// 識別可能なオブジェクト．
    /// </summary>
    public interface IIdentifiable{
        Guid guid { get; }
    }
}

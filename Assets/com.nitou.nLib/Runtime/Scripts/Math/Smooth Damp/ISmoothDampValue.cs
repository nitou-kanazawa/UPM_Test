using System.Collections.Generic;

namespace Nitou{

    public interface ISmoothDampValue<TValue>
        where TValue : struct{

        /// <summary>
        /// 平滑化された値を取得する．
        /// </summary>
        public TValue GetNext(TValue target, float smoothTime);

        /// <summary>
        /// 平滑化された値を取得する．
        /// </summary>
        public TValue GetNext(TValue target, float smoothTime, float maxSpeed);

        /// <summary>
        /// 平滑化された値を取得する．
        /// </summary>
        public TValue GetNext(TValue target, float smoothTime, float maxSpeed, float deltaTime);
    }
}

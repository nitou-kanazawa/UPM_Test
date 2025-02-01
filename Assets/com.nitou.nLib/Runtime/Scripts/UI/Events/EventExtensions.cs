using UnityEngine;

namespace Nitou{


    public static class EventExtensions{


        /// ----------------------------------------------------------------------------
        // EventType

        public static bool IsRepaintOrLayout(this EventType self) {
            return self == EventType.Repaint || self == EventType.Layout; 
        }

    }
}

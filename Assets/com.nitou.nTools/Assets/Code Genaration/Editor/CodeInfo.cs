using System.Collections.Generic;
using UnityEngine;

namespace Nitou.Tools.CodeGeneration{

    public sealed class CodeInfo{
        public string className;
        public string namespaceName;


        public CodeInfo(string className, string namespaceName) {
            this.className = className;
            this.namespaceName = namespaceName;
        }
    }

}

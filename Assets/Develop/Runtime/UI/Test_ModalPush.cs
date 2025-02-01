using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityScreenNavigator.Runtime.Core.Modal;
using Nitou;
using Nitou.UI;
using UnityScreenNavigator.Runtime.Foundation.Coroutine;

namespace Project
{
    public class Test_ModalPush : MonoBehaviour
    {

        private string containerKey = "MainModalContainer";
        private string modalKey = "Prefab_ModalTemplate";


        async void Start()
        {
            await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

            var container = ModalContainer.Find(containerKey);
            var modal = await container.PushModal<Modal>(modalKey, true);
            Debug.Log($"modal : {modal}");
            
            await UniTask.WaitForSeconds(3);


            await container.Pop(true);

        }

    }
}

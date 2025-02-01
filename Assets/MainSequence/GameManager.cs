using System;
using UnityEngine;
using Nitou.GameSystem; // 修正版のSimpleProcessBaseクラスを使用

public class GameManager : MonoBehaviour {
    [SerializeField] private PauseMenuView pauseMenuController;

    private IProcess gameProcess;

    private async void Start() {
        // ゲームプロセスを作成
        gameProcess = new GameProcess();

        // ポーズメニューを初期化
        pauseMenuController.Initialize(Unpause, Quit);

        // ゲーム開始
        gameProcess.Run();

        var result = await gameProcess.ProcessFinished;
    }

    private void Update() {
        // ポーズ操作
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (gameProcess.State.Value == ProcessState.Running) {
                Pause();
            } 
            else if (gameProcess.State.Value == ProcessState.Paused) {
                Unpause();
            }
        }
    }

    private void Pause() {
        gameProcess.Pause();
        pauseMenuController.ShowPauseMenu();
    }

    private void Unpause() {
        pauseMenuController.HidePauseMenu();
        gameProcess.UnPause();
    }

    private void Quit() {
        gameProcess.Cancel(new CancelResult("User quit the game."));
        Application.Quit();
    }
}


public class GameProcess : SimpleProcessBase {
    protected override void OnStart() {
        Debug.Log("Game started!");
    }

    protected override void OnPause() {
        Debug.Log("Game paused!");
        Time.timeScale = 0f; // 時間を止める

    }

    protected override void OnUnPause() {
        Debug.Log("Game resumed!");
        Time.timeScale = 1f; // 時間を再開
    }

    protected override void OnCancel(CancelResult cancelResult) {
        Debug.Log($"Game cancelled: {cancelResult.Message}");
    }

    protected override void OnDispose() {
        Debug.Log("Game process disposed.");
    }
}

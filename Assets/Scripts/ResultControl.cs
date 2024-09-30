using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

namespace Trash {
    public class ResultControl : MonoBehaviour {

        [SerializeField] private GameTimer gameTimer;
        [SerializeField] private LeaderBoard leaderBoard;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text timeText;

        [SerializeField] private GameState leaderBoardState;

        private string playerName = string.Empty;
        

        private void OnEnable() {
            Keyboard.current.onTextInput += CharInput;
            leaderBoard.LoadLeaderBoard();
            timeText.text = string.Format("{0:0.00}", gameTimer.Elapsed);
        }

        private void OnDisable() {
            Keyboard.current.onTextInput -= CharInput;
            leaderBoard.SaveLeaderBoard();
        }

        private void CharInput(char c) {
            if (c == '\b') {
                if (playerName.Length != 0) {
                    playerName = playerName.Substring(0, playerName.Length - 1);
                    nameText.text = playerName;
                }
            } else if ((c == '\n') || (c == '\r')) {
                DoneNameInput();
            } else {
                playerName += c;
                nameText.text = playerName;
            }
        }

        private void DoneNameInput() {
            leaderBoard.TryInsertEntry(new LeaderBoardEntry { Name = playerName, Time = gameTimer.Elapsed });
            SceneSelector.Instance.ChangeState(leaderBoardState);
        }


    }

}
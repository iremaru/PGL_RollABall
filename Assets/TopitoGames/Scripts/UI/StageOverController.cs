using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TopitoGames
{
    public class StageOverController : MonoBehaviour
    {
        [SerializeField] GameObject gameOverPanel;
        [SerializeField] TMP_Text scoreText;
        [SerializeField] TMP_Text timeMessage;
        const string TIME_MESSAGE = "En {time} segundos";

        #region UNITY METHODS

            private void OnEnable() {
                GameManager.getReadyToStart += InitStage;
                GameManager.gameIsOver += OnStageOver;
                GameManager.onGameOverGetScore += SetScore;
                GameManager.onGameOverGetTime += SetTime;
            }

            private void OnDisable() {
                GameManager.getReadyToStart -= InitStage;
                GameManager.gameIsOver -= OnStageOver;
                GameManager.onGameOverGetScore -= SetScore;
                GameManager.onGameOverGetTime -= SetTime;
            }

        #endregion

        #region PRIVATE METHODS
            void RePlay() => GameManager.Instance.Replay();

            void InitStage() => gameOverPanel.SetActive(false);
            void OnStageOver() => gameOverPanel.SetActive(true);
            void SetTime(int time) => timeMessage.text = TIME_MESSAGE.Replace("{time}", time.ToString()) ;
            void SetScore(int score) => scoreText.text = score.ToString();
        #endregion
    }
}

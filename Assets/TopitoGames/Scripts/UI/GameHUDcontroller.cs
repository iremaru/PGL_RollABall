using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameHUDcontroller : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text timeText;
    [SerializeField] Image timeImage;
    [SerializeField] TMP_Text healthText;
    [SerializeField] Image healthImage;
    float maxTime = -1;

    #region UNITY METHODS

    private void OnEnable() {
            GameManager.onScoreChange += ChangeScoreText;
            GameManager.onGameCountDownChange += ChangeTimeText;
            GameManager.onHealthChange += ChangeHealthText;
        }

        private void OnDisable() {
            GameManager.onScoreChange -= ChangeScoreText;
            GameManager.onGameCountDownChange -= ChangeTimeText;
            GameManager.onHealthChange -= ChangeHealthText;
        }
    #endregion

    #region PRIVATE METHODS
        
        private void ChangeScoreText(int value){
            scoreText.text = value.ToString("000");
        }
        
        private void ChangeTimeText(int value){
            if( maxTime == -1 ) maxTime = value;
            timeText.text = value.ToString("000");
            timeImage.fillAmount = (float)(value) / maxTime;
        }

        private void ChangeHealthText(int value){
            healthText.text = value.ToString("000");
            healthImage.fillAmount = (float)(value) / 100;
        }

    #endregion
}

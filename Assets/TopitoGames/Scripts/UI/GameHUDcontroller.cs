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

    #region UNITY METHODS
        
        private void OnEnable() {
            GameManager.onScoreChange += ChangeScoreText;
            GameManager.onCountDownChange += ChangeTimeText;
        }

        private void OnDisable() {
            GameManager.onScoreChange -= ChangeScoreText;
            GameManager.onCountDownChange -= ChangeTimeText;
        }
    #endregion

    #region PRIVATE METHODS
        
        private void ChangeScoreText(int value){
            scoreText.text = value.ToString("000") ;
        }
        
        private void ChangeTimeText(int value){
            timeText.text = value.ToString("000") ;
            timeImage.fillAmount = (float)(value) / 100;
        }

    #endregion
}

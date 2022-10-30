using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountDownController : MonoBehaviour
{
    [SerializeField] GameObject countDown;
    [SerializeField] TMP_Text countDownText;
    [SerializeField] Image circleInner;
    [SerializeField] Image circleOuter;
    [SerializeField] AudioClip countDownAudio;

    bool isSounding = false;

    #region UNITY METHODS
    private void OnEnable() {
            GameManager.getReadyToStart += GetReady;
            GameManager.onReadyCountDownChange += SetCountDownText;
        }

        private void OnDisable() {
            GameManager.onReadyCountDownChange -= SetCountDownText;
            GameManager.getReadyToStart -= GetReady;
        }

    #endregion

    #region PRIVATE METHODS

        void GetReady()
        {
            isSounding = false;
            countDown.SetActive(true);
        }
        void SetCountDownText(int value)
        {
            if(!isSounding)
            {
                isSounding = true;
                GameManager.Instance.PlaySFXSound( countDownAudio );
            }
            countDownText.text = value < 0? "-" : value.ToString();
            
            if(value == -1) countDown.SetActive(false);

            Image circleToChange = value%2 == 0 ? circleOuter: circleInner;
            float alpahValue = value%3 == 0 ? 0.5f : 1f;
            ChangeCircleAlpha(circleToChange, alpahValue);
        }

        void ChangeCircleColor( Image circle, Color color)
        {
            circle.color = color;
        }

        void ChangeCircleAlpha(Image circle, float value)
        {
            circle.color = new Color(circle.color.r, circle.color.g, circle.color.b, value );
        }
    #endregion
}

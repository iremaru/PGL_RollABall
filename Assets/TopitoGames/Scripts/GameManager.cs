using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Properties
        const int INITIAL_TIME = 100;
        const int INITIAL_SCORE = 0;
        const int INITIAL_HEALTH = 100;
        const int SCORE_WIN_CONDITION = 100;
        const int HEALTH_REGENERATE_RATIO = 1;
        const int HEALTH_HEAL_TIME = 1;

        static int timeCountDown;
        static int score;
        static int health;

        static bool gameIsPlaying = false;

    #endregion


    #region Observer pattern
        public delegate void Report(int value);
        public delegate void PlayAudio(AudioClip clip);
        public delegate void Alert();
        public static Report onScoreChange;
        public static Report onCountDownChange;
        public static Report onHealthChange;
        public static PlayAudio onSFXsound;
        public static Alert gameIsOver;
        public static Alert gameIsStarting;

    #endregion

    #region UNITY METHODS
        private void Start() {
            StargGame();
        }

    #endregion


    #region PUBLIC METHODS
        
        public static void IncrementScore(int increment)
        {
            score += increment;
            if(onScoreChange != null)
                onScoreChange(score);
        }

        public static void DecrementHealth(int value)
        {
            SetHealth(health -= value);
        }

        public static void PlaySFXSound(AudioClip clip){
            Debug.Log($"PickUpBehaviour me ha mandado: {clip.name}");

            if(onSFXsound != null)
                onSFXsound(clip);
        }
    #endregion

    #region PRIVATE METHODS

        private void StargGame()
        {
            gameIsPlaying = true;

            if( gameIsStarting != null )
                gameIsStarting();
                
            IncrementScore(INITIAL_SCORE);
            SetCountDownTime(INITIAL_TIME);
            StartCoroutine( RunCountDown() );
        }
        private static void EndGame()
        {
            gameIsPlaying = false;

            if( gameIsOver != null )
                gameIsOver();
        }

        private void SetCountDownTime(int newValue)
        {   
            timeCountDown = newValue;
            if(onCountDownChange != null)
                onCountDownChange(timeCountDown);
            if(timeCountDown < 1)
                EndGame();
        }

        private IEnumerator RunCountDown()
        {
            while (gameIsPlaying)
            {
                yield return new WaitForSeconds(1);
                SetCountDownTime( timeCountDown -1 ) ;
                
            }
        }

        //TODO: Implement damage
        private void RegenerateHealth()
        {
            if ( gameIsPlaying && health < 100 && health > 0)
            {
                SetHealth(health + HEALTH_REGENERATE_RATIO);
            }
        }

        static private void SetHealth(int newHealth)
        {
            if ( gameIsPlaying )
            {
                health = newHealth;
                if(onHealthChange != null)
                    onHealthChange(newHealth);
                if(health < 1)
                    EndGame();
            }
        }

    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Properties
        const int INITIAL_TIME = 50;
        const int INITIAL_READY_TIME = 3;
        const int INITIAL_SCORE = 0;
        const int INITIAL_HEALTH = 100;
        const int SCORE_WIN_CONDITION = 100;
        const int HEALTH_REGENERATE_RATIO = 1;
        const int HEALTH_HEAL_TIME = 1;

        ///GAME OVER CONDITIONS
        const int GAMEOVER_WIN_SCORE = 12;
        const int GAMEOVER_LOSE_SCORE = -1;
        const int GAMEOVER_LOSE_HEALTH = 0;
        const int GAMEOVER_LOSE_TIME = 0;

        enum GameOverResult
        {
            WinByScore,
            LoseByScore,
            LoseByHealth,
            LoseByTime
        }

        int readyTimeCountDown;
        int timeCountDown;
        int score;
        int health;

        static bool gameIsPlaying = false;

    #endregion

    #region Singleton
        public static GameManager Instance { get; private set; }

    #endregion

    #region Observer pattern
        public delegate void Report(int value);
        public static Report onScoreChange;
        public static Report onGameCountDownChange;
        public static Report onReadyCountDownChange;
        public static Report onHealthChange;
        public static Report onGameOverGetTime;
        /// <summary>
        /// Get the time spent between game start and game over.
        /// </summary>
        public static Report onGameOverGetScore;

    public delegate void PlayAudio(AudioClip clip);
        public static PlayAudio onSFXsound;

        public delegate void Alert();
        public static Alert gameIsOver;
        /// <summary>
        /// It will be called every time stage will start. 
        /// Whether it is the first play or is a replay.
        /// </summary>
        public static Alert getReadyToStart;
        public static Alert gameHasStarted;

    #endregion

    #region UNITY METHODS
        private void Awake() {
            if (Instance != null && Instance != this) Destroy(this);
            Instance = this;
        }
        private void Start() {
            InitGame();
        }

    #endregion

    #region PUBLIC METHODS
        
        public void IncrementScore(int increment)
        {
            SetScore(score + increment);
        }

        public void DecrementHealth(int value)
        {
            SetHealth(health - value);
            
        }

        public void PlaySFXSound(AudioClip clip){
            Debug.Log($"PickUpBehaviour me ha mandado: {clip.name}");

            if(onSFXsound != null)
                onSFXsound(clip);
        }
    
        public void Replay()
        {
            InitGame();
        }

        public void InstantDeath()
        {
            EndGame();
        }
    #endregion

    #region PRIVATE METHODS

        private void InitGame()
        {
            StopAllCoroutines();
            gameIsPlaying = true;

            if( getReadyToStart != null )
                getReadyToStart();
            
            SetHealth(INITIAL_HEALTH);
            SetScore(INITIAL_SCORE);
            SetCountDownTime(INITIAL_TIME);
            StartCoroutine( RunReadyCountDown(INITIAL_READY_TIME) );
        }
        private void EndGame()
        {
            gameIsPlaying = false;

            if( gameIsOver != null ) gameIsOver();
            if( onGameOverGetScore != null ) onGameOverGetScore(score);
            if( onGameOverGetTime != null ) onGameOverGetTime(INITIAL_TIME - timeCountDown);
    }

        private void SetCountDownTime(int newValue)
        {   
            timeCountDown = newValue;
            if(onGameCountDownChange != null)
                onGameCountDownChange(timeCountDown);
            if( CheckGameOverCondition_Time() )
                EndGame();
        }

        private void SetReadyCountDown(int newValue)
        {
            readyTimeCountDown = newValue;
            if(onReadyCountDownChange != null)
                onReadyCountDownChange(newValue);
        }

        private void SetHealth(int newHealth)
        {
            if ( gameIsPlaying )
            {
                health = newHealth;
                if(onHealthChange != null)
                    onHealthChange(newHealth);
                if( CheckGameOverCondition_Health() )
                    EndGame();
            }
        }

        private void SetScore(int newScore){
            score = newScore;
            if(onScoreChange != null)
                onScoreChange(score);
            if( CheckGameOverCondition_Score() )
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

        /**
        * Count down to start game.
        * When it down to zero, the game count down start.
        */
        private IEnumerator RunReadyCountDown(int startValue)
        {
            while(startValue >= -1)
            {
                yield return new WaitForSeconds(1);
                SetReadyCountDown(startValue);
                startValue--;
            }
            
            SetReadyCountDown(startValue);
            if( gameHasStarted != null)
                gameHasStarted();

            StartCoroutine( RunCountDown() );
        }
    
        //TODO: Implement damage
        private void RegenerateHealth()
        {
            if ( gameIsPlaying && health < 100 && health > 0)
            {
                SetHealth(health + HEALTH_REGENERATE_RATIO);
            }
        }

    #endregion

    #region GAME OVER CONDITIONS
        private bool CheckGameOverCondition_Score() => score >= GAMEOVER_WIN_SCORE || score <= GAMEOVER_LOSE_SCORE;
        private bool CheckGameOverCondition_Time() => timeCountDown <= GAMEOVER_LOSE_TIME;
        private bool CheckGameOverCondition_Health() => health <= GAMEOVER_LOSE_HEALTH;
        
    #endregion
}

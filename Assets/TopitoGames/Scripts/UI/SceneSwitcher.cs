using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TopitoGames
{
    public class SceneSwitcher : MonoBehaviour
    {
        [SerializeField] SceneDirectory sceneDirectory;
        static int currentStage = -1;

        public void SwitchToMainMenu()
        {
            SceneManager.LoadScene( sceneDirectory.GetMainMenu() );
        }
        public void SwitchTo(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
            currentStage = sceneDirectory.GetLevel(sceneName);
        }

        public void SwitchToFirstStage()
        {
            SceneManager.LoadScene( sceneDirectory.GetNextLevel(-1) );
            currentStage = 0;
        }

        public void SwitchToNextStage()
        {
            if(currentStage == -1) currentStage++;

            SceneManager.LoadScene(sceneDirectory.GetNextLevel(currentStage));
            currentStage++;
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}

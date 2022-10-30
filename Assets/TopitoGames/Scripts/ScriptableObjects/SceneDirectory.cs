using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TopitoGames
{
    
    [CreateAssetMenu(fileName = "SceneDirectory", menuName = "2022_Rollaball/SceneDirectory", order = 0)]
    public class SceneDirectory : ScriptableObject {
        
        [SerializeField] string MainMenu;
        [SerializeField] GameStage[] gameStages;

        [System.Serializable]
        struct GameStage {
            [SerializeField] string levelName;
            [SerializeField] string scene;
            public string GetScene() => scene;
            public string LevelName { get { return levelName; } }
        }

        public string GetMainMenu() => MainMenu;
        public string GetNextLevel(int currentLevel) =>  gameStages[currentLevel +1].GetScene();

        /// <summary>
        /// Get de stage level setted in the SceneDirectory.
        /// It has not relation with the scenes in the building setting.
        /// </summary>
        /// <param name="stageName"></param>
        /// <returns>The stage level or -1 if the stage name is wrong</returns>
        public int GetLevel(string stageName){
            for (int level = 0; level < gameStages.Length; level++)
            {
                if(stageName == gameStages[level].LevelName)
                    return level;
            }
            return -1;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopitoGames
{
    public class PowerUpPoolController : MonoBehaviour
    {
        [SerializeField] Transform[] powerUpTransforms;

        #region UNITY METHODS
            private void OnEnable() {
                GameManager.getReadyToStart += Init;
            }
            private void OnDisable() {
                GameManager.getReadyToStart -= Init;
            }

        #endregion

        void Init()
        {
            Debug.Log(powerUpTransforms);
            if( powerUpTransforms.Length < 1) powerUpTransforms = gameObject.GetComponentsInChildren<Transform>() ;
            foreach (Transform powerUp in powerUpTransforms)
            {
                if (!powerUp.gameObject.activeInHierarchy) powerUp.gameObject.SetActive(true);
            }
        }
    }
    
}

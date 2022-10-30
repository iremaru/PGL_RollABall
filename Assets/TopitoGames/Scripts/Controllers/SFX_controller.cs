using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_controller : MonoBehaviour
{
    
    #region SCENE REFERENCES
        [SerializeField] AudioSource mainAudioSource;
    #endregion

    #region UNITY METHODS
        private void OnEnable() {
            GameManager.onSFXsound += PlaySFX;
        }

        private void OnDisable() {
            GameManager.onSFXsound -= PlaySFX;
        }
    #endregion

    #region PUBLIC METHODS
        void PlaySFX(AudioClip clip){
            mainAudioSource.PlayOneShot(clip);
        }

        void PlayPlaylist(AudioClip[] clips)
        {
            
        }
    #endregion

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBehaviour : MonoBehaviour
{
    [SerializeField] int value = 1;
    [SerializeField] AudioClip pickUpSound;

    #region UNITY METHODS
        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Player"))
            {
                GameManager.Instance.IncrementScore(value);
                GameManager.Instance.PlaySFXSound(pickUpSound);
            }
        }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopitoGames
{
    public class deathZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other) {
            if(other.gameObject.CompareTag("Player"))
                GameManager.Instance.InstantDeath();
        }
    }
    
}

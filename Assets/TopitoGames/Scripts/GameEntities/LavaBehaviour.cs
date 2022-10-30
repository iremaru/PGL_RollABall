using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBehaviour : MonoBehaviour
{
    [SerializeField] int damagePerSecond = 1;
    bool playerIsOnLava = false;

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player") && !playerIsOnLava)
        {
            playerIsOnLava = true;
            StartCoroutine( GiveDamagePerSecond() );
        }
    }

    private void OnCollisionExit(Collision other) {
        
        if (other.gameObject.CompareTag("Player") && playerIsOnLava)
        {
            playerIsOnLava = false;
            StopDamagePerSecond();
        }
    }

    IEnumerator GiveDamagePerSecond()
    {
        while ( playerIsOnLava )
        {
            yield return new WaitForSeconds(1f);
            GameManager.Instance.DecrementHealth( damagePerSecond);
        }
    }

    void StopDamagePerSecond()
    {
        StopCoroutine( "GiveDamagePerSecond" );
    }
}

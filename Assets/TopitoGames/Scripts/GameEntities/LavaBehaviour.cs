using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBehaviour : MonoBehaviour
{
    [SerializeField] int damageOnEnter = 1;
    [SerializeField] int damagePerSecond = 1;
    bool playerIsOnLava = false;

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player") && !playerIsOnLava)
        {
            Debug.Log("Daño por entrar " + other.gameObject.tag);
            GameManager.DecrementHealth(damageOnEnter);
            playerIsOnLava = true;
            StartCoroutine( "GiveDamagePerSecond" );
        }
    }

    private void OnCollisionExit(Collision other) {
        playerIsOnLava = false;
        StopDamagePerSecond();
    }

    IEnumerator GiveDamagePerSecond()
    {
        while ( playerIsOnLava )
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("Daño por mantenerse");
            //GameManager.DecrementHealth( (int)(damagePerSecond * Time.deltaTime));
            GameManager.DecrementHealth( damagePerSecond);
        }
    }

    void StopDamagePerSecond()
    {
        StopCoroutine( "GiveDamagePerSecond" );
    }
}

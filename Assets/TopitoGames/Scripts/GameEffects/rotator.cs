using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotator : MonoBehaviour
{
    [SerializeField][Range(0,1)] float changeSizeSpeed = 0.1f;
    [SerializeField][Range(0,10)] float maxScale = 2;
    [SerializeField][Range(0,10)] float minScale = 0.5f;
    bool isGettingBigger = false;
    
    void Update()
    {
        transform.Rotate(new Vector3(45, 45, 45) * Time.deltaTime);
        ChangeScale();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) 
        {
            gameObject.SetActive(false);
        }
    }

    #region PRIVATE METHODS
        
        private void ChangeScale()
        {
            Vector3 lastScale = transform.localScale;
            Vector3 lastPosition = transform.position;

            float changeSpeed = changeSizeSpeed * Time.deltaTime;

            isGettingBigger = !isGettingBigger && lastScale.x <= minScale ||
                isGettingBigger && lastScale.x <= maxScale ? true : false;

            if(isGettingBigger)
            {
                transform.localScale = new Vector3(lastScale.x + changeSpeed, 
                    lastScale.y + changeSpeed, 
                    lastScale.z + changeSpeed);
                transform.position = new Vector3(lastPosition.x, lastPosition.y + changeSpeed , lastPosition.z);
            } else {
                transform.localScale = new Vector3(lastScale.x - changeSpeed, 
                    lastScale.y - changeSpeed, 
                    lastScale.z - changeSpeed);
                transform.position = new Vector3(lastPosition.x, lastPosition.y - changeSpeed , lastPosition.z);
            }
        }
    #endregion
}

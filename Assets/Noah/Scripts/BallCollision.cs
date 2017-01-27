using UnityEngine;

namespace Noah.Scripts
{
    public class BallCollision : MonoBehaviour {
        private Health _health;

        void OnTriggerEnter(Collider other){
            if (other.gameObject.CompareTag("Ball")){
                Debug.Log ("Withdraw Health");
            }

        }


    }
}

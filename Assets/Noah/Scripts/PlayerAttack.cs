using System.Collections.Generic;
using UnityEngine;

namespace Noah.Scripts
{
    public class PlayerAttack : MonoBehaviour {

        /* Zelfde knop gebruiken voor attack hierbij is het X op de PS2 controller > Zodra dit wordt ingeklikt gaat er een tijd lopen bijvoorbeeld 5 seconden >
	 Als die binnen deze tijd blijft dan komen er op een volgende attacks > valt die buiten deze 5 seconden > En wordt er daarna weer op X geklikt > Dan gaat ie waar naar
	 de eerste Attack animatie.
	 if(input.getkeydown(keycode.g)){
		curanim++;
		if(curanim >= 3){
		curanim = 0;
		}
		}
	*/
        private float _time;

        private float _attackCooldown;

        private int _curanim = 0;
        private int _i;

        private Animator _animator;
        private Rigidbody _rigidbody;
        private Health _health;

        [SerializeField] private BoxCollider _swordCollider;
        [SerializeField] private LayerMask _swordMask;

        private HashSet<Collider> _collidersHitBySword;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
            _collidersHitBySword = new HashSet<Collider>();
            _health = GetComponent<Health>();
            _i = 0;
        }

        private void Update(){
            if (Input.GetKeyDown(KeyCode.Space) && _health.MP >= 3 && _attackCooldown <= Time.time) {
                Attack ();
                _time = Time.time + 5;
                _attackCooldown = Time.time + 1;
            }
            if (_time <= Time.time || _i > 3) {
                _i = 0;
                Debug.Log ("reset");
            }

            if (!_checkForDamage) return;

            var col = Physics.OverlapBox(_swordCollider.transform.position, _swordCollider.size/2, Quaternion.identity, _swordMask);
            if (col.Length <= 0) return;
            foreach (var enemy in col)
            {
                if (_collidersHitBySword.Contains(enemy)) continue;
                enemy.GetComponent<Wakka>().RemoveHealth(2f);
                Debug.Log("Attacked enemy");
                _collidersHitBySword.Add(enemy);
            }
        }

        private void Attack(){
            _health.RemoveMana(3f);
            _animator.SetTrigger("Attack");
            _rigidbody.velocity = CalculateLaunchVelocity(transform.position + transform.forward);
            _i++;
        }

        private bool _checkForDamage = false;
        public void CheckForDamage()
        {
            _checkForDamage = !_checkForDamage;
            if (_checkForDamage) _collidersHitBySword.Clear();
        }

        private Vector3 CalculateLaunchVelocity(Vector3 toPosition)
        {
            var displacementY  = toPosition.y - transform.position.y;
            var displacementXZ = new Vector3(toPosition.x - transform.position.x,
                0,
                toPosition.z - transform.position.z);

            const float gravity = -9.8f;
            const float h = 0.1f;
            var velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
            var velocityXZ = displacementXZ / (Mathf.Sqrt(-2*h/gravity) + Mathf.Sqrt(2*(displacementY-h)/gravity));

            return velocityXZ + velocityY;
        }
    }
}

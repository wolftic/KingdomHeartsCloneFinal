using UnityEngine;

namespace Noah.Scripts
{
    public class PlayerMovement : MonoBehaviour {
        private Rigidbody _rigidbody;
        [SerializeField]
        private float _movSpeed = 10.0f;
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            var hor = Input.GetAxis ("Horizontal");
            var ver = Input.GetAxis ("Vertical");
            var direction = new Vector3(hor, 0f, ver);
            direction = Camera.main.transform.TransformDirection(direction);
            direction.y = 0;

            _rigidbody.MovePosition(transform.position + direction * _movSpeed * Time.deltaTime);
            if (direction == Vector3.zero)
            {
                _animator.SetBool("Run", false);
            }
            else
            {
                _rigidbody.rotation = Quaternion.Lerp(_rigidbody.rotation, Quaternion.LookRotation(direction, transform.up), 5.0f * Time.deltaTime);
                _animator.SetBool("Run", true);
            }
        }
    }
}

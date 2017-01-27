using UnityEngine;

namespace Noah.Scripts
{
    public class Health : MonoBehaviour {

        /*[SerializeField]
        private float _health = 100.0f;

        public float PlayerHealth{
            get {
                return _health;
            }
            set	{
                _health = value;
            }
        }*/

        [SerializeField]
        private float _startHp = 10, _startMp = 10;
        private float _hp, _mp;

        [SerializeField] private UnityEngine.UI.Image _hpbar;
        [SerializeField] private UnityEngine.UI.Image _mpbar;

        protected virtual void Start() {
            _hp = _startHp;
            _mp = _startMp;
        }

        public float MP
        {
            get { return _mp; }
        }

        public virtual void RemoveHealth (float hp){
            _hp -= hp;
        }
        public virtual void RemoveMana (float mp){
            _mp -= mp;
        }

        private void Update()
        {
            _hpbar.fillAmount = (_hp / _startHp);
            _mpbar.fillAmount = (_mp / _startMp);

            _mp += Time.deltaTime;
            _mp = Mathf.Clamp(_mp, 0, _startMp);

            if (_hp <= 0)
            {
                gameObject.SetActive(false);
            }

            //Hoort hier niet maar is voor testen
            if (Input.GetKeyDown(KeyCode.R))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.transform.CompareTag("Projectile")) return;
            RemoveHealth(2f);
            Destroy(other.gameObject);
        }
    }
}

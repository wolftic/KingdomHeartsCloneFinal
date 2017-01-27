using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakkaAttack : MonoBehaviour {
	private Wakka _wakka;

    [SerializeField] private GameObject _wakkaHand, _ballPrefab;

	[SerializeField]
	private Animator _animator;

    private void Start () {
		_wakka = GetComponent<Wakka> ();
		_animator = GetComponent<Animator> ();
	}
	
	public void PerformAttack () {
	    var player = GameObject.FindGameObjectWithTag("Player");
	    var position = player.transform.position;
	    transform.LookAt(position, transform.up);
	    //Play animation
        ThrowBall();
	}

    public void ThrowBall()
    {
        var position     = _wakkaHand.transform.position;
        var player = GameObject.FindGameObjectWithTag("Player");

        var ball = Instantiate(_ballPrefab, position, Quaternion.identity) as GameObject;
        var rb = ball.GetComponent<Rigidbody>();
        Destroy(ball, 2f);

        var velocity = CalculateLaunchVelocity(player);
        rb.velocity = velocity;
    }

    private Vector3 CalculateLaunchVelocity(GameObject player)
    {
        var displacementY  = player.transform.position.y - _wakkaHand.transform.position.y;
        var displacementXZ = new Vector3(player.transform.position.x - _wakkaHand.transform.position.x,
            0,
            player.transform.position.z - _wakkaHand.transform.position.z);

        const float gravity = -9.8f;
        const float h = 2;
        var velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        var velocityXZ = displacementXZ / (Mathf.Sqrt(-2*h/gravity) + Mathf.Sqrt(2*(displacementY-h)/gravity));

        return velocityXZ + velocityY;
    }
}

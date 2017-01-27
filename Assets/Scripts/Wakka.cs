using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wakka : Enemy {
	[SerializeField]
	private float _delayTime = 2f;

	private float _timeUntilNextMove;

	private WakkaAttack _attack;
	private WakkaMovement _movement;

	protected override void Start() {
		base.Start ();
		_attack = GetComponent<WakkaAttack> ();
		_movement = GetComponent<WakkaMovement> ();
	}

	public override void RemoveHealth (float hp)
	{
	    Hp -= hp;
	}

	public override void RemoveMana (float mp)
	{
	    Mp -= mp;
	}

    private void Update()
    {
        GameObject.Find("WakkaHpText").GetComponent<UnityEngine.UI.Text>().text = "Wakka hp: " + Hp;
        if (Hp == 0)
        {
            GameObject.Find("WakkaHpText").GetComponent<UnityEngine.UI.Text>().text = "You win, press R to restart!";
            gameObject.SetActive(false);
        }

        if (!(_timeUntilNextMove <= Time.time)) return;
        NextAction ();
        _timeUntilNextMove = Time.time + _delayTime;
    }

	private void NextAction() {
		var r = Mathf.RoundToInt(Random.Range (0, 2));
	    if (_movement.IsWalking) return;

	    if (r == 0) {
	        _attack.PerformAttack ();
	    } else {
	        _movement.WanderToNextPoint ();
	    }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	[SerializeField]
	protected float StartHp = 10, StartMp = 10;
	protected float Hp, Mp;
	public float MovementSpeed;

	protected virtual void Start() {
		Hp = StartHp;
		Mp = StartMp;
	}

    public virtual void RemoveHealth (float hp){
        Hp -= hp;
    }
	public virtual void RemoveMana (float mp){
	    Mp -= mp;
	}
}
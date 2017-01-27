using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnTargetManager : MonoBehaviour {
    public static LockOnTargetManager current;
    public SpriteRenderer spriteRenderer;

	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        current = this;
	}
}

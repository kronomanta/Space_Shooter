﻿using UnityEngine;

public class Mover : MonoBehaviour {
    public float Speed;
	void Start () {
        GetComponent<Rigidbody>().velocity = transform.forward * Speed;
	}
	
}
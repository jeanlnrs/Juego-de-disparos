﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float smoothing = 5f;
	Vector3 offset;

	void Start(){
		offset = transform.position - target.position;
	}

	void FixedUpdate()
	{
		// Interpolates between the vectors a and b by the interpolant t.
		Vector3 targetCamPos = target.position + offset; 
		transform.position =  Vector3.Lerp( transform.position, targetCamPos, smoothing * Time.deltaTime );
	}




}

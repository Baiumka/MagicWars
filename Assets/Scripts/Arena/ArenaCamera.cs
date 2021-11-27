using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaCamera : MonoBehaviour
{

    Quaternion startRotation;

	float sensitivity =0.9f; // чувствительность мышки
	private float X, Y;

	void Update()
	{
		if (Input.GetMouseButton(0))
		{
			X = transform.localEulerAngles.y - Input.GetAxis("Mouse X") * sensitivity;
			Y += Input.GetAxis("Mouse Y") * sensitivity;
			Y = Mathf.Clamp(Y, -90, 90);
			transform.localEulerAngles = new Vector3(Y, X, 0);
		}
		else
		{
			if(transform.rotation != startRotation)
				transform.rotation = Quaternion.Lerp(transform.rotation, startRotation, Time.deltaTime*10f);
		}
	}

	private void Start()
    {
        startRotation = transform.rotation;
    }
}

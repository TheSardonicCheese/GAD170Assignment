using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	CharacterController controller;

	public float speed = 6.0f;
	public float jumpSpeed = 8.0f;
	public float gravity =20.0f;

	Vector3 moveDirection = Vector3.zero;


	void Start () 
	{
		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(controller.isGrounded)
		{
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection *= speed;

			if(Input.GetButton("Jump"))
			{
				moveDirection.y = jumpSpeed;
			}
		}

		moveDirection.y -= gravity * Time.deltaTime;

		controller.Move(moveDirection * Time.deltaTime);

		Vector3 lookDir = new Vector3(moveDirection.x, 0, moveDirection.z);


		if (moveDirection.x + moveDirection.z != 0)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), 0.15f);
		}

	}
}

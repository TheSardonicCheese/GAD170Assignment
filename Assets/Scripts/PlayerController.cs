using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	CharacterController controller;

	public float speed = 6.0f;
	public float jumpSpeed = 8.0f;
	public float gravity =20.0f;

	Vector3 MoveDirection = Vector3.zero;


	void Start () 
	{
		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(controller.isGrounded)
		{
			MoveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			MoveDirection *= speed;

			if(Input.GetButton("Jump"))
			{
				MoveDirection.y = jumpSpeed;
			}
		}

		MoveDirection.y -= gravity * Time.deltaTime;

		controller.Move(MoveDirection * Time.deltaTime);

		Vector3 lookDir = new Vector3(MoveDirection.x, 0, MoveDirection.z);

		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), 0.15f);
	}
}

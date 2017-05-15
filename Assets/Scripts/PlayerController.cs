using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]

public class PlayerController : MonoBehaviour {


	//Handling
	public float rotationSpeed = 450;
	public float runSpeed = 5;
	public float walkSpeed = 8;

	//System
	private Quaternion targetRotation;

	//Components
	private CharacterController controller;
	private Camera cam;
	public Gun gun;


	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController> ();
		cam = Camera.main;
	}


	void ControlMouse(){

		Vector3 mousePos = Input.mousePosition;
		mousePos = cam.ScreenToWorldPoint (new Vector3 (mousePos.x, mousePos.y, cam.transform.position.y - transform.position.y));
		targetRotation = Quaternion.LookRotation (mousePos - new Vector3(transform.position.x, 0, transform.position.z));
		transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle (transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);
		 
		Vector3 input = new Vector3 (Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

		Vector3 motion = input;


		if(Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1){
			input = input * 0.7f;
		}

		if(Input.GetButtonDown("Run")){
			motion = input * runSpeed;
			print ("Running");
		}else{
			motion = input * walkSpeed;
			print ("Walking");
		}

		motion += Vector3.up * -8f;

		controller.Move (motion * Time.deltaTime);
	}

	void ControlWASD(){
		Vector3 input = new Vector3 (Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

		if(input != Vector3.zero){
			targetRotation = Quaternion.LookRotation (input);
			transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle (transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);
		}

		Vector3 motion = input;


		if(Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1){
			input = input * 0.7f;
		}

		if(Input.GetButtonDown("Run")){
			motion = input * runSpeed;
			print ("Running");
		}else{
			motion = input * walkSpeed;
			print ("Walking");
		}

		motion += Vector3.up * -8f;

		controller.Move (motion * Time.deltaTime);
	}

	// Update is called once per frame
	void Update () {
		ControlMouse ();
		//ControlWASD ();


		if (Input.GetButtonDown ("Shoot")) {
			gun.Shoot ();
		} else if (Input.GetButton ("Shoot")) {
			gun.ShootContinuous ();
		}

	}
}

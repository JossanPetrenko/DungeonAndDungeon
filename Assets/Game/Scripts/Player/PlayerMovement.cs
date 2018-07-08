using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof (NavMeshAgent))]
public class PlayerMovement : MonoBehaviour {

	NavMeshAgent agent;

	Animator anim;

	public float TurnSpeed;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		anim = GetComponentInChildren<Animator> ();
		agent.updateRotation = false;
	}

	// Update is called once per frame
	void Update () {

		UpdateVelocity ();

	}

	void UpdateVelocity () {

		var velocity = agent.velocity.magnitude / agent.speed;
		if(! Mathf.Approximately( velocity, 0.0f)  ) { 
			anim.SetFloat ("movementeSpeed", velocity, .1f, Time.deltaTime);
		}else{
			anim.SetFloat ("movementeSpeed", 0, .1f, Time.deltaTime);

		}

		UpdateFacingAngle (velocity);

		if (Input.GetMouseButton (0)) {
			RaycastHit hit;
			Debug.Log (agent.destination);

			if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 100, LayerMask.GetMask ("Terrain"))) {
				agent.destination = hit.point;
				if(anim.GetFloat("movementeSpeed") <= 0){
					anim.SetFloat ("movementeSpeed", 1f, .1f, Time.deltaTime);
				}
			}
		}

	

	}

	private void UpdateFacingAngle (float velocity) {
	
		if( Mathf.Approximately( velocity, 0.0f)  ) return;
		var eulerAngle = agent.velocity.normalized;
		//eulerAngle.y = 0;

		transform.rotation = Quaternion.LookRotation(eulerAngle);
	}

}
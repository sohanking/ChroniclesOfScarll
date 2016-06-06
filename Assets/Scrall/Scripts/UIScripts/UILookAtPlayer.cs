using UnityEngine;
using System.Collections;

public class UILookAtPlayer : MonoBehaviour {

	GameObject playa;

	LineRenderer lr;
	[SerializeField]
	GameObject target;
	// Use this for initialization
	void Start () {
		playa = GameObject.FindGameObjectWithTag ("MainCamera");
		lr = this.gameObject.GetComponent<LineRenderer> ();
		lr.useWorldSpace = true;
		lr.SetVertexCount (2);
		lr.SetWidth (0.01f, 0.01f);

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		this.transform.LookAt (2*transform.position-playa.transform.position,playa.transform.up);
		Vector3 canvasAncher = new Vector3 (this.transform.position.x - this.transform.localScale.x / 2, this.transform.position.y - this.transform.localScale.y / 2, this.transform.position.z);
		Vector3 targetAncher = target.GetComponent<MeshRenderer> ().bounds.ClosestPoint(canvasAncher);
		Vector3[] points = { canvasAncher, targetAncher };
		lr.SetPositions(points);


	}
}

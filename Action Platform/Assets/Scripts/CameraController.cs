using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    
	public Transform target;
	public float cameraSpeed;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position = new Vector3(target.position.x,target.position.y, transform.position.z);
		transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), cameraSpeed * Time.deltaTime);
	}
}
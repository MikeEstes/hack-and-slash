using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {
	private const string DEFAULT_DROP_ZONE_NAME = "dz_Default";
	public GameObject destination;

	// Use this for initialization
	void Start () {
		if( destination == null )
			destination = GameObject.Find( DEFAULT_DROP_ZONE_NAME );
			
	}
	
	public void OnTriggerEnter( Collider other ) {
		if( other.transform.CompareTag("Player") )
			other.transform.position = destination.transform.position;
		
	}
}

using UnityEngine;
using System.Collections;

public class Healthbar : MonoBehaviour {
	private UIProgressBar _health;
	private bool _grow = false;
	private float _val = .01f;

	void Start () {
		_health = UIProgressBar.create( "progressBar.png", "progressBarBorder.png", 5, 3, 0, 0);
		_health.positionFromTopLeft( .01f, .01f);
		_health.resizeTextureOnChange = true;
		_health.value = .9f;
	}
	
	void Update () {
		if( !_grow && _health.value == 0 )
			_grow = true;
		if( _grow && _health.value == 1 )
			_grow = false;
	
		if( _grow )
			_health.value += _val;
		else
			_health.value -= _val;
	}
}

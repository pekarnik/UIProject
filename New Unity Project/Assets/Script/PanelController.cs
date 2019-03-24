using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PanelController:MonoBehaviour
{
	private GameObject _swapper;
	private float _offset;
	private void Start()
	{
		_swapper = GameObject.FindGameObjectWithTag("Swapper");
		_offset = transform.position.y - _swapper.transform.position.y;
	}
	private void Update()
	{
		transform.position=Vector3.Lerp(transform.position,new Vector3(0, _swapper.transform.position.y + _offset),1);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PanelController:MonoBehaviour
{
	[SerializeField]
	private Transform _startPos;
	private SwapperController _swapper;
	private float _offset;
	private Vector3 nextpos=new Vector3();
	private bool isTouched;

	private void Start()
	{
		UIMonitor.OnResolutionChange += UIMonitor_OnResolutionChange;
		UIMonitor.OnOrientationChange += UIMonitor_OnOrientationChange;
		_swapper = FindObjectOfType<SwapperController>();
		SetPosition();
	}

	private void UIMonitor_OnOrientationChange(DeviceOrientation obj)
	{
		SetPosition();
	}

	private void UIMonitor_OnResolutionChange(Vector2 obj)
	{
		SetPosition();
	}

	//private void OnRectTransformDimensionsChange()
	//{
	//	_offset = _swapper.transform.position.y - _swapper.GetComponent<CircleCollider2D>().radius;
	//}
	private void Update()
	{
		if(_swapper.isTouched)
		{
			nextpos.Set(_swapper.transform.position.x, _swapper.transform.position.y+_offset,_swapper.transform.position.z);
			transform.position = nextpos;
		}
		else
		{
			if(_swapper.whereIsIt==Position.Upper)
			{
				nextpos = Vector3.Lerp(nextpos, _swapper.upper, 0.1f);
				transform.position = nextpos;
			}
			else if (_swapper.whereIsIt == Position.Middle)
			{
				nextpos = Vector3.Lerp(nextpos, _swapper.middle, 0.1f);
				transform.position = nextpos;
			}
			else if (_swapper.whereIsIt == Position.Lower)
			{
				nextpos = Vector3.Lerp(nextpos, _swapper.lower, 0.1f);
				transform.position = nextpos;
			}
		}
	}
	//private void OnMouseDrag()
	//{
	//	if(_swapper.whereIsIt==Position.Upper)
	//	{
	//		isTouched = true;
	//		if (Input.touchCount == 1)
	//		{
	//			Touch touch = Input.GetTouch(0);
	//			Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
	//			touchPosition.z = 0f;
	//			touchPosition.x = 0f;
	//			if (transform.position.y <= _swapper.transform.position.y+_offset)
	//			{
	//				transform.position = touchPosition;
	//			}
	//			else
	//			{
	//				transform.position = new Vector3(0, _swapper.transform.position.y + _offset,0);
	//			}
	//		}
	//	}
	//}
	private void SetPosition()
	{
		transform.position = _startPos.position;
		_offset = transform.position.y - _swapper.transform.position.y - _swapper.GetComponent<CircleCollider2D>().radius;
	}
	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PanelController : MonoBehaviour
{
	[SerializeField]
	private Transform _lowerPosition;
	[SerializeField]
	private Transform _middlePosition;
	[SerializeField]
	private Transform _upperPosition;
	private bool _isTouched=false;
	private float _speed=0.1f;
	private void Start()
	{
		//transform.position = _lowerPosition.position;
	}
	private void OnMouseDrag()
	{
		_isTouched = true;
		if (Input.touchCount==1)
		{
			Touch touch = Input.GetTouch(0);
			Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
			touchPosition.z = 0f;
			touchPosition.x = 0f;
			if (transform.position.y >= _lowerPosition.position.y)
			{
				transform.position = touchPosition;
			}
			else
			{
				transform.position = new Vector3(0,_lowerPosition.position.y,0);
			}
		}
	}

	private void OnMouseUp()
	{
		_isTouched = false;
		
	}
	private void Update()
	{
		if (!_isTouched)
		{
			
			if (transform.position.y < _lowerPosition.position.y ||
				Vector2.Distance(transform.position, _lowerPosition.position)
				< Vector2.Distance(_middlePosition.position, transform.position))
			{
				transform.position = Vector2.Lerp(transform.position, _lowerPosition.position, _speed);
			}
			else if (Vector2.Distance(transform.position, _middlePosition.position)
				< Vector2.Distance(_lowerPosition.position, transform.position) &&
				Vector2.Distance(transform.position, _middlePosition.position)
				< Vector2.Distance(_upperPosition.position, transform.position))
			{
				transform.position = Vector2.Lerp(transform.position, _middlePosition.position, _speed);
			}
			else if (transform.position.y > _upperPosition.position.y ||
				Vector2.Distance(transform.position, _upperPosition.position)
				< Vector2.Distance(_middlePosition.position, transform.position))
			{
				transform.position = Vector2.Lerp(transform.position, _upperPosition.position, _speed);
			}
		}
	}
}

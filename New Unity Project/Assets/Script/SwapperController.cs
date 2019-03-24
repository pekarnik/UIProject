using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class SwapperController : MonoBehaviour
{
	[SerializeField]
	private Transform _lowerPosition;
	[SerializeField]
	private Transform _middlePosition;
	[SerializeField]
	private Transform _upperPosition;
	[HideInInspector]
	public bool isTouched=false;
	private float _speed=0.1f;
	[HideInInspector]
	public Position whereIsIt=Position.Lower;
	private Vector3 _lower;
	private Vector3 _middle;
	private Vector3 _upper;
	private void Start()
	{
		_lower = new Vector3(0, _lowerPosition.position.y, 0);
		_middle = new Vector3(0, _middlePosition.position.y, 0);
		_upper = new Vector3(0, _upperPosition.position.y, 0);
	}
	private void OnMouseDrag()
	{
		isTouched = true;
		if (Input.touchCount==1)
		{
			Touch touch = Input.GetTouch(0);
			Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
			touchPosition.z = 0f;
			touchPosition.x = 0f;
			if (transform.position.y >= _lower.y)
			{
				transform.position = touchPosition;
			}
			else
			{
				transform.position = _lower;
			}
		}
	}

	private void OnMouseUp()
	{
		isTouched = false;
		
	}
	private void Update()
	{
		if (!isTouched)
		{
			
			if (transform.position.y < _lower.y ||
				Vector2.Distance(transform.position, _lower)
				< Vector2.Distance(_middle, transform.position))
			{
				transform.position = Vector2.Lerp(transform.position, _lower, _speed);
				whereIsIt = Position.Lower;
			}
			else if (Vector2.Distance(transform.position, _middle)
				< Vector2.Distance(_lowerPosition.position, transform.position) &&
				Vector2.Distance(transform.position, _middle)
				< Vector2.Distance(_upper, transform.position))
			{
				transform.position = Vector2.Lerp(transform.position, _middle, _speed);
				whereIsIt = Position.Middle;
			}
			else if (transform.position.y > _upper.y ||
				Vector2.Distance(transform.position, _upper)
				< Vector2.Distance(_middle, transform.position))
			{
				transform.position = Vector2.Lerp(transform.position, _upper, _speed);
				whereIsIt = Position.Upper;
			}
		}
	}
}

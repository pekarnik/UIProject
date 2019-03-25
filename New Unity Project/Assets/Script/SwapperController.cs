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
	public Vector3 lower;
	public Vector3 middle;
	public Vector3 upper;
	private float _delta;
	private float _deltaPos;
	private uint count = 0;
	private Vector3 _nextPos;
	private Swap _swap;
	private void Start()
	{
		_nextPos = new Vector3();
		lower = new Vector3(_lowerPosition.position.x,_lowerPosition.position.y,_lowerPosition.position.z);
		middle = new Vector3(_middlePosition.position.x, _middlePosition.position.y, _middlePosition.position.z);
		upper = new Vector3(_upperPosition.position.x, _upperPosition.position.y, _upperPosition.position.z);
		UIMonitor.OnResolutionChange += UIMonitor_OnResolutionChange;
		UIMonitor.OnOrientationChange += UIMonitor_OnOrientationChange;
		SetNewPos();
	}

	private void UIMonitor_OnResolutionChange(Vector2 obj)
	{
		SetNewPos();
	}

	private void UIMonitor_OnOrientationChange(DeviceOrientation obj)
	{
		SetNewPos();
	}
	
	public void OnMouseDrag()
	{
		isTouched = true;
		if (Input.touchCount==1)
		{
			Touch touch = Input.GetTouch(0);
			Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
			touchPosition.z = 0f;
			touchPosition.x = 0f;
			float position = transform.position.y;
			transform.position = touchPosition;
			_deltaPos = transform.position.y - position;
			if(_deltaPos>0)
			{
				_swap = Swap.Up;
			}
			else if(_deltaPos<0)
			{
				_swap = Swap.Down;
			}
		}
		if (count > 0)
		{
			StopAllCoroutines();
			count = 0;
		}
	}

	public void OnMouseUp()
	{
		isTouched = false;
		if (count > 0)
		{
			StopAllCoroutines();
			count = 0;
			StartCoroutine(AfterSwap());
		}
		else
		{
			StartCoroutine(AfterSwap());
		}		
	}
	IEnumerator AfterSwap()
	{
		count++;
		if(Screen.width<Screen.height)
		{
			while (!isTouched)
			{
				CorForPortrait();
				yield return new WaitForFixedUpdate();
			}
		}
		else
		{
			while (!isTouched)
			{
				CorForLandScape();
				yield return new WaitForFixedUpdate();
			}
		}
		
	}

	

	private void CorForPortrait()
	{
		//if (transform.position.y < _lower.y ||
		//		Vector2.Distance(transform.position, _lower)
		//		< Vector2.Distance(_middle, transform.position))
		//{
		//		transform.position = Vector3.Lerp(transform.position, _lower, _speed);
		//		whereIsIt = Position.Lower;
		//}
		//else if (Vector2.Distance(transform.position, _middle)
		//	< Vector2.Distance(_lowerPosition.position, transform.position) &&
		//	Vector2.Distance(transform.position, _middle)
		//	< Vector2.Distance(_upper, transform.position))
		//{
		//		transform.position = Vector3.Lerp(transform.position, _middle, _speed);
		//		whereIsIt = Position.Middle;
		//}
		//else if (transform.position.y > _upper.y ||
		//   Vector2.Distance(transform.position, _upper)
		//   < Vector2.Distance(_middle, transform.position))
		//{
		//		transform.position = Vector3.Lerp(transform.position, _upper, _speed);
		//		whereIsIt = Position.Upper;
		//}
		if(transform.position.y<lower.y)
		{
			_nextPos.Set(0, lower.y + 0.1f, 0);
			transform.position = Vector3.Lerp(transform.position, _nextPos, _speed);
			whereIsIt = Position.Lower;
		}
		else if(transform.position.y>upper.y)
		{
			_nextPos.Set(0, upper.y - 0.1f, 0);
			transform.position = Vector3.Lerp(transform.position, _nextPos, _speed);
			whereIsIt = Position.Upper;
		}
		else if(transform.position.y<middle.y&&_swap==Swap.Up)
		{
			_nextPos.Set(0, middle.y, 0);
			transform.position = Vector3.Lerp(transform.position, _nextPos, _speed);
			whereIsIt = Position.Middle;
		}
		else if (transform.position.y < middle.y && _swap == Swap.Down)
		{
			_nextPos.Set(0, lower.y, 0);
			transform.position = Vector3.Lerp(transform.position, _nextPos, _speed);
			whereIsIt = Position.Lower;
		}
		else if (transform.position.y > middle.y && _swap == Swap.Up)
		{
			_nextPos.Set(0, upper.y - 0.1f, 0);
			transform.position = Vector3.Lerp(transform.position, _nextPos, _speed);
			whereIsIt = Position.Upper;
		}
		else if (transform.position.y > middle.y && _swap == Swap.Down)
		{
			_nextPos.Set(0, middle.y, 0);
			transform.position = Vector3.Lerp(transform.position, _nextPos, _speed);
			whereIsIt = Position.Middle;
		}
	}
	private void CorForLandScape()
	{
		if (transform.position.y < lower.y)
		{
			_nextPos.Set(0, lower.y + 0.1f, 0);
			transform.position = Vector3.Lerp(transform.position, _nextPos, _speed);
			whereIsIt = Position.Lower;
		}
		else if (transform.position.y > upper.y)
		{
			_nextPos.Set(0, upper.y - 0.1f, 0);
			transform.position = Vector3.Lerp(transform.position, _nextPos, _speed);
			whereIsIt = Position.Upper;
		}
		else if (_swap == Swap.Up)
		{
			_nextPos.Set(0, upper.y - 0.1f, 0);
			transform.position = Vector3.Lerp(transform.position, _nextPos, _speed);
			whereIsIt = Position.Upper;
		}
		else if(_swap == Swap.Down)
		{
			_nextPos.Set(0, lower.y + 0.1f, 0);
			transform.position = Vector3.Lerp(transform.position, _nextPos, _speed);
			whereIsIt = Position.Lower;
		}
	}
	private void SetNewPos()
	{
		lower= new Vector3(0, _lowerPosition.position.y, 0);
		transform.position = lower;
		lower = _lowerPosition.position;
		middle = _middlePosition.position;
		upper = _upperPosition.position;
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class TouchHandler : BaseRaycaster, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
	Vector2 startScreenPos = Vector2.zero;
	Vector2 curScreenPos = Vector2.zero;

	Vector3 startWorldPos = Vector3.zero;
	Vector3 curWorldPos = Vector3.zero;

	Vector3 startTargetPos = Vector3.zero;
	Vector3 startCameraPos = Vector3.zero;

	Plane plane = new Plane (Vector3.up, Vector3.zero);

	public Transform target = null;
	Vector3 dirTargetToCamera = Vector3.zero;

	Vector3 targetPosition = Vector3.zero;
	Vector3 targetCameraPosition = Vector3.zero;

	public float followSpeed = 20f;
	float distance = 0f;

	protected override void Awake ()
	{
		base.Awake ();
	}

	protected override void Start()
	{
		base.Start ();
		dirTargetToCamera = (Camera.main.transform.position - target.position).normalized;
		distance = Vector3.Distance (Camera.main.transform.position, target.position);
		targetCameraPosition = target.position + dirTargetToCamera * distance;
		startCameraPos = Camera.main.transform.position;
	}

	// Update is called once per frame
	void Update ()
	{
		target.position = Vector3.Lerp (target.position, targetPosition, Time.deltaTime * followSpeed);
		Camera.main.transform.position = Vector3.Lerp (Camera.main.transform.position, targetCameraPosition, Time.deltaTime * followSpeed);
	}

	#region implemented abstract members of BaseRaycaster

	public override void Raycast (PointerEventData eventData, System.Collections.Generic.List<RaycastResult> resultAppendList)
	{
		RaycastResult result = new RaycastResult ();
		result.distance = 1;
		result.depth = 2;
		result.gameObject = gameObject;
		resultAppendList.Add (result);
	}

	public override Camera eventCamera {
		get { return null; }
	}

	#endregion

	#region IBeginDragHandler implementation

	public void OnBeginDrag (PointerEventData eventData)
	{
		startScreenPos = eventData.position;
		Ray ray = Camera.main.ScreenPointToRay (new Vector3 (startScreenPos.x, startScreenPos.y, 0f));
		float rayDistance;
		if (plane.Raycast (ray, out rayDistance))
		{
			startWorldPos = ray.GetPoint (rayDistance);
			startTargetPos = target.position;
		}
	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		// drag begin point.
		float rayDistance;
		Ray ray = Camera.main.ScreenPointToRay (new Vector3 (startScreenPos.x, startScreenPos.y, 0f));
		if (plane.Raycast (ray, out rayDistance)) 
		{
			startWorldPos = ray.GetPoint (rayDistance);
		}

		curScreenPos = eventData.position;
		ray = Camera.main.ScreenPointToRay (new Vector3 (curScreenPos.x, curScreenPos.y, 0f));

		if (plane.Raycast (ray, out rayDistance))
		{
			curWorldPos = ray.GetPoint (rayDistance);

			targetPosition = startTargetPos + (startWorldPos - curWorldPos);
			targetCameraPosition = targetPosition + dirTargetToCamera * distance;
		}
	}

	#endregion


	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
	}

	#endregion

	#region IPointerDownHandler implementation

	public void OnPointerDown (PointerEventData eventData)
	{
	}

	#endregion

	#region IPointerUpHandler implementation

	public void OnPointerUp (PointerEventData eventData)
	{
	}

	#endregion
}

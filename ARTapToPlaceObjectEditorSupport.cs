using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


[RequireComponent(typeof(ARRaycastManager))]

public class ARTapToPlaceObjectEditorSupport : MonoBehaviour
{
	[SerializeField]public GameObject gameObjectToInstantiate;
	private GameObject spawnedObject;
	private ARRaycastManager _arRaycastManager;
	private ARSessionOrigin _aRSessionOrigin;
	private Vector2 touchPosition;

	static List<ARRaycastHit> hits = new List<ARRaycastHit>();

	public LayerMask layerMask;
	private void Awake()
	{
		_arRaycastManager = GetComponent<ARRaycastManager>();
		_aRSessionOrigin = GetComponent<ARSessionOrigin>();
	}

	bool TryGetTouchPosition(out Vector2 touchPosition)
	{
		if (Input.touchCount > 0)
		{
			touchPosition = Input.GetTouch(0).position;
			return true;
		}
		touchPosition = default;
		return false;
	}

	void Update()
	{
		if (Application.isEditor)
		{
			Ray ray = Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth * 0.5f,Camera.main.pixelHeight * 0.5f, 0f));
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 500f, layerMask))
			{
				if (spawnedObject == null)
				{
					spawnedObject = Instantiate(gameObjectToInstantiate, hit.point, Quaternion.Euler(new Vector3(0, 0, 0)));
				}
				else
                {
					spawnedObject.transform.position = hit.point;
                }
			}
		}

		if (!TryGetTouchPosition(out Vector2 touchPosition))
		{
			return;
		}

		if (_arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
		{
			var hitPose = hits[0].pose;
			if (spawnedObject == null)
			{
				spawnedObject = Instantiate(gameObjectToInstantiate, hitPose.position, hitPose.rotation);
			}
			else
			{
				spawnedObject.transform.position=hitPose.position;
			}
		}
	}
}


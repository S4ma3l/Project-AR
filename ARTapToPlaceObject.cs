using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ARTapToPlaceObject : MonoBehaviour
{
	public GameObject gameObjectToInstantiate;

	private GameObject spawnedObject;
	private ARRaycastManager _arRaycastManager;
	private Vector2 touchPosition;
	private Vector2 touchType;
	//private float holdTime = 0.8f; //or whatever
	//private float acumTime = 0;
	static List<ARRaycastHit> hits = new List<ARRaycastHit>();

	private void Awake()
	{
		_arRaycastManager = GetComponent<ARRaycastManager>();

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
		if (!TryGetTouchPosition(out Vector2 touchPosition))
		{
			return;
		}
		if (_arRaycastManager.Raycast(touchPosition, hits, TrackableType.Planes))
		{
			var hitPose = hits[0].pose;

			if (spawnedObject == null)
			{
				spawnedObject = Instantiate(gameObjectToInstantiate, hitPose.position, hitPose.rotation);
			}
            #region Hold touch to move
           /* else
            {
				if (Input.touchCount > 0)
				{
					acumTime += Input.GetTouch(0).deltaTime;

					if (acumTime >= holdTime)
					{
						//Long tap
						spawnedObject.transform.position = hitPose.position;
					}

					if (Input.GetTouch(0).phase == TouchPhase.Ended) 
				    {
						acumTime = 0;
					}
				}
			}
		   */
            #endregion 

        }
    }

}
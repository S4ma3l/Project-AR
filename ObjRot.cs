using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRot : MonoBehaviour
{
    private Touch touch;
    private Vector3 touchPosition;
    private Quaternion rotationY;
    [SerializeField]
    private float rotateSpeedModifier = 0.1f;
    private GameObject _spawnedObject;

     private void Start()
     {
	_spawnedObject = GameObject.Find("spawnedObject");
     }

   private void Update()
    {
        if(_spawnedObject==null)
        {
            return;
        }
        else if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Moved)
            {
                rotationY = Quaternion.Euler(0f, - touch.deltaPosition.x * rotateSpeedModifier, 0f);
          
                _spawnedObject.transform.rotation = rotationY * transform.rotation;

            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]

public class objhandel : MonoBehaviour
{
    public GameObject model;
    private List<ARRaycastHit> hits;
    private ARRaycastManager manager;
    private GameObject mModel;

    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<ARRaycastManager>();
        hits = new List<ARRaycastHit>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)
            return; 
        Touch touch = Input.GetTouch(0);
        if (manager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose pose = hits[0].pose;
            if (mModel == null)
            {
                mModel = Instantiate(model, pose.position, pose.rotation);
            }
            else
            {
                // 更新模型的位置和旋转
                mModel.transform.position = pose.position;
                mModel.transform.rotation = pose.rotation;
            }
        } 
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
 
public class Skode_SetObj : MonoBehaviour
{
    #region Public Parameters
 
    [Tooltip("xingTomatoClock")]
    public GameObject objPrefab;
 
    /// <summary>
    /// 已经实例化、摆放在场景中的预制体
    /// </summary>
    [HideInInspector]
    public GameObject spawnedObject;
 
    #endregion
 
 
    #region Private Parameters
 
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    Vector3 screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
    ARRaycastManager arRaycastManager;
 
    #endregion
 
 
    #region MonoBehaviour CallBacks
 
    private void Start()
    {
        arRaycastManager = FindObjectOfType<ARRaycastManager>();
    }
 
    #endregion
 
 
    #region Public Methods
 
    //将该方法绑定在放置按钮上即可。
    public void Skode_SetTarget()
    {
        if (arRaycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon))
        {
            // 列表中元素是按命中的距离排序的，所以第一个命中的距离是最近的。
            var hitPose = hits[0].pose;
 
            if (spawnedObject == null && JudgeIsHorizontal(hitPose))
            {
                spawnedObject = Instantiate(objPrefab, hitPose.position, hitPose.rotation);
            }
        }
    }
 
    #endregion
 
 
    #region Private Methods
 
    /// <summary>
    /// 判断该平面是水平面还是竖直面
    /// </summary>
    bool JudgeIsHorizontal(Pose target)
    {
        bool value = true;
 
        if (Mathf.Abs(target.rotation.eulerAngles.x) > 20 || Mathf.Abs(target.rotation.eulerAngles.z) > 20)
            value = false;
 
        return value;
    }
 
    #endregion
}

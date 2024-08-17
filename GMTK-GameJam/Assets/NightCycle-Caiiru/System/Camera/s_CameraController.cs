using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_CameraController : MonoBehaviour
{ 
    [Header("MVP")] public Transform cameraFollowObject;
    public CameraData cameraData;
    void Start()
    {
        if (cameraData == null)
        {
            Debug.LogError("Camera Without data");
        } 
        if (cameraFollowObject == null)
            return;
    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        FollowObject();
    }

    public void FollowObject()
    {
        this.transform.position =
            Vector3.Lerp(transform.position, cameraFollowObject.position + cameraData.cameraPositionOffset, cameraData.cameraLerpTime);
        this.transform.rotation = Quaternion.Euler(cameraData.cameraRotationOffset);
    }
} 

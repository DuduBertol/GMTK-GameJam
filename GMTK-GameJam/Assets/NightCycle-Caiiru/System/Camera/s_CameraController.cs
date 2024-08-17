using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_CameraController : MonoBehaviour
{
    public CameraState cameraState = CameraState.Platformer;
    public List<CameraStateSettings> cameras = new List<CameraStateSettings>();
    
    public Vector3 cameraOffset;
    public Vector3 cameraRotationOffset;
    [Range(0.1f,0.9f)]
    public float cameraLerpTime;
    [Space(10)]
    [Header("MVP")] public Transform cameraFollowObject;
       
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        FollowObject();
    }

    public void FollowObject()
    {
        if (cameraFollowObject == null)
            return;

        this.transform.position =
            Vector3.Lerp(transform.position, cameraFollowObject.position + cameraOffset, cameraLerpTime);
        this.transform.rotation = Quaternion.Euler(cameraRotationOffset);
    }
}
[System.Serializable]
public struct CameraStateSettings{
    public string name;
    public Vector3 offset; 
    public float cameraTimeLerp;

    public CameraStateSettings(string name, Vector3 _offset, float _cameraLerpTime){
        this.name = name;
        this.offset = _offset; 
        this.cameraTimeLerp = _cameraLerpTime;
    }

}

public enum CameraState{
    Platformer,
    ThirdPerson,
    FirstPerson
}

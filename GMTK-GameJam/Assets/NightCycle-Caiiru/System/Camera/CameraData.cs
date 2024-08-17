using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName="System/Camera/Data", fileName="New Camera Data")]
public class CameraData : ScriptableObject
{
     public Vector3 cameraPositionOffset;
     public Vector3 cameraRotationOffset;
     
     [Range(0.1f,1.5f)]
     public float cameraLerpTime;
}

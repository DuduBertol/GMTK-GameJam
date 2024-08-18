using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_BeanTreeController : MonoBehaviour
{
    [SerializeField] private Transform colliderTransform;
    [SerializeField] private Transform interactionText;

    public void ToggleLeanText(bool value)
    {
        if(value)
        {
            interactionText.LeanScale(Vector3.one, 0.5f);
        }
        else
        {
            interactionText.LeanScale(Vector3.zero, 0.5f);
        }
    }
}

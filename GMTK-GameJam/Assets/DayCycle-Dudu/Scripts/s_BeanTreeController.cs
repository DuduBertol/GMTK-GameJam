using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_BeanTreeController : MonoBehaviour
{
    public int treeLevel;
    public int baseAmounts;

    [SerializeField] private Transform colliderTransform;
    [SerializeField] private Transform interactionText;
    [SerializeField] private List<GameObject> beanBasesList;
    [SerializeField] private GameObject beanBasePrefab;
    [SerializeField] private Transform beanBaseSpawnTransform;
    [SerializeField] private float heightOffsetBean;
    [SerializeField] private float rotationOffsetBean;


    private void Start() 
    {
        beanBasesList[0].SetActive(true);

        CreateBeanBases();
    }

    private void CreateBeanBases()
    {
        if(beanBasesList.Count >= baseAmounts)
        {
            return;
        }
        else
        {
            Transform lastBeanTransform = beanBasesList[beanBasesList.Count - 1].transform;

            GameObject beanBase = Instantiate(beanBasePrefab, gameObject.transform.GetChild(0));

            beanBase.transform.position = lastBeanTransform.position + new Vector3 (0, heightOffsetBean, 0);

            beanBase.transform.eulerAngles = lastBeanTransform.eulerAngles + new Vector3 (0, lastBeanTransform.rotation.y - rotationOffsetBean, 0);
            
            beanBasesList.Add(beanBase);
            beanBase.SetActive(false);

            CreateBeanBases();
        }
    }

    public void UpdateBeanTreeVisual()
    {
        treeLevel++;
        beanBasesList[treeLevel].SetActive(true);
    }

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

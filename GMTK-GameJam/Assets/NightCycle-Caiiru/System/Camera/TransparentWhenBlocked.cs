using System.Collections.Generic;
using UnityEngine;

public class TransparentWhenBlocked : MonoBehaviour
{
    public Transform player; // Referência ao jogador
    public Camera mainCamera; // Referência à câmera principal
    public Material transparentMaterial; // Material transparente
    public Material originalMaterial; // Para armazenar o material original
    public LayerMask _layerMask;
    public MeshRenderer objectRenderer;
    public Transform gameObjectParent;

    void Start()
    {
        player = GameController.Instance.GetPlayer().transform;
        mainCamera = Camera.main;
        //objectRenderer = GetComponent<MeshRenderer>();
        if (objectRenderer != null)
        {
            originalMaterial = objectRenderer.material;
        } 
    }

    void Update()
    {
        Vector3 directionToPlayer = player.position - mainCamera.transform.position;
        RaycastHit hit;
        Debug.DrawRay(mainCamera.transform.position,player.position - mainCamera.transform.position, Color.yellow);

        if (Physics.Raycast(mainCamera.transform.position, directionToPlayer, out hit,Mathf.Infinity,_layerMask))
        {  
            if (hit.transform == gameObjectParent)
            {
                
                    //MakeTransparent();
                    ChangeMaterial(gameObjectParent, transparentMaterial);
            }else{
                    ChangeMaterial(gameObjectParent, originalMaterial);
                    //RestoreOriginalMaterial();
                
            }
            /*if (hit.transform == transform)
            {
                if (IsPlayerBlockedByObject())
                {
                    MakeTransparent();
                }
                else
                {
                    RestoreOriginalMaterial();
                }
            }
            */
        }
    }

    private void ChangeMaterial(Transform _transform, Material _material ){
        for(int i = 0; i<_transform.GetChild(0).transform.childCount;i++){
            if(_transform.GetChild(0).GetChild(i).gameObject.transform.GetComponent<MeshRenderer>() != null)
                 _transform.GetChild(0).GetChild(i).gameObject.GetComponent<MeshRenderer>().material = _material;
        }
    }

    bool IsPlayerBlockedByObject()
    {
        Vector3 directionToObject = transform.position - mainCamera.transform.position;
        float cameraToObjectDistance = Vector3.Dot(directionToObject, mainCamera.transform.forward);
        float cameraToPlayerDistance = Vector3.Dot(player.position - mainCamera.transform.position, mainCamera.transform.forward);

        return cameraToObjectDistance < cameraToPlayerDistance;
    }

    void MakeTransparent()
    {
        if (objectRenderer.material != transparentMaterial)
        {
            objectRenderer.material = transparentMaterial;
        }
    }

    void RestoreOriginalMaterial()
    {
        if (objectRenderer.material != originalMaterial)
        {
            objectRenderer.material = originalMaterial;
        }
    }
}

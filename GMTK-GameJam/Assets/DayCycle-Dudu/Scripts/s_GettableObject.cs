using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_GettableObject : MonoBehaviour
{
    public enum ObjectType
    {
        GoldenEgg,
        Fertilizer,
        Chicken
    }

    public ObjectType objectType;


    private void OnCollisionEnter(Collision collision) 
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<s_PlayerCollision>().SetPlayerObjectChild(transform);
            SoundManager.Instance.PlayDropGetItemSound(Camera.main.transform.position, 1f);

            if(objectType == ObjectType.Chicken)
            {
                GameController.Instance.hasGoldenChicken = true;
            }
        }
    }
}

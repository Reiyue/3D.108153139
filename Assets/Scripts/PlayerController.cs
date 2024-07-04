using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Basic")]
    public float maxLifeValue;  
    public float lifeHpChangeUnit;  

    [Header("UI")]
    public GameManager gameManager;

    float lifeValue;

    void Start()
    {
        lifeValue = maxLifeValue;
    }

    // ¸I¼²°»´ú
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            lifeValue -= lifeHpChangeUnit;
            gameManager.UpdateLifeBar(lifeValue / maxLifeValue); 
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Basic")]
    public float maxLifeValue;  
    public float lifeHpChangeUnit;

    float lifeValue;

    public static Action<float> onHpChange;

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
            onHpChange?.Invoke(lifeValue / maxLifeValue);
        }
    }
}

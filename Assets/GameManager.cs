using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public Image lifeBarImage;

    public void UpdateLifeBar(float _value)
    {
        lifeBarImage.fillAmount = _value;
    }
}

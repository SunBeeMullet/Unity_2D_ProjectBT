using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thermo : MonoBehaviour
{
    public Sprite[] sprites;
    public Image image;

    void Awake()
    {
        image = gameObject.GetComponent<Image>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
public class changeTreeColor : MonoBehaviour
{
    private SpriteRenderer sprit;


    void Awake()
    {
        sprit = GetComponent<SpriteRenderer>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// �𽥻ָ���ɫ
    /// </summary>
    public void FadeOut()
    {
        Color targetColor = new Color(1, 1, 1, 1);
        sprit.DOColor(targetColor, Settings.fadeDuration);
    }
    /// <summary>
    /// �𽥰�͸��
    /// </summary>
    public void FadeIn()
    {
        Color targetColor = new Color(1, 1, 1, Settings.targetAlpha);
        sprit.DOColor(targetColor, Settings.fadeDuration);
    }
}

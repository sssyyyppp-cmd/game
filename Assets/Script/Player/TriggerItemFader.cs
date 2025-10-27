using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerItemFader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        changeTreeColor[] trees = collision.GetComponentsInChildren<changeTreeColor>();
        if(trees.Length > 0)
        {
            foreach(var item in trees)
            {
                item.FadeIn();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        changeTreeColor[] trees = collision.GetComponentsInChildren<changeTreeColor>();
        if (trees.Length > 0)
        {
            foreach (var item in trees)
            {
                item.FadeOut();
            }
        }
    }
}

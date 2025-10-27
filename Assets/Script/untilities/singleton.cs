using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class singloten<T> : MonoBehaviour where T : singloten<T>
{
    // Start is called before the first frame update
    private static T instance;
    
    public static T Instance
    {
        get => instance;
    }


    protected virtual void Awake()
    {
        if (instance != null) 
        {
            Destroy(gameObject);
        }
        else 
        {
            instance = (T)this;
        }
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }
}

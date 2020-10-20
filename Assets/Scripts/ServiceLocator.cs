﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class ServiceLocator
{
    static private readonly Dictionary<System.Type, object> m_systems = new Dictionary<System.Type, object>();

    
    static public T Register<T>(object target)
    {
        if(Conatins<T>())
        {
            Debug.Log("There is already a type of :" + typeof(T) + "exist");

        }
        else
        {
            Debug.Log("registering ..." + typeof(T));
            m_systems.Add(typeof(T), target);
        }
        return (T)target;
    }

    static public T Get<T>()
    {
        object ret = null;
        m_systems.TryGetValue(typeof(T), out ret);
        if(ret==null)
        {
            Debug.Log("Could not find [" + typeof(T) + "] as a registered system");
        }
        return (T)ret;
    }

    static public bool Conatins<T>()
    {
        return m_systems.ContainsKey(typeof(T));
    }
}

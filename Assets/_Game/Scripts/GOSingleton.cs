using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOSingleton<T> : MonoBehaviour where T : Component
{
    private static T m_instance;

    public static T Instance()
    {
        if (m_instance == null)
        {
            m_instance = GameObject.FindObjectOfType<T>();
            if (m_instance == null)
            {
                GameObject gameObject = new GameObject();
                m_instance = gameObject.AddComponent<T>();
            }

            DontDestroyOnLoad(m_instance.gameObject);
        }

        return m_instance;

    }
}
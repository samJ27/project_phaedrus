using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Implement the singleton class and extend for singleton component. 
/// Used to manage state so only one instance / state created at once e.g. intro, middle, end etc. 
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class MonoSingleton<T> : MonoBehaviour where T: MonoSingleton<T>
{	public static bool isTemporaryInstance { private set; get; }
	public static bool isInitialized { private set; get; }

	private static T m_Instance = null;

	public static T Instance
    {
        get
        {
            // Setup instance if first time
            if (m_Instance == null)
            {
                m_Instance = Object.FindObjectOfType(typeof(T)) as T;

                // If object not found, create a temporary one
                if (m_Instance == null)
                {
                    Debug.LogWarning("No instance of " + typeof(T).ToString() + ", a temporary one is created.");
                    isTemporaryInstance = true;
                    m_Instance = new GameObject("Temp Instance of " + typeof(T).ToString(), typeof(T)).GetComponent<T>();

                    // Problem during the creation, this should not happen
                    if (m_Instance == null)
                    {
                        Debug.LogError("Problem during the creation of " + typeof(T).ToString());
                    }
                }
                if (!isInitialized)
                {
                    isInitialized = true;
                    m_Instance.Init();
                }
            }
            return m_Instance;
        }
    }
    /// <summary>
    /// This function is called when the instance is used the first time
    /// Put all the initializations you need here, as you would do in Awake
    /// </summary>
    public virtual void Init() { }
    private void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this as T;
        }
        else if (m_Instance != this)
        {
            Debug.LogError("Another instance of " + GetType() + " already exists! Destroying self...");
            DestroyImmediate(this);
            return;
        }
        if (!isInitialized)
        {
            isInitialized = true;
            m_Instance.Init();
        }
    }
    private void OnDestroy()
    {
        isInitialized = false;
    }
    private void OnApplicationQuit()
    {
        m_Instance = null;
    }
}
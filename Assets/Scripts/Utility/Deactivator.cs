using UnityEngine;
using System.Collections;

public class Deactivator : MonoBehaviour
{
    // Utility script to prevent errors from OnDisable() methods in scripts when quitting the application
    void OnApplicationQuit()
    {
        MonoBehaviour[] scripts = Object.FindObjectsOfType<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = false;
        }
    }
}

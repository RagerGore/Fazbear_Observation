using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeHandler : MonoBehaviour
{
    public void ChangeScene([SerializeField] string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}

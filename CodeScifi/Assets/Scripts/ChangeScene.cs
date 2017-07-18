using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //You will need this in order to change scenes

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string loadLevel;

    // When Entering it should change level as long as the Tag is Player if not, nothing will happen
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            SceneManager.LoadScene(loadLevel);
        }
    }
}

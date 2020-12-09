using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void ActivateMenu(GameObject menuToActivate)
    {
        if(!menuToActivate.activeSelf)
            menuToActivate.SetActive(true);
            
    }

    public void DeactivateMenu(GameObject menuToDeactivate)
    {
        if (menuToDeactivate.activeSelf)
            menuToDeactivate.SetActive(false);
    }

    public void LoadNew()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LoadThis()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

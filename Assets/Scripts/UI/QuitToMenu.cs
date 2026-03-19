using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class QuitToMenu: MonoBehaviour
{
    [SerializeField] PlayerInput input;
    [SerializeField] int sceneIndexToLoad;

    void Awake()
    {
        var action = input.actions.FindAction("Quit");
        action.Enable();
        action.performed += Quit;
    }

    void Quit(InputAction.CallbackContext _)
    {
        input.actions.FindAction("Quit").performed -= Quit;
        SceneManager.LoadScene(sceneIndexToLoad);
    }
}
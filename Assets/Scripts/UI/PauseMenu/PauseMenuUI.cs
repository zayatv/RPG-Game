using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private Player player;

    private void Update()
    {
        SetPauseMenuUI();
    }

    private void OnDisable()
    {
        ClosePauseMenu();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void SetPauseMenuUI()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePauseMenu();
        }
    }

    public void ClosePauseMenu()
    {
        gameObject.SetActive(false);

        ContinueTime();

        player.Input.PlayerActions.Enable();

        UIManager.IsInMenu = false;
    }

    private void ContinueTime()
    {
        Time.timeScale = 1f;
    }
}

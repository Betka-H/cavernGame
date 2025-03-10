using UnityEngine;
using UnityEngine.SceneManagement;

public class gameStartController : MonoBehaviour
{
    int gameSceneIndex = 1;

    public void startGame()
    {
        SceneManager.LoadScene(gameSceneIndex);
    }
}

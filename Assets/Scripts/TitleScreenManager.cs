using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartGame()
    {
        // 메인 게임 씬을 불러옵니다.
        SceneManager.LoadScene("MainGame");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

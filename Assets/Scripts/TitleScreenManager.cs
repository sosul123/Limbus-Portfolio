using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartGame()
    {
        // ���� ���� ���� �ҷ��ɴϴ�.
        SceneManager.LoadScene("MainGame");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

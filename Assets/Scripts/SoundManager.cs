using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip SlashAttackSound; // ���� ����
    public AudioClip BluntAttackSound; // Ÿ�� ����
    public AudioClip PierceAttackSound; // ���� ����
    public AudioClip ClashSound; // �浹 ����
    public AudioClip CoinTossSound; // ���� �佺 ����

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.5f; // ���� ���� (0.0f ~ 1.0f)        
    }

    public void PlayOneShot( AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}   

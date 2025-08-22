using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip SlashAttackSound; // 참격 사운드
    public AudioClip BluntAttackSound; // 타격 사운드
    public AudioClip PierceAttackSound; // 관통 사운드
    public AudioClip ClashSound; // 충돌 사운드
    public AudioClip CoinTossSound; // 코인 토스 사운드

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.5f; // 볼륨 설정 (0.0f ~ 1.0f)        
    }

    public void PlayOneShot( AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}   

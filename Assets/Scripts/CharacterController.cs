using UnityEditor.U2D.Animation;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public CharacterData characterData; // 캐릭터 데이터

    private int currentHealth; // 현재 체력
    private int currentSanity; // 현재 정신력
    private SpriteRenderer spriteRenderer; // 스프라이트 렌더러
    [SerializeField] private GameObject soundManager; // 사운드 매니저

    private void Awake()
    {
        currentHealth = characterData.MaxHealth; // 초기 체력 설정
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = characterData.CharacterSprite; // 스프라이트 설정
    }
    public void TakeDamage(int damage)
    {
        soundManager.GetComponent<SoundManager>().PlayOneShot(characterData.HitSound); // 피격 사운드 재생
        currentHealth -= damage;
    }

    public void changeSanity(int changeAmount)
    {
        currentSanity += changeAmount;
        if (currentSanity < -45)
        {
            currentSanity = -CharacterData.MAX_SANITY; // 정신력이 0 이하로 떨어지지 않도록 제한
        }
        else if (currentSanity > CharacterData.MAX_SANITY)
        {
            currentSanity = CharacterData.MAX_SANITY; // 정신력이 최대값을 넘지 않도록 제한
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void SetsoundManaer(GameObject soundManagerObject)
    {
        soundManager = soundManagerObject; // 사운드 매니저 설정
    }

    public int GetCurrentSanity()
    {
        return currentSanity; // 현재 정신력 반환
    }
}

using UnityEditor.U2D.Animation;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public CharacterData characterData; // ĳ���� ������

    private int currentHealth; // ���� ü��
    private int currentSanity; // ���� ���ŷ�
    private SpriteRenderer spriteRenderer; // ��������Ʈ ������
    [SerializeField] private GameObject soundManager; // ���� �Ŵ���

    private void Awake()
    {
        currentHealth = characterData.MaxHealth; // �ʱ� ü�� ����
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = characterData.CharacterSprite; // ��������Ʈ ����
    }
    public void TakeDamage(int damage)
    {
        soundManager.GetComponent<SoundManager>().PlayOneShot(characterData.HitSound); // �ǰ� ���� ���
        currentHealth -= damage;
    }

    public void changeSanity(int changeAmount)
    {
        currentSanity += changeAmount;
        if (currentSanity < -45)
        {
            currentSanity = -CharacterData.MAX_SANITY; // ���ŷ��� 0 ���Ϸ� �������� �ʵ��� ����
        }
        else if (currentSanity > CharacterData.MAX_SANITY)
        {
            currentSanity = CharacterData.MAX_SANITY; // ���ŷ��� �ִ밪�� ���� �ʵ��� ����
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void SetsoundManaer(GameObject soundManagerObject)
    {
        soundManager = soundManagerObject; // ���� �Ŵ��� ����
    }

    public int GetCurrentSanity()
    {
        return currentSanity; // ���� ���ŷ� ��ȯ
    }
}

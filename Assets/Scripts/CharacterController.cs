using UnityEditor.U2D.Animation;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public CharacterData characterData; // ĳ���� ������

    private int currentHealth; // ���� ü��
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
    public int GetCurrentHealth()
    {
        return currentHealth;
    }
    public void SetsoundManaer(GameObject soundManagerObject)
    {
        soundManager = soundManagerObject; // ���� �Ŵ��� ����
    }
}

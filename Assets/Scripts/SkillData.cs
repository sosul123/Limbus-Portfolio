using UnityEngine;
public enum SkillAttribute
{
    Slash, // Slash
    Pierce, // Pierce
    Blunt  // Blunt
}


[System.Serializable] // �� Ŭ������ �ν����� â�� ǥ���ϱ� ���� ��ɾ�
public class SkillData
{
    public string skillName;
    public int power; // �⺻����
    public int coinBonusPower; // ���� ���ʽ� ����
    public int coinCount; // ���� ����
    public SkillAttribute attribute;
}
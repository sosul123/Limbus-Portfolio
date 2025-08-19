using UnityEngine;
public enum SkillAttribute
{
    ����, // Slash
    ����, // Pierce
    Ÿ��  // Blunt
}


[System.Serializable] // �� Ŭ������ �ν����� â�� ǥ���ϱ� ���� ��ɾ�
public class SkillData
{
    public string skillName;
    public int power;
    public SkillAttribute attribute;
}
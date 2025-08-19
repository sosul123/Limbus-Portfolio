using UnityEngine;
public enum SkillAttribute
{
    참격, // Slash
    관통, // Pierce
    타격  // Blunt
}


[System.Serializable] // 이 클래스를 인스펙터 창에 표시하기 위한 명령어
public class SkillData
{
    public string skillName;
    public int power;
    public SkillAttribute attribute;
}
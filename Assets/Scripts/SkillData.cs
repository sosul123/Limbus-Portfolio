using UnityEngine;
public enum SkillAttribute
{
    Slash, // Slash
    Pierce, // Pierce
    Blunt  // Blunt
}


[System.Serializable] // 이 클래스를 인스펙터 창에 표시하기 위한 명령어
public class SkillData
{
    public string skillName;
    public int power; // 기본위력
    public int coinBonusPower; // 코인 보너스 위력
    public int coinCount; // 코인 개수
    public SkillAttribute attribute;
}
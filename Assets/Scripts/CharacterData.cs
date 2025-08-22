using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CharaterData", menuName = "Scriptable Objects/CharaterData")]
public class CharacterData : ScriptableObject
{
    public const int MAX_SANITY = 45;

    [SerializeField] private string characterName;
    [SerializeField] private int maxHealth;
    [SerializeField] private int maxSpeed;
    [SerializeField] private int minSpeed;

    [SerializeField] private Sprite characterSprite;
    [SerializeField] private List <SkillData> skills; // 스킬 데이터 리스트
    [SerializeField] private AudioClip hitSound;

    public string CharacterName { get { return characterName; } }
    public int MaxHealth { get { return maxHealth; } }
    public int MaxSpeed { get { return maxSpeed; } }    
    public int MinSpeed { get { return minSpeed; } }    
    public Sprite CharacterSprite { get { return characterSprite; } }   
    public List<SkillData> Skills { get { return skills; } } // 스킬 데이터 리스트 반환
    public AudioClip HitSound { get { return hitSound; } } // 피격 사운드 반환 

}

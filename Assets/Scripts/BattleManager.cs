using System.Collections.Generic;
using UnityEngine;

public class NewEmptyCSharpScript : MonoBehaviour
{

    // 캐릭터와 적 프리팹을 유니티 에디터에서 연결할 변수
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    // 캐릭터와 적이 생성될 위치를 지정할 변수
    public Transform playerSpawnPoint;
    public Transform enemySpawnPoint;

    // Start is called before the first frame update



    public List<SkillData> playerSkills; // 플레이어의 스킬 목록
    public List<SkillData> enemySkills;  // 적의 스킬 목록

    private SkillData selectedPlayerSkill; // 플레이어가 선택한 스킬
    private SkillData selectedEnemySkill;  // 적이 선택한 스킬

    void Start()
    {
        // 게임이 시작되면 지정된 위치에 플레이어와 적을 생성(Instantiate)
        Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
        Instantiate(enemyPrefab, enemySpawnPoint.position, Quaternion.identity);

        Debug.Log("전투 시작!");
    }


    // 1. 플레이어가 스킬 버튼을 눌렀을 때 호출될 함수
    public void OnSkillButtonClick(int skillIndex)
    {
        // 플레이어 스킬 선택
        selectedPlayerSkill = playerSkills[skillIndex];

        // 적 스킬 랜덤 선택 (간단한 AI)
        int randomEnemySkillIndex = Random.Range(0, enemySkills.Count);
        selectedEnemySkill = enemySkills[randomEnemySkillIndex];

        Debug.Log($"플레이어 스킬: {selectedPlayerSkill.skillName} (위력 {selectedPlayerSkill.power})");
        Debug.Log($"적 스킬: {selectedEnemySkill.skillName} (위력 {selectedEnemySkill.power})");

        // 합 로직 실행
        ResolveClash();
    }

    // 2. '합' 결과를 판정하는 함수
    void ResolveClash()
    {
        // 1차: 위력 비교
        if (selectedPlayerSkill.power > selectedEnemySkill.power)
        {
            PlayerWins();
        }
        else if (selectedPlayerSkill.power < selectedEnemySkill.power)
        {
            EnemyWins();
        }
        else // 2차: 위력이 같을 경우 속성 비교
        {
            // 1일차에 정했던 상성 관계를 코드로 구현
            // 예: (참격 > 타격), (타격 > 관통), (관통 > 참격)
            if ((selectedPlayerSkill.attribute == SkillAttribute.참격 && selectedEnemySkill.attribute == SkillAttribute.타격) ||
                (selectedPlayerSkill.attribute == SkillAttribute.타격 && selectedEnemySkill.attribute == SkillAttribute.관통) ||
                (selectedPlayerSkill.attribute == SkillAttribute.관통 && selectedEnemySkill.attribute == SkillAttribute.참격))
            {
                PlayerWins();
            }
            else if (selectedPlayerSkill.attribute == selectedEnemySkill.attribute)
            {
                Draw();
            }
            else
            {
                EnemyWins();
            }
        }
    }

    // 3. 결과 처리 함수들 (지금은 로그만 출력)
    void PlayerWins()
    {
        Debug.Log("합 결과: 플레이어 승리!");
    }

    void EnemyWins()
    {
        Debug.Log("합 결과: 적 승리!");
    }

    void Draw()
    {
        Debug.Log("합 결과: 무승부");
    }
}

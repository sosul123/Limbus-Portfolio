using System.Collections.Generic;
using UnityEngine;

public class NewEmptyCSharpScript : MonoBehaviour
{

    // ĳ���Ϳ� �� �������� ����Ƽ �����Ϳ��� ������ ����
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    // ĳ���Ϳ� ���� ������ ��ġ�� ������ ����
    public Transform playerSpawnPoint;
    public Transform enemySpawnPoint;

    // Start is called before the first frame update



    public List<SkillData> playerSkills; // �÷��̾��� ��ų ���
    public List<SkillData> enemySkills;  // ���� ��ų ���

    private SkillData selectedPlayerSkill; // �÷��̾ ������ ��ų
    private SkillData selectedEnemySkill;  // ���� ������ ��ų

    void Start()
    {
        // ������ ���۵Ǹ� ������ ��ġ�� �÷��̾�� ���� ����(Instantiate)
        Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
        Instantiate(enemyPrefab, enemySpawnPoint.position, Quaternion.identity);

        Debug.Log("���� ����!");
    }


    // 1. �÷��̾ ��ų ��ư�� ������ �� ȣ��� �Լ�
    public void OnSkillButtonClick(int skillIndex)
    {
        // �÷��̾� ��ų ����
        selectedPlayerSkill = playerSkills[skillIndex];

        // �� ��ų ���� ���� (������ AI)
        int randomEnemySkillIndex = Random.Range(0, enemySkills.Count);
        selectedEnemySkill = enemySkills[randomEnemySkillIndex];

        Debug.Log($"�÷��̾� ��ų: {selectedPlayerSkill.skillName} (���� {selectedPlayerSkill.power})");
        Debug.Log($"�� ��ų: {selectedEnemySkill.skillName} (���� {selectedEnemySkill.power})");

        // �� ���� ����
        ResolveClash();
    }

    // 2. '��' ����� �����ϴ� �Լ�
    void ResolveClash()
    {
        // 1��: ���� ��
        if (selectedPlayerSkill.power > selectedEnemySkill.power)
        {
            PlayerWins();
        }
        else if (selectedPlayerSkill.power < selectedEnemySkill.power)
        {
            EnemyWins();
        }
        else // 2��: ������ ���� ��� �Ӽ� ��
        {
            // 1������ ���ߴ� �� ���踦 �ڵ�� ����
            // ��: (���� > Ÿ��), (Ÿ�� > ����), (���� > ����)
            if ((selectedPlayerSkill.attribute == SkillAttribute.���� && selectedEnemySkill.attribute == SkillAttribute.Ÿ��) ||
                (selectedPlayerSkill.attribute == SkillAttribute.Ÿ�� && selectedEnemySkill.attribute == SkillAttribute.����) ||
                (selectedPlayerSkill.attribute == SkillAttribute.���� && selectedEnemySkill.attribute == SkillAttribute.����))
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

    // 3. ��� ó�� �Լ��� (������ �α׸� ���)
    void PlayerWins()
    {
        Debug.Log("�� ���: �÷��̾� �¸�!");
    }

    void EnemyWins()
    {
        Debug.Log("�� ���: �� �¸�!");
    }

    void Draw()
    {
        Debug.Log("�� ���: ���º�");
    }
}

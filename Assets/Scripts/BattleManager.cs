using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // Slider를 사용하기 위해 추가


public class BattleManager : MonoBehaviour
{

    // 캐릭터와 적 프리팹을 유니티 에디터에서 연결할 변수
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Slider playerHealthSlider; // 플레이어 체력 바
    public Slider enemyHealthSlider;  // 적 체력 바

    private int playerHealth = 100; // 플레이어 체력
    private int enemyHealth = 100;  // 적 체력
    private int maxHealth = 100;    // 최대 체력
    public float moveSpeed = 5f; // 이동 속도

    public Transform playerAttackPosition; // 플레이어가 공격할 때 이동할 위치
    public Transform enemyAttackPosition;  // 적이 공격할 때 이동할 위치

    private GameObject playerObject;
    private GameObject enemyObject;
    private Animator playerAnimator;
    private Animator enemyAnimator;
    public float attackAnimationDuration = 0.8f; // 공격 애니메이션의 길이(초)

    private Vector3 playerOriginalPosition; // 플레이어의 원래 위치를 저장
    private Vector3 enemyOriginalPosition;  // 적의 원래 위치를 저장

    [Header("Effects")]
    public GameObject hitEffectPrefab;
    public GameObject clashEffectPrefab; // 충돌 이펙트 프리팹
    public Transform clashPosition;      // 이펙트가 생성될 위치

    // 캐릭터와 적이 생성될 위치를 지정할 변수
    public Transform playerSpawnPoint;
    public Transform enemySpawnPoint;

    public GameObject playerSkillPerfab;
    private Button playerSkill1;
    private Button playerSkill2;
    public Transform playerSkillPosition1;
    public Transform playerSkillPosition2;

    public List<SkillData> playerSkills; // 플레이어의 스킬 목록
    public List<SkillData> enemySkills;  // 적의 스킬 목록

    private SkillData selectedPlayerSkill; // 플레이어가 선택한 스킬
    private SkillData selectedEnemySkill;  // 적이 선택한 스킬

    public TextMeshProUGUI playerSanityText;
    public TextMeshProUGUI enemySanityText;
    private int playerSanity = 0; // 플레이어의 정신력
    private int enemySanity = 0;  // 적의 정신력


    public GameObject damageTextPrefab; // 데미지 텍스트 프리팹
    public Transform playerCanvasPosition; // 플레이어 데미지 텍스트가 생성될 위치
    public Transform enemyCanvasPosition;  // 적 데미지 텍스트가 생성될 위치
    public Canvas mainCanvas;
    public TextMeshProUGUI playerSkillNamePower;
    public TextMeshProUGUI enemySkillNamePower;

    private AudioSource audioSource;
    public AudioClip SlashAttackSound; // 참격 사운드
    public AudioClip BluntAttackSound; // 타격 사운드
    public AudioClip PierceAttackSound; // 관통 사운드
    public AudioClip ManHitSound; // 피격 사운드
    public AudioClip WomanHitSound; //피격 사운드
    public AudioClip ClashSound; // 충돌 사운드
    public AudioClip CoinTossSound; // 코인 토스 사운드

    // static 변수
    static private int MAX_COIN_TOSS_COUNT = 100; // 최대 코인 토스 횟수
    static private int MAX_SANITY = 45; // 최대 정신력


    public GameObject resultPanel; // 결과를 표시할 UI 패널
    public TextMeshProUGUI resultText; // 결과 텍스트

    public Animator coinAnimator; // 코인 애니메이터를 연결할 변수
    public Image coinImage; // 코인 애니메이션 UI의 Image 컴포넌트
    public Sprite lostCoinSprite; // '합' 패배 시 보여줄 스프라이트


    [Header("Multi-Coin UI")] // 인스펙터에서 보기 좋게 그룹화
    public GameObject coinIconPrefab; // 코인 아이콘 프리팹
    public Transform playerCoinContainer; // 코인 아이콘들이 생성될 부모 (Horizontal Layout Group)
    public Transform enemyCoinContainer; // 코인 아이콘들이 생성될 부모 (Horizontal Layout Group)
    public Sprite headsCoinSprite; // 앞면(성공) 코인 스프라이트
    public Sprite tailsCoinSprite; // 뒷면(실패) 코인 스프라이트

    private List<GameObject> playerCoinIcons = new List<GameObject>(); // 플레이어 생성된 코인 아이콘들을 관리할 리스트
    private List<GameObject> enemyCoinIcons = new List<GameObject>(); // 적의 생성된 코인 아이콘들을 관리할 리스트

    private int lastTossResultPower;


    void Start()
    {

        playerObject = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
        playerAnimator = playerObject.GetComponentInChildren<Animator>();
        playerOriginalPosition = playerObject.transform.position; // 원래 위치 저장

        enemyObject = Instantiate(enemyPrefab, enemySpawnPoint.position, Quaternion.identity);
        enemyAnimator = enemyObject.GetComponentInChildren<Animator>();
        enemyOriginalPosition = enemyObject.transform.position; // 원래 위치 저장 

        // 체력 바 초기 설정
        playerHealthSlider.maxValue = maxHealth;
        playerHealthSlider.value = playerHealth;
        enemyHealthSlider.maxValue = maxHealth;
        enemyHealthSlider.value = enemyHealth;

        GameObject newButton = Instantiate(playerSkillPerfab, playerSkillPosition1.position, Quaternion.identity, mainCanvas.transform);
        playerSkill1 = newButton.GetComponent<Button>();
        UpdateSkillButton(playerSkill1, 1);
        newButton = Instantiate(playerSkillPerfab, playerSkillPosition2.position, Quaternion.identity, mainCanvas.transform);
        playerSkill2 = newButton.GetComponent<Button>();
        UpdateSkillButton(playerSkill2, 2);


        // 이 스크립트가 붙어있는 게임 오브젝트의 AudioSource 컴포넌트를 가져옴
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.5f; // 볼륨 설정 (0.0f ~ 1.0f)

        Debug.Log("전투 시작!");
    }
    //해당 버튼 랜덤 스킬의 내용으로 업데이트
    public void UpdateSkillButton( Button skillButton, int position)
    {
        int skillIndex = Random.Range(0, playerSkills.Count); // 플레이어 스킬 목록에서 랜덤 인덱스 선택
        if (skillIndex < playerSkills.Count)
        {
            SkillData skill = playerSkills[skillIndex];
            skillButton.GetComponentInChildren<TextMeshProUGUI>().text = $"{skill.skillName}";
            skillButton.onClick.RemoveAllListeners(); // 기존 리스너 제거
            int index = skillIndex; // 지역 변수로 복사
            skillButton.onClick.AddListener(() => OnSkillButtonClick(index, position)); // 버튼 클릭 시 호출될 함수 
        }

    }
    // 1. 플레이어가 스킬 버튼을 눌렀을 때 호출될 함수
    public void OnSkillButtonClick(int skillIndex, int position)
    {
        playerSkill1.gameObject.SetActive(false); // 스킬 버튼 비활성화
        playerSkill2.gameObject.SetActive(false); // 스킬 버튼 비활성화

        // 플레이어 스킬 선택
        selectedPlayerSkill = playerSkills[skillIndex];

        // 적 스킬 랜덤 선택 (간단한 AI)
        int randomEnemySkillIndex = Random.Range(0, enemySkills.Count);
        selectedEnemySkill = enemySkills[randomEnemySkillIndex];

        Debug.Log($"플레이어 스킬: {selectedPlayerSkill.skillName} (위력 {selectedPlayerSkill.power})");
        Debug.Log($"적 스킬: {selectedEnemySkill.skillName} (위력 {selectedEnemySkill.power})");
        playerSkillNamePower.text = $"{selectedPlayerSkill.skillName} (기본위력: {selectedPlayerSkill.power}), 코인 : {selectedPlayerSkill.coinCount} , 코인보너스 : {selectedPlayerSkill.coinBonusPower} ";
        enemySkillNamePower.text = $"{selectedEnemySkill.skillName} (기본위력: {selectedEnemySkill.power}), 코인 : {selectedEnemySkill.coinCount} , 코인보너스 : {selectedEnemySkill.coinBonusPower} ";
        // 합 로직 실행
        StartCoroutine(ClashSequenceCoroutine());

        // 선택한 스킬 버튼 다른 랜덤 스킬로 변경
        if(position == 1)
        {
            UpdateSkillButton(playerSkill1, 1);
        }
        else if (position == 2)
        {
            UpdateSkillButton(playerSkill2, 2);
        }
    }

    // 2. 코인토스
    private bool PerformCoinToss(int sanity)
    {
        // 정신력에 따라 동전 던지기 결과를 결정
        // 예: 정신력이 높을수록 성공 확률이 높아짐
        float successChance = 0.5f + (sanity * 0.01f); // 기본확률 50% 정신력당 5% 증가

        // 동전 던지기 확률 계산
        if (Random.value < successChance)
        {
            Debug.Log("코인: 앞면!");
            return true; // 앞면 (성공)
        }
        else
        {
            Debug.Log("코인: 뒷면!");
            return false; // 뒷면 (실패)
        }

    }

    void PlayerSanityUpdate( int amount)
    {
        if(playerSanity + amount> MAX_SANITY)
        {
            playerSanity = MAX_SANITY; // 최대 정신력 초과 방지
        }
        else if (playerSanity + amount < -MAX_SANITY)
        {
            playerSanity = -MAX_SANITY; // 최소 정신력 0으로 제한
        }
        else
        {
            playerSanity += amount; // 정신력 업데이트
        }
    }
    void EnemySanityUpdate(int amount)
    {
        if (enemySanity + amount > MAX_SANITY)
        {
            enemySanity = MAX_SANITY; // 최대 정신력 초과 방지
        }
        else if (enemySanity + amount < -MAX_SANITY)
        {
            enemySanity = -MAX_SANITY; // 최소 정신력 0으로 제한
        }
        else
        {
            enemySanity += amount; // 정신력 업데이트
        }
    }

    void Draw()
    {
        Debug.Log("합 결과: 무승부");
    }

    // 정신력 증감 함수
    void UpdateSanityUI()
    {
        playerSanityText.text = "" + playerSanity;
        enemySanityText.text = "" + enemySanity;
    }

    // 데미지 텍스트를 생성하는 함수
    void ShowDamageText(int damage, Transform position)
    {
        GameObject textInstance = Instantiate(damageTextPrefab, position.position, Quaternion.identity, mainCanvas.transform); // BattleManager의 자식으로 생성
        textInstance.GetComponent<TextMeshProUGUI>().text = damage.ToString();
        // 간단한 애니메이션을 위해 Destroy를 지연시킴
        Destroy(textInstance, 1f); // 1초 뒤에 사라짐
    }

    void CheckForGameEnd()
    {
        if (playerHealth <= 0)
        {
            // 플레이어 패배
            resultPanel.SetActive(true);
            resultText.text = "you die";
            // resultPanel의 순서를 맨 마지막(가장 위)으로 변경
            resultPanel.transform.SetAsLastSibling();
            Time.timeScale = 0f; // 게임 일시 정지
        }
        else if (enemyHealth <= 0)
        {
            // 플레이어 승리
            resultPanel.SetActive(true);
            resultText.text = "you win";
            // resultPanel의 순서를 맨 마지막(가장 위)으로 변경
            resultPanel.transform.SetAsLastSibling();
            Time.timeScale = 0f; // 게임 일시 정지
        }
    }

    //  코인 토스 연출을 위한 코루틴
    IEnumerator TossCoinsCoroutine(SkillData skill, int coinCount, int sanity, Transform container, List<GameObject> iconList)
    {
        // 1. 스킬에 맞춰 코인 UI 생성
        UpdateCoinUI(skill, container, iconList, coinCount);

        // 2. 최종 위력 계산 및 각 코인 연출
        int finalPower = skill.power;
        Debug.Log($"'{skill.skillName}' 시작! 기본 위력: {finalPower}");

        for (int i = 0; i < coinCount; i++)
        {
            GameObject currentCoinIcon = iconList[i];
            Image coinImage = currentCoinIcon.GetComponent<Image>();

            // 간단한 플립 연출
            currentCoinIcon.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            yield return new WaitForSeconds(0.1f);
            currentCoinIcon.transform.localScale = Vector3.one;
            audioSource.PlayOneShot(CoinTossSound); // 코인 토스 사운드 재생
            // 코인 토스 실행 및 결과 적용
            bool isHeads = PerformCoinToss(sanity);
            if (isHeads)
            {
                finalPower += skill.coinBonusPower;
                coinImage.sprite = headsCoinSprite;
                Debug.Log($"코인 {i + 1}: 앞면! (+{skill.coinBonusPower}) 현재 위력: {finalPower}");
            }
            else
            {
                coinImage.sprite = tailsCoinSprite;
                Debug.Log($"코인 {i + 1}: 뒷면! 위력 변화 없음.");
            }
        }

        yield return new WaitForSeconds(0.3f); // 모든 코인 토스 후 잠시 대기


        lastTossResultPower = finalPower; // 최종 위력을 저장
    }

    // [수정] 전체 전투 흐름을 관리하는 메인 코루틴
    IEnumerator ClashSequenceCoroutine()
    {
        int playerCoinCount = selectedPlayerSkill.coinCount; // 플레이어 코인 개수
        int enemyCoinCount = selectedEnemySkill.coinCount; // 적 코인 개수
        int coinTossCount = 0; // 합 횟수

        int finalPlayerPower = 0;
        int finalEnemyPower = 0;


        // ----- 1. 공격 위치로 이동 -----
        playerAnimator.SetBool("isWalking", true); // <<-- 걷기 시작!
        while (Vector3.Distance(playerObject.transform.position, playerAttackPosition.position) > 0.1f)
        {
            playerObject.transform.position = Vector3.MoveTowards(playerObject.transform.position, playerAttackPosition.position, moveSpeed * Time.deltaTime);
            yield return null;
        }
        playerObject.transform.position = playerAttackPosition.position; // 정확한 위치 보정
        playerAnimator.SetBool("isWalking", false); // <<-- 걷기 멈춤!
        enemyAnimator.SetBool("isWalking", true); // 적도 걷기 시작!
        while (Vector3.Distance(enemyObject.transform.position, enemyAttackPosition.position) > 0.1f)
        {
            enemyObject.transform.position = Vector3.MoveTowards(enemyObject.transform.position, enemyAttackPosition.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        enemyObject.transform.position = enemyAttackPosition.position; // 정확한 위치 보정
        enemyAnimator.SetBool("isWalking", false); // 적 걷기 멈춤!

        while (playerCoinCount > 0 && enemyCoinCount > 0 && coinTossCount < MAX_COIN_TOSS_COUNT)
        {
            // ----- 플레이어 턴 -----
            yield return StartCoroutine(TossCoinsCoroutine(selectedPlayerSkill, playerCoinCount,playerSanity,playerCoinContainer, playerCoinIcons));
            finalPlayerPower = lastTossResultPower;

            // ----- 적 턴 -----
            yield return StartCoroutine(TossCoinsCoroutine(selectedEnemySkill, enemyCoinCount, enemySanity, enemyCoinContainer, enemyCoinIcons));
            finalEnemyPower = lastTossResultPower;

            // 공격 애니메이션 실행
            playerAnimator.SetTrigger("AttackTrigger");
            enemyAnimator.SetTrigger("AttackTrigger");

            // --- 이 부분을 추가 ---
            if (clashEffectPrefab != null && clashPosition != null)
            {

                // Z축을 기준으로 0도에서 360도 사이의 무작위 회전값 생성
                Quaternion randomRotation = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));

                // 무작위 회전값으로 이펙트 생성
                Instantiate(clashEffectPrefab, clashPosition.position, randomRotation);
                audioSource.PlayOneShot(ClashSound); // 충돌 사운드 재생

            }

            // 애니메이션이 끝날 때까지 대기
            yield return new WaitForSeconds(attackAnimationDuration);

            if ( finalPlayerPower > finalEnemyPower)
            {
                enemyCoinCount--; // 적의 코인 개수 감소
            }
            else if (finalPlayerPower < finalEnemyPower)
            {
                playerCoinCount--; // 플레이어의 코인 개수 감소
            }
            else
            {
                Draw(); // 무승부 처리
            }

            ClearAllCoinIcons();
            yield return new WaitForSeconds(0.5f);


        }


        // ----- 최종 결과 판정 -----

        if (finalPlayerPower > finalEnemyPower)
        {
            yield return StartCoroutine(PlayerWinsCoroutine(playerCoinCount));
        }
        else if (finalPlayerPower < finalEnemyPower)
        {
            yield return StartCoroutine(EnemyWinsCoroutine(enemyCoinCount));
        }
        else
        {
            Draw();
        }
        playerSkillNamePower.text = ""; // 스킬 이름과 위력 텍스트 초기화
        enemySkillNamePower.text = ""; // 스킬 이름과 위력 텍스트 초기화 
        playerSkill1.gameObject.SetActive(true); // 스킬 버튼 다시 활성화
        playerSkill2.gameObject.SetActive(true); // 스킬 버튼 다시 활성화

        // ----- 3. 원래 위치로 복귀 -----
        playerAnimator.SetBool("isWalking", true); // <<-- 걷기 시작!
        while (Vector3.Distance(playerObject.transform.position, playerOriginalPosition) > 0.1f)
        {
            playerObject.transform.position = Vector3.MoveTowards(playerObject.transform.position, playerOriginalPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        playerObject.transform.position = playerOriginalPosition;
        playerAnimator.SetBool("isWalking", false); // <<-- 걷기 멈춤!
        enemyAnimator.SetBool("isWalking", true); // 적도 걷기 시작!
        while (Vector3.Distance(enemyObject.transform.position, enemyOriginalPosition) > 0.1f)
        {
            enemyObject.transform.position = Vector3.MoveTowards(enemyObject.transform.position, enemyOriginalPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        enemyObject.transform.position = enemyOriginalPosition; // 정확한 위치 보정
        enemyAnimator.SetBool("isWalking", false); // 적 걷기 멈춤!

    }

    // 모든 코인 아이콘을 정리하는 헬퍼 함수
    public void ClearAllCoinIcons()
    {
        foreach (var icon in playerCoinIcons) Destroy(icon);
        playerCoinIcons.Clear();
        foreach (var icon in enemyCoinIcons) Destroy(icon);
        enemyCoinIcons.Clear();
    }

    void UpdateCoinUI(SkillData skill, Transform container, List<GameObject> iconList, int coinCount)
    {
        // 2. 스킬의 코인 수만큼 새로 생성
        for (int i = 0; i < coinCount; i++)
        {
            GameObject newIcon = Instantiate(coinIconPrefab, container);
            iconList.Add(newIcon);
        }
    }


    // [수정] PlayerWins를 코루틴으로 변경
    IEnumerator PlayerWinsCoroutine(int remainingCoins)
    {
        PlayerSanityUpdate(5);
        EnemySanityUpdate(-5);
        UpdateSanityUI();

        int totalDamage = selectedPlayerSkill.power; // 기본 위력으로 시작

        // 1. 남은 코인 수만큼 공격 연출 반복
        for (int i = 0; i < remainingCoins; i++)
        {
            // ✨ [핵심] ✨
            // 공격 직전에 코인 아이콘을 하나 생성
            GameObject newIcon = Instantiate(coinIconPrefab, playerCoinContainer);

            Image currentCoinIcon = newIcon.GetComponent<Image>();

            playerCoinIcons.Add(newIcon); // 정리를 위해 리스트에 추가

            // 코인 토스로 추가 데미지 결정
            if (PerformCoinToss(playerSanity))
            {
                totalDamage += selectedPlayerSkill.coinBonusPower;
                currentCoinIcon.sprite = headsCoinSprite; // 앞면 이미지
            }
            else
            {
                currentCoinIcon.sprite = tailsCoinSprite; // 뒷면 이미지
            }


                // 공격 애니메이션 실행
                playerAnimator.SetTrigger("AttackTrigger");

            // 여기에 '타격' 이펙트를 적 위치에 생성하는 코드를 추가하면 좋습니다.
             Instantiate(hitEffectPrefab, enemyObject.transform.position, Quaternion.identity);

            // 스킬 속성에 따른 공격 사운드 재생
            if (selectedPlayerSkill.attribute.Equals(SkillAttribute.Slash)) audioSource.PlayOneShot(SlashAttackSound);
            else if (selectedPlayerSkill.attribute.Equals(SkillAttribute.Pierce)) audioSource.PlayOneShot(PierceAttackSound);
            else if (selectedPlayerSkill.attribute.Equals(SkillAttribute.Blunt)) audioSource.PlayOneShot(BluntAttackSound);


            // 애니메이션이 재생될 시간만큼 잠시 대기
            yield return new WaitForSeconds(attackAnimationDuration);
        }

        // 2. 최종 데미지 적용 및 피격 사운드
        audioSource.PlayOneShot(WomanHitSound);
        Debug.Log("적에게 최종 데미지 : " + totalDamage);
        enemyHealth -= totalDamage;
        ShowDamageText(totalDamage, enemyCanvasPosition);
        enemyHealthSlider.value = enemyHealth;
        ClearAllCoinIcons();
        CheckForGameEnd();

    }

    // [수정] EnemyWins를 코루틴으로 변경
    IEnumerator EnemyWinsCoroutine(int remainingCoins)
    {
        PlayerSanityUpdate(-5);
        EnemySanityUpdate(5);
        UpdateSanityUI();

        int totalDamage = selectedEnemySkill.power;

        for (int i = 0; i < remainingCoins; i++)
        {
            // ✨ [핵심] ✨
            // 공격 직전에 코인 아이콘을 하나 생성
            GameObject newIcon = Instantiate(coinIconPrefab, enemyCoinContainer);

            Image currentCoinIcon = newIcon.GetComponent<Image>();

            enemyCoinIcons.Add(newIcon); // 정리를 위해 리스트에 추가

            if (PerformCoinToss(enemySanity))
            {
                totalDamage += selectedEnemySkill.coinBonusPower;
                currentCoinIcon.sprite = headsCoinSprite; // 앞면 이미지

            }
            else
            {
                currentCoinIcon.sprite = tailsCoinSprite; // 뒷면 이미지

            }
            enemyAnimator.SetTrigger("AttackTrigger");

            Instantiate(hitEffectPrefab, playerObject.transform.position, Quaternion.identity);

            if (selectedEnemySkill.attribute.Equals(SkillAttribute.Slash)) audioSource.PlayOneShot(SlashAttackSound);
            else if (selectedEnemySkill.attribute.Equals(SkillAttribute.Pierce)) audioSource.PlayOneShot(PierceAttackSound);
            else if (selectedEnemySkill.attribute.Equals(SkillAttribute.Blunt)) audioSource.PlayOneShot(BluntAttackSound);


            yield return new WaitForSeconds(attackAnimationDuration);
        }

        audioSource.PlayOneShot(ManHitSound);
        Debug.Log("플레이어에게 최종 데미지 : " + totalDamage);
        playerHealth -= totalDamage;
        ShowDamageText(totalDamage, playerCanvasPosition);
        playerHealthSlider.value = playerHealth;
        ClearAllCoinIcons();
        CheckForGameEnd();
    }


}

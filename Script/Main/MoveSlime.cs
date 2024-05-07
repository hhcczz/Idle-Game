using UnityEngine;
using UnityEngine.UI;

public class MoveSlime : MonoBehaviour
{
    private EnemyManager enemyManager;
    private float speed = 1f; // 슬라임 이동 속도
    private float maxDistance = 315f; // 최대 이동 거리
    private bool movingForward = true; // 슬라임의 이동 방향을 나타내는 변수

    public Image CuteImg;
    public Animator CuteAnimator; // 애니메이터 컴포넌트
    public RuntimeAnimatorController[] AnimationControllers; // 애니메이션 컨트롤러 배열


    private void Start()
    {
        enemyManager = EnemyManager.Instance;
        CuteAnimator = CuteImg.GetComponent<Animator>();
    }
    public void CuteSlimeChange()
    {
        RectTransform newslimeTransform = CuteImg.GetComponent<RectTransform>();

        // 배열에서 적절한 애니메이션 컨트롤러 가져오기
        RuntimeAnimatorController controller = AnimationControllers[enemyManager.FixDifficulty + enemyManager.FixDifficultyInStage * 4];

        // 가져온 애니메이션 컨트롤러 적용
        CuteAnimator.runtimeAnimatorController = controller;


        if (enemyManager.FixDifficultyInStage == 0)
        {
            if (enemyManager.FixDifficulty == 3) CuteImg.color = ColorManager.ColorChange("빨간색");
            else CuteImg.color = ColorManager.ColorChange("하얀색");
        }
        else if(enemyManager.FixDifficultyInStage == 1) CuteImg.color = ColorManager.ColorChange("하얀색");
        else if (enemyManager.FixDifficultyInStage == 2)
        {
            if (enemyManager.FixDifficulty == 0) CuteImg.color = ColorManager.ColorChange("골렘1번색");
            if (enemyManager.FixDifficulty == 1) CuteImg.color = ColorManager.ColorChange("골렘2번색");
            if (enemyManager.FixDifficulty == 2) CuteImg.color = ColorManager.ColorChange("골렘3번색");
            if (enemyManager.FixDifficulty == 3) CuteImg.color = ColorManager.ColorChange("골렘4번색");
        }
        else CuteImg.color = ColorManager.ColorChange("하얀색");
        
        // 슬라임을 이동 방향으로 이동
        if (movingForward)
        {
            if (enemyManager.FixDifficultyInStage == 2 || enemyManager.FixDifficultyInStage >= 4)
            {
                newslimeTransform.localRotation = Quaternion.Euler(0, 0, 0);
                newslimeTransform.Translate(speed * Time.deltaTime * Vector2.right);
            }
            else 
            {
                newslimeTransform.localRotation = Quaternion.Euler(0, 180, 0);
                newslimeTransform.Translate(speed * Time.deltaTime * Vector2.left);
            }

            // 최대 이동 거리에 도달하면 이동 방향 변경
            if (newslimeTransform.anchoredPosition.x >= maxDistance) movingForward = false;
        }
        else
        {
            if (enemyManager.FixDifficultyInStage == 2 || enemyManager.FixDifficultyInStage >= 4)
            {
                newslimeTransform.localRotation = Quaternion.Euler(0, 180, 0);
                newslimeTransform.Translate(speed * Time.deltaTime * Vector2.right);
            }
            else
            {
                newslimeTransform.localRotation = Quaternion.Euler(0, 0, 0);
                newslimeTransform.Translate(speed * Time.deltaTime * Vector2.left);
            }

            // 초기 위치에 도달하면 다시 이동 방향을 전진으로 변경
            if (newslimeTransform.anchoredPosition.x <= 0f) movingForward = true;
        }
    }


    private void Update()
    {
        CuteSlimeChange();
    }
}

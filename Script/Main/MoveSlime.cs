using UnityEngine;
using UnityEngine.UI;

public class MoveSlime : MonoBehaviour
{
    private EnemyManager enemyManager;

    private float speed = 1f; // 슬라임 이동 속도
    private float maxDistance = 315f; // 최대 이동 거리
    private bool movingForward = true; // 슬라임의 이동 방향을 나타내는 변수

    public GameObject[] Slime;


    private void Start()
    {
        enemyManager = EnemyManager.Instance;
        CuteSlimeChange();
    }

    public void CuteSlimeChange()
    {
        int slimeIndex = enemyManager.FixDifficulty + enemyManager.FixDifficultyInStage * 4;
        GameObject newslime = Slime[slimeIndex];
        RectTransform newslimeTransform = newslime.GetComponent<RectTransform>();

        for (int i = 0; i < Slime.Length; i++)
        {
            Slime[i].SetActive(i == slimeIndex);
        }

        // 슬라임을 이동 방향으로 이동
        if (movingForward)
        {
            if (enemyManager.FixDifficultyInStage == 1) newslimeTransform.Translate(speed * Time.deltaTime * Vector2.left);
            else newslimeTransform.Translate(speed * Time.deltaTime * Vector2.right);

            // 최대 이동 거리에 도달하면 이동 방향 변경
            if (newslimeTransform.anchoredPosition.x >= maxDistance)
            {
                movingForward = false;
                if (enemyManager.FixDifficultyInStage == 1)
                {
                    // 슬라임의 방향을 변경합니다.
                    newslimeTransform.localRotation = Quaternion.Euler(0, 0, 0);
                }
                else newslimeTransform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
        else
        {
            // 초기 위치로 되돌아가는 이동
            if (enemyManager.FixDifficultyInStage == 1) newslimeTransform.Translate(speed * Time.deltaTime * Vector2.left);
            else newslimeTransform.Translate(speed * Time.deltaTime * Vector2.right);

            // 초기 위치에 도달하면 다시 이동 방향을 전진으로 변경
            if (newslimeTransform.anchoredPosition.x <= 0f)
            {
                movingForward = true;
                if (enemyManager.FixDifficultyInStage == 1)
                {
                    // 슬라임의 방향을 변경합니다.
                    newslimeTransform.localRotation = Quaternion.Euler(0, 180, 0);
                }
                else newslimeTransform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }


    private void Update()
    {
        CuteSlimeChange();
    }
}

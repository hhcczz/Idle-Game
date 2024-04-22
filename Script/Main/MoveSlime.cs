using UnityEngine;
using UnityEngine.UI;

public class MoveSlime : MonoBehaviour
{
    private EnemyManager enemyManager;

    private float speed = 1f; // 슬라임 이동 속도
    private float maxDistance = 315f; // 최대 이동 거리
    private bool movingForward = true; // 슬라임의 이동 방향을 나타내는 변수

    public GameObject[] Slime;

    private void Awake()
    {
        enemyManager = EnemyManager.Instance;
    }

    private void Start()
    {
        CuteSlimeChange();
    }

    public void CuteSlimeChange()
    {

        // RectTransform 컴포넌트 가져오기
        RectTransform newslime = Slime[enemyManager.FixDifficulty].GetComponent<RectTransform>();

        for (int i = 0; i < Slime.Length; i++)
        {
            if (i == enemyManager.FixDifficulty)
            {
                Slime[i].SetActive(true);
            }
            else
            {
                Slime[i].SetActive(false);
            }
        }

        // 슬라임을 이동 방향으로 이동
        if (movingForward)
        {
            newslime.Translate(speed * Time.deltaTime * Vector2.right);

            // 최대 이동 거리에 도달하면 이동 방향 변경
            if (newslime.anchoredPosition.x >= maxDistance)
            {
                movingForward = false;
            }
        }
        else
        {
            // 초기 위치로 되돌아가는 이동
            newslime.Translate(speed * Time.deltaTime * Vector2.left);

            // 초기 위치에 도달하면 다시 이동 방향을 전진으로 변경
            if (newslime.anchoredPosition.x <= 0f)
            {
                movingForward = true;
            }
        }
    }

    private void Update()
    {
        CuteSlimeChange();
    }
}

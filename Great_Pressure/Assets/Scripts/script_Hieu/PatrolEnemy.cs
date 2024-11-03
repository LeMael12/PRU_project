using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : EnemyLv4
{
    public Transform[] patrolPoints;
    public float speed;
    public float distanceThreshold = 0.1f; // Ngưỡng khoảng cách nhỏ
    int currentPointIndex;

    float waitTime;
    public float startWaitTime;

    Animator animator;

    private void Start()
    {
        transform.position = patrolPoints[0].position;
        transform.rotation = patrolPoints[0].rotation;
        waitTime = startWaitTime;  // Đặt giá trị chờ ban đầu
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Di chuyển đến vị trí mục tiêu
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);

        // Kiểm tra nếu đã gần đủ vị trí mục tiêu
        if (Vector2.Distance(transform.position, patrolPoints[currentPointIndex].position) < distanceThreshold)
        {
            //transform.rotation = patrolPoints[currentPointIndex].rotation;
            animator.SetBool("isRunning", false);
            // Nếu đã hết thời gian chờ, chuyển sang điểm tiếp theo
            if (waitTime <= 0)
            {
                if (currentPointIndex + 1 < patrolPoints.Length)
                {
                    currentPointIndex++;
                }
                else
                {
                    currentPointIndex = 0;
                }
                // Đặt lại thời gian chờ sau khi chuyển điểm
                waitTime = startWaitTime;
            }
            else
            {
                // Giảm thời gian chờ khi đứng ở điểm tuần tra
                waitTime -= Time.deltaTime;
            }
        }
        else
        {
            animator.SetBool("isRunning", true);

            // So sánh vị trí hiện tại và vị trí của điểm tuần tra tiếp theo để xác định hướng
            if (transform.position.x < patrolPoints[currentPointIndex].position.x)
            {
                animator.SetBool("isRight", true); // Di chuyển sang phải
                transform.localScale = new Vector3(1, 1, 1); // Đảm bảo đối tượng không bị lật
            }
            else if (transform.position.x > patrolPoints[currentPointIndex].position.x)
            {
                animator.SetBool("isRight", false); // Di chuyển sang trái
                transform.localScale = new Vector3(-1, 1, 1); // Lật đối tượng khi di chuyển sang trái
            }
        }

}
}

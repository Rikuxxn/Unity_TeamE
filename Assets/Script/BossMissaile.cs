using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f; // �e�̑��x
    [SerializeField] private float riseDuration = 1f; // �㏸����
    [SerializeField] private float pauseDuration = 2f; // �Î~����
    [SerializeField] private float chaseDuration = 8f; // �v���C���[�ǔ�����

    private Vector3 initialDirection; // �ŏ��̕���
    private Transform target; // �v���C���[
    private bool isChasingPlayer = true; // �v���C���[�ǔ��t���O
    private Vector3 finalDirection; // �ŏI�I�ɐi�ޕ���

    public void Initialize(Vector3 direction, Transform playerTransform, float speed)
    {
        initialDirection = direction.normalized; // ���������𐳋K��
        target = playerTransform; // �v���C���[�̎Q��
        this.speed = speed; // �e����ݒ�

        // �ŏ��̌�����ݒ�
        transform.rotation = Quaternion.LookRotation(initialDirection);

        // ������Ǘ�����R���[�`�����J�n
        StartCoroutine(HandleMovement());
    }


    private IEnumerator HandleMovement()
    {
        // �ŏ��ɏ㏸���ĐÎ~
        Vector3 upDirection = Vector3.up;
        float timer = riseDuration;

        while (timer > 0)
        {
            transform.position += upDirection * speed * Time.deltaTime;
            timer -= Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(pauseDuration); // �Î~����

        // �v���C���[�ǔ��J�n
        StartCoroutine(ChasePlayer());
    }

    private IEnumerator ChasePlayer()
    {
        float remainingChaseTime = chaseDuration;

        while (remainingChaseTime > 0 && target != null)
        {
            // �v���C���[�̕������v�Z
            Vector3 directionToPlayer = (target.position - transform.position).normalized;

            // �e�̌������X�V
            transform.rotation = Quaternion.LookRotation(directionToPlayer);

            // �v���C���[�����Ɉړ�
            transform.position += directionToPlayer * speed * Time.deltaTime;

            remainingChaseTime -= Time.deltaTime;
            yield return null;
        }

        // �ǔ��I����̐i�s������ݒ�
        isChasingPlayer = false;
        finalDirection = transform.forward; // ���݂̌�����ێ�
    }

    void Update()
    {
        if (!isChasingPlayer)
        {
            // �ǔ��I����͒��i
            transform.position += finalDirection * speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �����ɓ���������폜
        Destroy(gameObject);
    }
}

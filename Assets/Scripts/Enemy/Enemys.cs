using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Enemys : MonoBehaviour
{
    [SerializeField] private Enemy[] prefabs;
    [SerializeField] private int columns = 6;
    [SerializeField] private float distanceBetweenEnemies = 2;
    [SerializeField] private AnimationCurve speed;
    [SerializeField] private Projectile missilePrefab;
    [SerializeField] private float missileAttackRate;
    [SerializeField] private Score score;
    [SerializeField] private int totalEnemy;
    
    private int _rows;
    private Vector3 _direction = Vector2.right;
    private int _amountKilled;
    private int _enemyAtStart => _rows * columns;
    private int _amountAlive => totalEnemy - _amountKilled;
    private float _percentKilled => (float) _amountKilled / (float) totalEnemy;
    private int _enemyForSpawn;

    private void Awake()
    {
        _rows = prefabs.Length;
        _enemyForSpawn = totalEnemy - _enemyAtStart;
        
        if(totalEnemy < _enemyAtStart)
            totalEnemy = _enemyAtStart;

        for (int row = 0; row < _rows; row++)
        {
            float width = 2 * (columns - 1);
            float height = 2 * (_rows - 1);
            Vector2 centering = new Vector2(-width / 2, -height / 2);
            Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * distanceBetweenEnemies), 0);
            
            for (int col = 0; col < columns; col++)
            {
                Enemy enemy = Instantiate(prefabs[row], transform);
                enemy.killed += EnemyKilled;
                Vector3 position = rowPosition;
                position.x += col * distanceBetweenEnemies;
                enemy.transform.localPosition = position;
            }
        }
        
        InvokeRepeating(nameof(MissileAttack), missileAttackRate, missileAttackRate);
    }

    private void Update()
    {
        transform.position += _direction * speed.Evaluate(_percentKilled) * Time.deltaTime;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform enemy in transform)
        {
            if (!enemy.gameObject.activeInHierarchy)
                continue;

            if (_direction == Vector3.right && enemy.position.x >= (rightEdge.x - 1))
                AdvanceRow();
            else if (_direction == Vector3.left && enemy.position.x <= (leftEdge.x + 1))
                AdvanceRow();
        }
    }

    private void AdvanceRow()
    {
        _direction.x *= -1;

        Vector3 position = transform.position;
        position.y -= 1;
        transform.position = position;
    }

    private void MissileAttack()
    {
        foreach (Transform enemy in transform)
        {
            if (!enemy.gameObject.activeInHierarchy)
                continue;

            if (Random.value < (1 / (float) _amountAlive))
            {
                Instantiate(missilePrefab, enemy.position, Quaternion.Euler(new Vector3(0, 0, 90)));
                break;
            }
        }
    }

    private void EnemyKilled(GameObject enemy)
    {
        _amountKilled++;
        score.UpdateScore();

        if (totalEnemy > _enemyAtStart)
            StartCoroutine(respawnEnemy(enemy));

        if (_amountKilled >= totalEnemy)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private IEnumerator respawnEnemy(GameObject enemy)
    {
        yield return new WaitForSeconds(3);

        if (_enemyForSpawn > 0)
        {
            enemy.gameObject.SetActive(true);
            _enemyForSpawn--;
        }
    }
}

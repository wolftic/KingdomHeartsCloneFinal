using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private GameObject _target;
    public GameObject EnemyTarget;
    [SerializeField]
    private float _speed, _backDistance, _maxAngle = 30f, _preferedDistanceFromTarget, _height;
    private float _oldDistance, _oldAngle = 0f, _angle = 0f;

    [SerializeField]
    private Image _lockOnTarget;

    private Vector3 _center, _targetPosition;
    private Quaternion _targetRotation;

    private void Start()
    {
        _oldAngle = _angle;
    }

    private void Update()
    {
        if (!EnemyTarget || !EnemyTarget.activeSelf) return;
        var playerPos = _target.transform.position;
        var enemyPos = EnemyTarget.transform.position;
        enemyPos.y = playerPos.y = _height;

        var direction = (playerPos - _center).normalized;

        _center = ((enemyPos - playerPos) / 2) + _target.transform.position;

        _angle = Vector3.Angle(direction.normalized, (transform.position - _center).normalized);
        var cross = Vector3.Cross(direction.normalized, (transform.position - _center).normalized);

        if (_angle - _oldAngle > .1f)
        {
            var distanceFromTarget = Vector3.Distance(_targetPosition, playerPos);
            var distance = Vector3.Distance(enemyPos, playerPos);

            var newAngle = Mathf.Sin(_preferedDistanceFromTarget / distance) * 100;
            if (_preferedDistanceFromTarget < distanceFromTarget && newAngle < _angle) _angle = newAngle;

            if (cross.y < 0) _angle = -_angle;

            _angle = Mathf.Clamp(_angle, -_maxAngle, _maxAngle);

            var angleFromCenterToCamera = (Quaternion.AngleAxis(_angle, Vector3.up) * direction) * (distance / 2 + _backDistance);
            _targetPosition = _center + angleFromCenterToCamera;

            _targetRotation = Quaternion.LookRotation(_center- transform.position, Vector3.up);

            Debug.DrawRay(_center, Quaternion.Euler(0, _angle, 0) * direction * (distance / 2 + _backDistance), Color.yellow);
        }
        transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * _speed);
        transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, 10f * Time.deltaTime);

        Debug.DrawLine(_center, playerPos, Color.green);
        Debug.DrawLine(_center, enemyPos, Color.blue);
        Debug.DrawLine(_center, transform.position, Color.red);

        ManageLockTarget();

        _oldAngle = Mathf.Abs(_angle);
    }

    private void ManageLockTarget()
    {
        Vector2 playerOnScreenPosition = Camera.main.WorldToScreenPoint(EnemyTarget.transform.position);
        _lockOnTarget.rectTransform.position = playerOnScreenPosition;
    }

    private bool InBoundaries(float minX, float maxX, float minY, float maxY, Vector2 screenPos)
    {
        return screenPos.x > minX && screenPos.x < maxX && screenPos.y > minY && screenPos.y < maxY;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_center,0.25f);
    }
}
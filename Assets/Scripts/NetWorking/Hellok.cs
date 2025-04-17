using System.Globalization;
using Unity.Netcode;
using UnityEngine;

public class Hellok : NetworkBehaviour
{
    public float moveSpeed = 5f;
    private Vector3 lastServerPosition;

    void Update()
    {
        if (IsOwner) // Мы проверяем, это ли локальный игрок
        {
            HandleMovement();
        }

    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        if (direction.magnitude >= 0.1f)
        {
            transform.Translate(direction * moveSpeed * Time.deltaTime);

            // Предсказание движения на клиенте
            MovePlayerServerRpc(transform.position);
        }
    }

    [ServerRpc]
    private void MovePlayerServerRpc(Vector3 newPosition)
    {
        MovePlayerClientRpc(newPosition);
    }

    [ClientRpc]
    private void MovePlayerClientRpc(Vector3 newPosition)
    {
        if (IsOwner) return;
        // Сервер проверяет и обновляет позицию
        transform.position = newPosition;
        lastServerPosition = newPosition; // Сохраняем позицию для синхронизации
    }
}

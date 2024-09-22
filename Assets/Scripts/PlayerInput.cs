using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
    PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        KeyListener();
    }

    void KeyListener()
    {
        if (Input.GetButtonDown("Jump"))
        {
            playerMovement.Jump();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            playerMovement.SwordAttack();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            playerMovement.GunAttack();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerMovement.Death();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)&& playerMovement.isOnTheFloor)
        {
            playerMovement.ChangeRunSpeed(2f);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && playerMovement.playerRunSpeed>3)
        {
            playerMovement.ChangeRunSpeed(0.5f);
        }

        playerMovement.Move(Input.GetAxis("Horizontal"));

        
    }
}

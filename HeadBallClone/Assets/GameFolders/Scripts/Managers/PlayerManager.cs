using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private readonly Vector2 Player_Starting_Position = new Vector2(-7, -4);
    public static PlayerManager Instance { get; private set; }
    PlayerController playerController;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        playerController = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerController>();
    }

    public void ResetPlayerPosition()
    {
        playerController.transform.position = Player_Starting_Position;
    }
    public void ChangedPlayerBodyType(RigidbodyType2D rigidbodyType2D)
    {
        playerController.Rigidbody2D.bodyType = rigidbodyType2D;
    }
}

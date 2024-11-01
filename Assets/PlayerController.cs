using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _playerSprite;

    [SerializeField]
    private SnakeMovementScript snakemovement;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Construct(PlayerData playerData)
    {
        _playerSprite.color = playerData.Color;

        snakemovement.Construct(playerData);
    }
}

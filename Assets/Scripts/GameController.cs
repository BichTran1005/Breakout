using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    [SerializeField] private GameObject blockGroup;

    private int blockCount = 0;

    public void CreateListBlock(int col, int brickAmount)
    {
        blockCount = 0;

        for (int i = 0; i < brickAmount; i++)
        {
            Brick brick = ObjectPool.Get<Brick>(Define.BlockGO);
            brick.gameObject.SetActive(true);
            brick.transform.SetParent(blockGroup.transform);
            brick.InitData(1, 1);
            brick.transform.position = new Vector2(i % col * 2 - col + (col % 2), (i / col) % col + 4);
            blockCount++;
        }
    }

    public void UpdateBlockCount()
    {
        blockCount--;

        if (blockCount <= 0)
        {
            GameManager.Instance.NextLevel();
        }
    }
}

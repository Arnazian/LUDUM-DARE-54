using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLayoutController : MonoBehaviour
{
    [SerializeField] private Transform levelBlockParent;
    [SerializeField] private GameObject[] levelBlocks;
    private List<GameObject> activeLevelBlocks = new();
    [SerializeField] private Transform enemyRoot;

    private float posX = -60;

    public void Start()
    {
        for (int i = 0; i < 4; i++) CreateNextLevelBlock();
    }

    public void CreateNextLevelBlock()
    {
        GameObject newBlockDesign = levelBlocks[Random.Range(0, levelBlocks.Length)];
        GameObject newLevelBlock = Instantiate(newBlockDesign);
        newLevelBlock.transform.parent = levelBlockParent;

        enemyRoot.transform.localPosition = new(posX, 0, enemyRoot.transform.position.z);
        posX += 20f;
        newLevelBlock.transform.localPosition = new Vector2(posX, newLevelBlock.transform.position.y);
        activeLevelBlocks.Add(newLevelBlock);
        if (activeLevelBlocks.Count > 4) DestroyOldestLevelBlock();
    }

    void DestroyOldestLevelBlock()
    {
        GameObject blockToDestroy = activeLevelBlocks[0];
        activeLevelBlocks.Remove(blockToDestroy);
        Destroy(blockToDestroy);
    }
}

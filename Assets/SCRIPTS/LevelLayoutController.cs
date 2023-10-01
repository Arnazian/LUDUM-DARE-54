using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLayoutController : MonoBehaviour
{
    [SerializeField] private Transform levelBlockParent;
    [SerializeField] private GameObject[] levelBlocks;
    private List<GameObject> activeLevelBlocks = new();

    private float posX = -40;

    public void Start()
    {
        for (int i = 0; i < 3; i++) CreateNextLevelBlock();
    }

    public void CreateNextLevelBlock()
    {
        GameObject newBlockDesign = levelBlocks[Random.Range(0, levelBlocks.Length)];
        GameObject newLevelBlock = Instantiate(newBlockDesign);
        newLevelBlock.transform.parent = levelBlockParent;

        posX += 20f;
        newLevelBlock.transform.position = new Vector2(posX, newLevelBlock.transform.position.y);
        activeLevelBlocks.Add(newLevelBlock);
        if (activeLevelBlocks.Count > 3) DestroyOldestLevelBlock();
    }

    void DestroyOldestLevelBlock()
    {
        GameObject blockToDestroy = activeLevelBlocks[0];
        activeLevelBlocks.Remove(blockToDestroy);
        Destroy(blockToDestroy);
    }
}

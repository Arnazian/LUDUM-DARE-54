using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLayoutController : MonoBehaviour
{
    [SerializeField] private float distanceOffsetX = 19.2f;
    [SerializeField] private Transform levelBlockFolder;
    [SerializeField] private GameObject[] levelBlocks;
    [SerializeField] private List<GameObject> activeLevelBlocks = new List<GameObject>();

    
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void CreateNextLevelBlock()
    {
        GameObject newBlockDesign = levelBlocks[Random.Range(0, levelBlocks.Length)];
        GameObject newLevelBlock = Instantiate(newBlockDesign);
        newLevelBlock.transform.parent = levelBlockFolder;

        float newPosX = activeLevelBlocks[activeLevelBlocks.Count - 1].transform.position.x + distanceOffsetX;
        newLevelBlock.transform.position = new Vector2 (newPosX, newLevelBlock.transform.position.y);
        activeLevelBlocks.Add(newLevelBlock);
        DestroyOldestLevelBlock();
    }

    void DestroyOldestLevelBlock()
    {
        GameObject blockToDestroy = activeLevelBlocks[0];
        activeLevelBlocks.Remove(blockToDestroy);
        Destroy(blockToDestroy);
    }
}

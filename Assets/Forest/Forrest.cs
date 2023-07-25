using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forrest : MonoBehaviour
{
    [SerializeField] private Tree[] treesPrefabs;
    [SerializeField] private Vector2 bounds;

    [SerializeField] [Range(1f, 10f)] private float gap;

    [SerializeField] [Range(0f, 40f)]  private float randomPositionOffset = 0f;
 

    // private Tree[] trees;
    [SerializeField] private List<Tree> trees = new List<Tree>();

    [SerializeField] private Color dimColor;
    [SerializeField] private Color freshColor;



    [SerializeField] private Transform player;
    [SerializeField] private float startDistance;
    [SerializeField] private float endDistance;

    private float _allDistance;




    private void Start()
    {
        _allDistance = startDistance - endDistance;

        for(float x = -bounds.x; x < bounds.x; x+= gap)
        {
    
            for (float z = -bounds.y; z < bounds.y; z+= gap)
            {
                Vector3 spawnPos = new Vector3(x + Random.Range(-randomPositionOffset, randomPositionOffset), 0f, 
                                               z + Random.Range(-randomPositionOffset, randomPositionOffset));

                int index = Random.Range(0, treesPrefabs.Length);
                Tree tree = Instantiate(treesPrefabs[index], spawnPos, Quaternion.identity);

                tree.transform.parent = transform;
                trees.Add(tree);
            }
        }
    }

    public void RemoveTree(Tree tree)
    {
        trees.Remove(tree);
        Destroy(tree.gameObject);
    }

    private void Update()
    {

        foreach (Tree tree in trees)
        {
            float currentDistance = Vector3.Distance(player.position, tree.transform.position);

            if (currentDistance <= startDistance)
            {
                float t = (currentDistance - endDistance) / _allDistance;
                Color treeColor = Color.Lerp(dimColor, freshColor, t);

                tree.ChangeColor(treeColor);
                tree.LookAt(player.position);
            }
        }


    }


}

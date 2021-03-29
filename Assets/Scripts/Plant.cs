using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {
    protected char pop;
    protected char push;
    protected char line;
    protected char endLine;
    protected char translate;
    protected char rotateCW;
    protected char rotateACW;

    public GameObject branchGO;
    public GameObject leafGO;
    public float maxAngle;
    public float minAngle;
    public float moveAmount;

    protected LSystem lSystem;
    protected char[] treeString;
    protected List<GameObject> branches;
    protected GameObject tree;

    void Start()
    {
        Configure();
        GenerateLSystem(5);
        Draw(Vector3.zero);
    }

    void Configure()
    {
        lSystem = new LSystem("0", new Rule[] {
            new Rule('1', "1"), //OR for a bigger trunk, make the second value '11' instead of just '1'
            new Rule('0', "11[-0]+0") //Add more 1's to the front to increase branch size
        });

        pop = ']';
        push = '[';
        line = '1';
        endLine = '0';
        translate = ' ';
        rotateCW = '+';
        rotateACW = '-';
    }

    void GenerateLSystem(int generations)
    {
        treeString = lSystem.Run(generations).ToCharArray();
    }

    private Stack<Vector3> locationStore = new Stack<Vector3>();
    private Stack<Vector3> forwardStore = new Stack<Vector3>();
    public GameObject Draw(Vector3 position)
    {
        if (tree) Destroy(tree);
        tree = new GameObject("TreeModel");
        branches = new List<GameObject>();
        Vector3 currentPosition = position;
        Vector3 currentForward = Vector3.up;
        for (int i = 0; i < treeString.Length; i++)
        {
            char current = treeString[i];
            if (current == line)
            {
                branches.Add(CreateLimb(currentPosition, currentForward, branchGO));
                Translate(ref currentPosition, currentForward);
            }
            if (current == endLine)
            {
                branches.Add(CreateLimb(currentPosition, currentForward, leafGO));
                Translate(ref currentPosition, currentForward);
            }
            if (current == translate)
            {
                Translate(ref currentPosition, currentForward);
            }
            if (current == rotateCW)
            {
                Rotate(ref currentForward, Random.Range(minAngle, maxAngle));
            }
            if (current == rotateACW)
            {
                Rotate(ref currentForward, Random.Range(-minAngle, -maxAngle));
            }
            if (current == push)
            {
                locationStore.Push(currentPosition);
                forwardStore.Push(currentForward);
            }
            else if (current == pop)
            {
                currentPosition = locationStore.Pop();
                currentForward = forwardStore.Pop();
            }
        }
        return tree;
    }

    GameObject CreateLimb(Vector3 position, Vector3 forward, GameObject prefab)
    {
        GameObject newGO = Instantiate(prefab, position + forward*prefab.GetComponentInChildren<Renderer>().bounds.size.y, Quaternion.LookRotation(forward, Vector3.up), tree.transform);
        return newGO;
    }

    void Translate(ref Vector3 position, Vector3 forward)
    {
        position += forward * moveAmount;
    }

    void Rotate(ref Vector3 forward, float angle)
    {
        forward = Quaternion.AngleAxis(angle, Vector3.forward) * forward;
        //forward = Quaternion.AngleAxis(angle, Vector3.right) * forward;
    }
}
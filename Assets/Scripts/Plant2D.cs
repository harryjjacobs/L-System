using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant2D : MonoBehaviour
{
  public LSystem LSystem;
  public int Iterations = 7;
  public char PopCharacter;
  public char PushCharacter;
  public char LineCharacter;
  public char EndLineCharacter;
  public char TranslateCharacter;
  public char RotateCWCharacter;
  public char RotateACWCharacter;
  public Material LineMat;
  public Material EndLineMat;
  public float LineWidth;
  public float MaxAngle;
  public float MinAngle;
  public float MoveAmount;

  private char[] treeString;
  private List<GameObject> branches;
  private GameObject tree;

  void Start()
  {
    GenerateLSystem(Iterations);
    Draw2D(Vector3.zero);
  }

  public void GenerateLSystem(int generations)
  {
    treeString = LSystem.Run(generations).ToCharArray();
  }

  private Stack<Vector3> locationStore = new Stack<Vector3>();
  private Stack<Vector3> forwardStore = new Stack<Vector3>();
  public GameObject Draw2D(Vector3 position)
  {
    if (tree) Destroy(tree);
    tree = new GameObject("TreeModel");

    branches = new List<GameObject>();

    Vector3 currentPosition = position;
    Vector3 currentForward = Vector3.up;
    for (int i = 0; i < treeString.Length; i++)
    {
      char current = treeString[i];
      if (current == LineCharacter)
      {
        branches.Add(CreateLine(currentPosition, currentForward, LineMat));
        Translate(ref currentPosition, currentForward);
      }
      if (current == EndLineCharacter)
      {
        branches.Add(CreateLine(currentPosition, currentForward, EndLineMat));
        Translate(ref currentPosition, currentForward);
      }
      if (current == TranslateCharacter)
      {
        Translate(ref currentPosition, currentForward);
      }
      if (current == RotateCWCharacter)
      {
        Rotate2D(ref currentForward, Random.Range(MinAngle, MaxAngle));
      }
      if (current == RotateACWCharacter)
      {
        Rotate2D(ref currentForward, Random.Range(-MinAngle, -MaxAngle));
      }
      if (current == PushCharacter)
      {
        locationStore.Push(currentPosition);
        forwardStore.Push(currentForward);
      }
      else if (current == PopCharacter)
      {
        currentPosition = locationStore.Pop();
        currentForward = forwardStore.Pop();
      }
    }
    return tree;
  }

  GameObject CreateLine(Vector3 position, Vector3 forward, Material mat)
  {
    GameObject newGO = new GameObject("Line");
    newGO.transform.parent = tree.transform;
    newGO.transform.position = position;
    LineRenderer lr = newGO.AddComponent<LineRenderer>();
    lr.SetPositions(new Vector3[] { position, position + forward * MoveAmount });
    lr.startWidth = LineWidth;
    lr.endWidth = LineWidth;
    lr.material = mat;
    return newGO;
  }

  void Translate(ref Vector3 position, Vector3 forward)
  {
    position += forward * MoveAmount;
  }

  void Rotate2D(ref Vector3 forward, float angle)
  {
    forward = Quaternion.AngleAxis(angle, Vector3.forward) * forward;
  }
}

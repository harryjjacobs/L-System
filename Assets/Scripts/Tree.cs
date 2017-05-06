using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {

    public LSystem lSystem;
    public char pop;
    public char push;
    public char line;
    public char translate;
    public char rotateRight;
    public char rotateLeft;

    public float moveAmount;

    private char[] output;

	void Start () {
        GenerateLSystem(5);
        Draw(Vector3.zero);
    }
	
    void GenerateLSystem(int generations)
    {
        output = lSystem.Run(generations).ToCharArray();
    }


    private Stack<Vector3> locationStore = new Stack<Vector3>();
    private Stack<Vector3> forwardStore = new Stack<Vector3>();
    void Draw(Vector3 position)
    {
        Vector3 currentPosition = position;
        Vector3 currentForward = Vector3.up;
        for (int i = 0; i < output.Length; i++)
        {
            char current = output[i];
            if (current == line)
            {
                Line(currentPosition, currentForward);
                Translate(ref currentPosition, currentForward);
            }
            else if (current == translate)
            {
                Translate(ref currentPosition, currentForward);
            }
            else if (current == rotateRight)
            {
                Rotate(ref currentForward, 30);
            }
            else if (current == rotateLeft)
            {
                Rotate(ref currentForward, -30);
            }
            else if (current == push)
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
    }

    void Line(Vector3 position, Vector3 forward)
    {
        GameObject newGO = new GameObject("Line");
        newGO.transform.position = position;
        LineRenderer lr = newGO.AddComponent<LineRenderer>();
        lr.SetPositions(new Vector3[] { position, position + forward * moveAmount });
        lr.startWidth = 1f;
        lr.endWidth = 1f;
    }

    void Translate(ref Vector3 position, Vector3 forward)
    {
        position += forward * moveAmount;
    }

    void Rotate(ref Vector3 forward, float angle)
    {
        forward = Quaternion.AngleAxis(angle, Vector3.forward) * forward;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack<T> {

    List<T> stack;

    public Stack() {
        stack = new List<T>();
    }

    public T Pop()
    {
        if (stack.Count == 0)
        {
            Debug.LogError("Cannot pop from the stack as nothing has been added yet");
            return default(T);
        }
        T item = stack[stack.Count - 1];
        stack.RemoveAt(stack.Count - 1);
        return item;
    }

    public void Push(T item)
    {
        stack.Add(item);
    }

}

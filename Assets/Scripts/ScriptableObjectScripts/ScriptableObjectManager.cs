using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class ScriptableObjectManager<T> : MonoBehaviour where T : NamedScriptableObject
{
    public int Count { get; private set; } = 0;

    [SerializeField] private List<T> editorElements;
    private Dictionary<string, T> elements;
    
    //Converts serialisable list from editor into dictionary of instantiated elements
    protected void InitializeObjects()
    {
        if(elements == null) 
            elements = new Dictionary<string, T>();
        if (elements.Count > 0)
            CleanObjects();

        for (int i = 0; i < editorElements.Count; i++)
        {
            T element = editorElements[i];
            AddElement(element);
        }
        editorElements.Clear();
    }

    //Instantiates and returns the reference to an instance of the element passed
    private T InstantiateElement(T element)
    {
        /*Instantiating a clone of the element allows multiple instances 
         *of the same scriptable object in the scene */
        element = Instantiate(element);
        element.Initialize(gameObject);
        return element;
    }

    //Member functions for editing dictionary
    protected bool AddElement(T element)
    {
        if (!elements.Keys.Contains(element.aName))
        {
            element = InstantiateElement(element);
            elements.Add(element.aName, element);
            Count++;
            return true;
        }
        else
        {
            Debug.LogWarning(string.Format("Duplicate element on {0}", gameObject.name));
            return false;
        }
    }
    protected bool RemoveElement(string element)
    {
        if (!elements.Keys.Contains(element))
        {
            Destroy(elements[element]);
            elements.Remove(element);
            Count--;
            return true;
        }
        else
        {
            Debug.LogWarning(string.Format("Element could not be removed as it does not exsist", gameObject.name));
            return false;
        }
    }


    //Member functions for accessing dictionary
    protected T GetElement(string name)
    {
        return elements[name];
    }
    protected T GetElement(int index)
    {
        if (index + 1 > Count) 
        {
            
            Debug.LogError(string.Format("Unit has no {0}", typeof(T).Name));
            return null;
        } 
        return elements[elements.Keys.ToList()[index]];
    }

    protected bool IsAnElement(string name)
    {
        return elements.Keys.Contains(name);
    }
    public void CleanObjects()
    {
        foreach(var key in elements.Keys)
        {
            Destroy(elements[key]);
            elements.Remove(key);
        }
    }
}

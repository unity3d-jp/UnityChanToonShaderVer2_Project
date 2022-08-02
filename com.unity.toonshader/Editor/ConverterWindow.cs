using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
public class ConverterWindow : EditorWindow
{
    public VisualTreeAsset visualTreeTemplate;

    protected void OnEnable()
    {
        visualTreeTemplate.CloneTree(rootVisualElement);
    }
}

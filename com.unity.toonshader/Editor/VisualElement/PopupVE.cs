using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace UnityEditor.Rendering.Toon
{
    internal class PopupVE : PopupField<string>
    {
        public new class UxmlFactory : UxmlFactory<PopupVE> 
        {
        }
    }
}
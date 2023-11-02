using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

#if UNITY_EDITOR
using Sirenix.Utilities.Editor;
using UnityEditor;
#endif

namespace CombatSystem.Utilities
{
    public abstract class ActionList<Data, Behavior> : ScriptableObject 
        where Data : ActionListComponent<Behavior>
        where Behavior : IActionListBehavior
    {
        [Space(10f), InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Hidden)]
        [ListDrawerSettings(OnBeginListElementGUI = "OnBeginDrawElement", OnEndListElementGUI = "OnEndDrawElement",
            CustomRemoveIndexFunction = "OnElementRemoved", HideAddButton = true, Expanded = true)]
        public List<Data> components;

#if UNITY_EDITOR

        [BoxGroup("Add Component"), ValueDropdown(nameof(GetAvailableTypes))]
        [InlineButton(nameof(AddComponent), "Add"), LabelWidth(50f)]
        public string type;

        private IEnumerable GetAvailableTypes()
        {
            var typeNames = new ValueDropdownList<string>();
            var types = GetTypesInheriting(typeof(Data));

            foreach (var type in types)
            {
                var name = ObjectNames.NicifyVariableName(type.Name);
                typeNames.Add(name);
            }

            if (string.IsNullOrEmpty(type) && types.Count > 0)
                type = typeNames[0].Value;

            return typeNames;
        }

        private void AddComponent()
        {
            var types = GetTypesInheriting(typeof(Data));
            var typeNames = types.Select(t => ObjectNames.NicifyVariableName(t.Name)).ToArray();

            for ( var i = 0; i < typeNames.Length; i++ )
            {
                if (typeNames[i] == type)
                {
                    var component = ScriptableObject.CreateInstance(types[i]) as Data;
                    component.name = type;
                    components.Add(component);

                    AssetDatabase.AddObjectToAsset(component, this);
                    AssetDatabase.SaveAssets();
                }
            }
        }

        private List<Type> GetTypesInheriting(Type t)
        {
            var types = from assembly in AppDomain.CurrentDomain.GetAssemblies()
                        from type in assembly.GetTypes()
                        where t.IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract
                        select type;

            return types.ToList();
        }

        private void OnElementRemoved(int index)
        {
            var obj = components[index];
            components.Remove(obj);
            AssetDatabase.RemoveObjectFromAsset(obj);
            AssetDatabase.SaveAssets();
        }

        private void OnBeginDrawElement(int index)
        {
            var name = ObjectNames.NicifyVariableName(components[index].GetType().Name);
            name += " " + components[index].CustomName;

            SirenixEditorGUI.BeginBox(name);
        }

        private void OnEndDrawElement(int index)
        {
            SirenixEditorGUI.EndBox();
        }
#endif
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Krem.AppCore.Attributes;
using Krem.AppCore.Interfaces;
using Krem.Core.Icons;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Krem.AppCore.EntityGraph.Views
{
    public class SearchItem
    {
        public string Name { get; }
        public string AssemblyQualifiedName { get; }
        public string GroupName { get; }

        public SearchItem(string itemName, string assemblyQualifiedName, string groupName)
        {
            this.Name = itemName;
            this.AssemblyQualifiedName = assemblyQualifiedName;
            this.GroupName = groupName;
        }
    }
    
    public class NodeSearchWindow : ScriptableObject, ISearchWindowProvider
    {
        private Action<Type> _createDelegate;

        public void Init(Action<Type> createDelegate)
        {
            _createDelegate = createDelegate;
        }
        
        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            var tree = new List<SearchTreeEntry>
            {
                new SearchTreeGroupEntry(new GUIContent("Search"), 0)
            };
            
            var actionIcon = GetTex("Action icon");
            var componentIcon = GetTex("Component icon");

            tree.AddRange(GetTreeOfNodeType<CoreAction>("Actions", actionIcon));
            tree.AddRange(GetTreeOfNodeType<CoreComponent>("Components", componentIcon));

            return tree;
        }

        private static IEnumerable<SearchTreeEntry> GetTreeOfNodeType<T>(string typeName, Texture icon = null) where T : ICoreNode
        {
            var tree = new List<SearchTreeEntry> {new SearchTreeGroupEntry(new GUIContent(typeName), 1)};
            var actionTypes = TypeCache.GetTypesDerivedFrom<T>();
            
            var actionGroups = new SortedSet<string>();
            var actionSearchItems = new List<SearchItem>();
            
            foreach (var type in actionTypes)
            {
                var groupName = "...";
                if (type.GetCustomAttribute<NodeGraphGroupNameAttribute>() != null)
                    groupName = type.GetCustomAttribute<NodeGraphGroupNameAttribute>().GroupName;
                
                if (type.GetCustomAttribute<NodeGraphHiddenAttribute>() != null || type.IsAbstract)
                {
                    continue;
                }
                
                actionSearchItems.Add(new SearchItem(type.Name, type.AssemblyQualifiedName, groupName));
                actionGroups.Add(groupName);
            }

            foreach (var groupName in actionGroups)
            {
                const int startLevel = 1;
                var levels = groupName.Count(i => i == '/');
                
                for (var i = 1; i <= levels + 1; i++)
                {
                    var treeEntry =
                        new SearchTreeGroupEntry(new GUIContent(groupName.Split('/')[i - 1]), startLevel + i);

                    if(tree.Exists(p => p.content.text == treeEntry.content.text))
                        continue;
                    
                    tree.Add(treeEntry);
                }
                
                actionSearchItems.FindAll(i => i.GroupName == groupName).ForEach((item) =>
                {
                    tree.Add(new SearchTreeEntry(new GUIContent($"{item.Name}", icon))
                    {
                        level = startLevel + levels + 2,
                        userData = item.AssemblyQualifiedName
                    });
                });
            }
            
            return tree;
        }

        public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
        {
            var typeName = (string)searchTreeEntry.userData;
            _createDelegate.Invoke(Type.GetType(typeName));
            return true;
        }
        
        private static Texture2D GetTex(string name)
        {
            return (Texture2D)Resources.Load("Icons/" + name);
        }
    }
}
﻿using Scriptables;

namespace EventArgs
{
    public class ComponentAddedEventArgs : System.EventArgs
    {
        public ComponentAddedEventArgs(ComponentData componentData)
        {
            this.ComponentData = componentData;
        }
        
        public ComponentData ComponentData { get; }
    }
}
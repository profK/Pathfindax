﻿using Duality;
using Pathfindax.Nodes;

namespace Pathfindax.PathfindEngine
{
    public interface ICompletedPath
    {
	    Vector2[] Path { get; }
        ISourceNode[] NodePath { get; }
    }
}

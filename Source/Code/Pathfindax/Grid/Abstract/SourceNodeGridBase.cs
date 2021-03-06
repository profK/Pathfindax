﻿using System;
using System.Collections;
using System.Collections.Generic;
using Duality;
using Pathfindax.Collections;
using Pathfindax.Nodes;
using Pathfindax.Utils;

namespace Pathfindax.Grid
{
    public abstract class SourceNodeGridBase<TNode> : ISourceNodeGrid<TNode>
        where TNode : ISourceGridNode
    {
        public TNode this[int index] => NodeArray[index];
        public IReadOnlyArray2D<TNode> NodeArray { get; protected set; }
        public Vector2 WorldSize { get; protected set; }
        public Vector2 NodeSize { get; protected set; }
        public int NodeCount => NodeArray.Length;
        public Vector2 Offset { get; protected set; }

        protected SourceNodeGridBase(IReadOnlyArray2D<TNode> grid, Vector2 nodeSize, Vector2 offset)
        {
            NodeArray = grid;
            WorldSize = new Vector2(NodeArray.Width * nodeSize.X - nodeSize.X, NodeArray.Height * nodeSize.Y - nodeSize.Y);
            NodeSize = nodeSize;
            Offset = offset;
        }

        protected SourceNodeGridBase() { }

        public TNode GetNode(float worldX, float worldY)
        {
            var percentX = (worldX - Offset.X) / WorldSize.X;
            var percentY = (worldY - Offset.Y) / WorldSize.Y;
            percentX = Mathf.Clamp(percentX, 0, 1);
            percentY = Mathf.Clamp(percentY, 0, 1);

            var x = (int)Math.Round((NodeArray.Width - 1) * percentX);
            var y = (int)Math.Round((NodeArray.Height - 1) * percentY);

            return NodeArray[x, y];
        }

	    public IEnumerator<TNode> GetEnumerator()
	    {
		    foreach (var gridNode in NodeArray)
		    {
			    yield return gridNode;
		    }
	    }

	    IEnumerator IEnumerable.GetEnumerator()
	    {
		    return GetEnumerator();
	    }
	}
}
﻿using Duality;
using Pathfindax.Collections;
using Pathfindax.Nodes;

namespace Pathfindax.Grid
{
	/// <summary>
	/// Generic interface for sourcenodegrids
	/// </summary>
	/// <typeparam name="TNode"></typeparam>
	public interface ISourceNodeGrid<out TNode> : ISourceNodeNetwork<TNode>, ISourceNodeGrid
		where TNode : ISourceGridNode
	{
		IReadOnlyArray2D<TNode> NodeArray { get; }
	}

	/// <summary>
	/// Interface for sourcenodegrids
	/// </summary>
	public interface ISourceNodeGrid : ISourceNodeNetwork
	{
		Vector2 NodeSize { get; }

		Vector2 WorldSize { get; }
	}
}
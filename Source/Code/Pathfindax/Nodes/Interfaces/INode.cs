﻿using System.Collections.Generic;
using Pathfindax.Primitives;

namespace Pathfindax.Nodes
{
	public interface INode<T> : INode
		where T : INode
	{
		IList<T> Neighbours { get; set; }
	}

	public interface INode
	{
		INode Parent { get; set; }
		bool Walkable { get; set; }

		/// <summary>
		/// The local world position in the grid.
		/// </summary>
		PositionF Position { get; }
	}
}
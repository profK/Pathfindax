﻿using Duality.Editor;
using Pathfindax.Algorithms;
using Pathfindax.Factories;
using Pathfindax.Grid;
using Pathfindax.Nodes;
using Pathfindax.Utils;

namespace Duality.Plugins.Pathfindax.Components
{
	/// <summary>
	/// Provides a way for other components to request a path from A to B. Uses the A* algorithm.
	/// </summary>
	[EditorHintCategory(PathfindaxStrings.Pathfindax)]
	[RequiredComponent(typeof(ISourceNodeNetworkProvider<ISourceNodeNetwork<SourceNode>>))]
	public class AstarPathfinderComponent : PathfinderComponentBase<ISourceNodeNetwork<SourceNode>>
	{
		/// <inheritdoc />
		public override void OnInit(InitContext context)
		{
			if (context == InitContext.Activate && DualityApp.ExecContext == DualityApp.ExecutionContext.Game)
			{
				var sourceNodeNetwork = GetSourceNodeNetwork();
				if (sourceNodeNetwork == null) return;
				Pathfinder = PathfinderFactory.CreatePathfinder(sourceNodeNetwork, s =>
				{
					var astarNodeNetwork = new AstarNodeNetwork(s);
					var algorithm = new AStarAlgorithm();
					return PathfinderFactory.CreateRequestProcesser(astarNodeNetwork, algorithm);
				});
				Pathfinder.Start();
			}
		}
	}
}
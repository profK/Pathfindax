﻿using Duality.Components;
using Duality.Editor;
using Duality.Input;
using Duality.Plugins.Pathfindax.Components;
using Duality.Plugins.Pathfindax.PathfindEngine;
using Pathfindax.Nodes;
using Pathfindax.PathfindEngine;
using Pathfindax.Utils;

namespace Duality.Plugins.Pathfindax.Examples.Components
{
	[EditorHintCategory(PathfindaxStrings.PathfindaxTest)]
	public class NonGridMouseClickPathfinder : Component, ICmpInitializable, IPathProvider
	{
		/// <summary>
		/// The size of the agent in nodes. Bigger agents won't fit in nodes whose clearance is smaller than this.
		/// </summary>
		[EditorHintRange(1, byte.MaxValue)]
		public byte AgentSize { get; set; }

		/// <summary>
		/// What collision categories will the agent collide with? These categories will be avoided when calculating a path.
		/// </summary>
		public PathfindaxCollisionCategory CollisionCategory { get; set; }

		/// <summary>
		/// The currently calculated path
		/// </summary>
		public Vector2[] Path { get; private set; }

		/// <summary>
		/// A reference to the <see cref="Duality.Components.Camera"/> thats used to convert the screen coordinates from mouseclicks to world coordinates.
		/// </summary>
		public Camera Camera { get; set; }

		private Vector3? _pathStart;
		private NonGridPathfinderProxy _nonGridPathfinderProxy;

		void ICmpInitializable.OnInit(InitContext context)
		{
			if (context == InitContext.Activate && DualityApp.ExecContext == DualityApp.ExecutionContext.Game)
			{
				DualityApp.Mouse.Move += Mouse_Move;
				DualityApp.Mouse.ButtonDown += Mouse_ButtonDown;
				_nonGridPathfinderProxy = new NonGridPathfinderProxy();
			}
		}

		void ICmpInitializable.OnShutdown(ShutdownContext context)
		{
			DualityApp.Mouse.ButtonDown -= Mouse_ButtonDown;
			DualityApp.Mouse.Move -= Mouse_Move;
		}

		private void PathSolved(PathRequest pathRequest)
		{
			Path = pathRequest.CompletedPath.Path;
		}

		private void Mouse_ButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (_pathStart == null)
			{
				_pathStart = Camera.GetSpaceCoord(e.Position);
			}
			else
			{
				Path = null;
				_pathStart = null;
			}
		}

		private void Mouse_Move(object sender, MouseMoveEventArgs e)
		{
			if (_pathStart != null)
			{
				var mouseWorldPosition = Camera.GetSpaceCoord(e.Position);
				var request = _nonGridPathfinderProxy.RequestPath(_pathStart.Value, mouseWorldPosition, CollisionCategory, AgentSize);
				request.AddCallback(PathSolved);
			}
		}
	}
}

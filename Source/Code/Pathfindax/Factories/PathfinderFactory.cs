﻿using System;
using System.Collections.Generic;
using Pathfindax.Algorithms;
using Pathfindax.Grid;
using Pathfindax.PathfindEngine;

namespace Pathfindax.Factories
{
    public static class PathfinderFactory
    {
        public static Pathfinder<TSourceNodeNetwork, TNodeNetwork> CreatePathfinder<TSourceNodeNetwork, TNodeNetwork>(TSourceNodeNetwork sourceNodeNetwork, Func<TSourceNodeNetwork, PathRequestProcesser<TNodeNetwork>> processerConstructor, int threads = 1)
            where TSourceNodeNetwork : ISourceNodeNetwork
			where TNodeNetwork : INodeNetwork
        {
            return new Pathfinder<TSourceNodeNetwork, TNodeNetwork>(sourceNodeNetwork, processerConstructor, threads);
        }

		public static PathRequestProcesser<TNodeNetwork> CreateRequestProcesser<TNodeNetwork>(TNodeNetwork nodeNetwork, IPathFindAlgorithm<TNodeNetwork> pathFindAlgorithm, IList<IPathPostProcess> pathPostProcesses = null)
		    where TNodeNetwork : INodeNetwork
	    {
		    return new PathRequestProcesser<TNodeNetwork>(nodeNetwork, pathFindAlgorithm, pathPostProcesses);
	    }
	}
}

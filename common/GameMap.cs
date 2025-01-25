using Godot;
using Godot.Collections;

[GlobalClass]
public partial class GameMap : TileMapLayer {
	private AStarGrid2D astar;

	public override void _Ready() {
		astar = new AStarGrid2D();
		astar.DefaultComputeHeuristic = AStarGrid2D.Heuristic.Manhattan;
		astar.DefaultEstimateHeuristic = AStarGrid2D.Heuristic.Manhattan;
		astar.DiagonalMode = AStarGrid2D.DiagonalModeEnum.OnlyIfNoObstacles;
		astar.Offset = new Vector2(20,20);
		astar.CellSize = new Vector2(32,32);
		astar.Region = GetUsedRect();
		astar.Update();

		for (int x = 0; x < GetUsedRect().Size.X; x++) {
			for (int y = 0; y < GetUsedRect().Size.Y; y++) {
				Vector2I tilePos = new Vector2I(x + GetUsedRect().Position.X, y + GetUsedRect().Position.Y);
				TileData tileData = GetCellTileData(tilePos);
				if (tileData == null || (bool)tileData.GetCustomData("isSolid")) {
					astar.SetPointSolid(new Vector2I(x,y), true);
				}
			}
		}
	 }

	public Array<Vector2> GetPathToPoint(Vector2 start, Vector2 end) {
		Vector2I fromID = LocalToMap(start) - GetUsedRect().Position;
		Vector2I toID = LocalToMap(end) - GetUsedRect().Position;
		Array<Vector2> path = new Array<Vector2>(astar.GetPointPath(fromID, toID));
		if (path.Count == 0) return path;
		path.RemoveAt(0);
		return path;
	}
}

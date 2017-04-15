using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap_Location_Script : MonoBehaviour {

	// upper left coords (120 & bway): 40.810489, -73.961964 
	// upper right coords (120 & morningside): 40.810489, -73.957637
	// lower left coords (114 & bway): 40.806677, -73.964718
	// lower right coords (114 & morningside): 40.804711, -73.9601014
	private long[] UPPER_LEFT_COORDS = new long[]{ (long)40.810497, (long)-73.961964 };
	private long[] UPPER_RIGHT_COORDS = new long[]{ (long)40.808666, (long)-73.957640 };
	private long[] LOWER_LEFT_COORDS = new long[]{ (long)40.806676, (long)-73.964743 };
	private long[] LOWER_RIGHT_COORDS = new long[]{ (long)40.804743, (long)-73.960183 };

	long[][][] coordinates_grid;
	private int LAT_IDX = 0;
	private int LONG_IDX = 0;

	void Start () {
		initCoordinateGrid ();
	}
	
	private void initCoordinateGrid() {
		// initialize the grid
		coordinates_grid = new long[50][][];

		/* iterate over the grid and use an
		 * interpolation scheme to fill
		 * the grid in with coordinates
		*/
		for (int latitude_iter = 0; latitude_iter < 50; latitude_iter++) {
			long[][] row = new long[50][];
			for (int longitude_iter = 0; longitude_iter < 50; longitude_iter++) {
				long[] coords = new long[2];
				// check for the known locations
				if (latitude_iter == 0 && longitude_iter == 0) { // upper left corner
					coords [LAT_IDX] = UPPER_LEFT_COORDS [LAT_IDX];
					coords [LONG_IDX] = UPPER_LEFT_COORDS [LONG_IDX];
				} else if (latitude_iter == 0 && longitude_iter == 49) { // upper right corner
					coords [LAT_IDX] = UPPER_RIGHT_COORDS [LAT_IDX];
					coords [LONG_IDX] = UPPER_RIGHT_COORDS [LONG_IDX];
				} else if (latitude_iter == 49 && longitude_iter == 0) { // lower left corner
					coords[LAT_IDX] = LOWER_LEFT_COORDS[LAT_IDX];
					coords [LONG_IDX] = LOWER_LEFT_COORDS [LONG_IDX];
				} else if (latitude_iter == 49 && longitude_iter == 49) { // lower right corner
					coords[LAT_IDX] = LOWER_RIGHT_COORDS[LAT_IDX];
					coords [LONG_IDX] = LOWER_RIGHT_COORDS [LONG_IDX];
				} else { // use interpolation method

				}

				row[longitude_iter] = coords;
			}
			coordinates_grid [latitude_iter] = row;
		}
	}

	/*
	 * Given coordinates, find the cell in the coordinate that contains coordinates closest to
	 * the params
	 */ 
	private int[] getClosestCellToCoords(long latitude, long longitude) {
		int[] closestCell = new int[2];

		if (latitude != null && longitude != null) {


		}

		return closestCell;
	}
}
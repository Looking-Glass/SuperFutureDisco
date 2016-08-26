using System.Collections;

/**********************************************
 * 
 * Events for ButterflyGame
 * 
 * *******************************************/

public class TestEvent : GameEvent {
    public int b;

    public TestEvent(int bb) {
        b = bb;
    }
}

public class PlayerHitObstacleEvent : GameEvent {
	public int playerColor;
	public int obstacleColor;
	public float obstacleX;
	public float obstacleY;
	public Obstacle obstacle;

	public PlayerHitObstacleEvent(int pc, int oc, float ox, float oy, Obstacle o){
		playerColor = pc;
		obstacleColor = oc;
		obstacleX = ox;
		obstacleY = oy;
		obstacle = o;
	}



}


public class ObstacleMissedEvent : GameEvent {

	public Obstacle obstacle;
	public float obstacleX;
	public float obstacleY;
	public ObstacleMissedEvent(Obstacle o){
		obstacle = o;
		obstacleX = obstacle.transform.position.x;
		obstacleY = obstacle.transform.position.y;
	}
}

public class TryStartPartyEvent : GameEvent {
	public TryStartPartyEvent(){}
}

public class StartSongEvent : GameEvent {
	public StartSongEvent(){}
}

public class BeatEvent : GameEvent {
	public BeatEvent(){}
}
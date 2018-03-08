using UnityEngine;
using System.Collections;
using System;
using Colyseus;
using MsgPack;

public class Network : MonoBehaviour {

	Client client;
	Room room;
	public string serverName = "localhost";
	public string port = "2657";
	public string roomName = "game";

	IEnumerator Start () {
		String uri = "ws://" + serverName + ":" + port;
		client = new Client(uri);
		client.OnOpen += OnOpen;
		client.OnMessage += OnMessage;
		yield return StartCoroutine(client.Connect());

		room = client.Join(roomName);
		room.OnJoin += OnRoomJoined;
		room.OnUpdate += OnUpdate;
		room.OnData += OnRoomData;

		room.state.Listen ("players/:id", "add", OnAddPlayer);
		room.state.Listen ("players/:id", "remove", OnPlayerRemoved);
		room.state.Listen (OnChangeFallback);

		while (true) {
			client.Recv ();

			if (client.error != null) {
				Debug.LogError ("Error: " + client.error);
				break;
			}

			yield return 0;
		}

		OnApplicationQuit ();
	}

	void OnOpen (object sender, EventArgs e) {
//		Debug.Log("Connected to server. Client id: " + client.id);
	}


	void OnRoomJoined (object sender, EventArgs e) {
//		Debug.Log("Joined room successfully.");
//		room.Send (new object[]{ "one", "two", 3 });
	}

	void OnRoomData (object sender, MessageEventArgs e) {
		Debug.Log("OnRoomData");
		Debug.Log (e.data);
	}

	void OnMessage (object sender, MessageEventArgs e) {
		Debug.Log ("OnMessage");
		Debug.Log (e.data);
	}

	void OnAddPlayer (string[] path, MessagePackObject value) {
		Debug.Log ("OnAddPlayer");
		Debug.Log (path[0]);
		Debug.Log (value);
	}

	void OnPlayerRemoved (string[] path, MessagePackObject value) {
		Debug.Log ("OnPlayerRemoved");
		Debug.Log (value);
	}

	void OnChangeFallback (string[] path, string operation, MessagePackObject value) {
		Debug.Log ("OnChangeFallback: " + operation + " : " + path[0]);
		Debug.Log (value);
	}

	void OnUpdate (object sender, RoomUpdateEventArgs e) {
		
	}

	void OnApplicationQuit() {
		// Ensure the connection with server is closed immediatelly
		client.Close();
	}

}
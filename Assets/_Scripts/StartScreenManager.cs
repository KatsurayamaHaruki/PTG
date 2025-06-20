using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class StartScreenManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField roomIdInput;
    [SerializeField] private Button createRoomButton;
    [SerializeField] private Button joinRoomButton;

    void Start()
    {
        // Photonサーバーに接続する
        PhotonNetwork.ConnectUsingSettings();
    }

    // マスターサーバーへの接続が成功した時に呼ばれる
    public override void OnConnectedToMaster()
    {
        Debug.Log("マスターサーバーに接続しました。");
        // ボタンを押せるようにする
        createRoomButton.interactable = true;
        joinRoomButton.interactable = true;
    }

    // ルーム作成ボタンが押された時の処理
    public void OnCreateRoomButtonClicked()
    {
        // 6桁のランダムな数字をルームIDとしてルームを作成
        string roomId = Random.Range(100000, 1000000).ToString();
        PhotonNetwork.CreateRoom(roomId, new RoomOptions { MaxPlayers = 8 });
    }

    // ルーム入室ボタンが押された時の処理
    public void OnJoinRoomButtonClicked()
    {
        string roomId = roomIdInput.text;
        if (!string.IsNullOrEmpty(roomId))
        {
            PhotonNetwork.JoinRoom(roomId);
        }
    }
    
    // ルームへの参加が成功した時に呼ばれる（作成・入室どちらも）
    public override void OnJoinedRoom()
    {
        Debug.Log("ルームに参加しました。ルーム名: " + PhotonNetwork.CurrentRoom.Name);
        // ルーム待機画面に遷移
        PhotonNetwork.LoadLevel("RoomLobbyScreen");
    }

    // ルームへの参加が失敗した時に呼ばれる
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError($"ルームへの参加に失敗しました: {message}");
    }
}
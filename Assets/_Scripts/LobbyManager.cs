using UnityEngine;
using Photon.Pun;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private GameObject startButton; // スタートボタン

    void Start()
    {
        // プレイヤーを生成する
        PhotonNetwork.Instantiate("PlayerAvatar", new Vector3(0, 0, 0), Quaternion.identity);

        // 自分が見たクライアント（部屋の作成者）である場合のみ、スタートボタンを表示する
        startButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public void OnStartButtonClicked()
    {
        // マスタークライアントでなければ処理しない
        if (!PhotonNetwork.IsMasterClient) return;

        // 全員の画面をメインゲームへ遷移させる
        PhotonNetwork.LoadLevel("MainGameScreen");
    }
}
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon; // Custom Properties を使うために必要

public class PlayerHUD : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI roleText;
    [SerializeField] private TextMeshProUGUI moneyText;

    void Start()
    {
        // ゲーム開始時に役職と所持金を設定する（例）
        if (PhotonNetwork.IsMasterClient) // マスタークライアントが全員の初期値を決める
        {
            // 本来は全プレイヤーに対してループ処理で設定する
            var initialProperties = new Hashtable
            {
                { "Role", "怪盗" },
                { "Money", 1000 }
            };
            PhotonNetwork.LocalPlayer.SetCustomProperties(initialProperties);
        }
        UpdateUI(PhotonNetwork.LocalPlayer);
    }

    // プレイヤーのプロパティが更新された時に呼ばれる
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        // 更新されたのが自分のプロパティならUIを更新
        if (targetPlayer.IsLocal)
        {
            UpdateUI(targetPlayer);
        }
    }

    void UpdateUI(Player player)
    {
        // Custom Properties から値を取得してUIに表示
        object roleValue;
        if (player.CustomProperties.TryGetValue("Role", out roleValue))
        {
            roleText.text = $"役職: {roleValue}";
        }

        object moneyValue;
        if (player.CustomProperties.TryGetValue("Money", out moneyValue))
        {
            moneyText.text = $"所持金: {moneyValue:N0}"; // 3桁区切りで表示
        }
    }
}
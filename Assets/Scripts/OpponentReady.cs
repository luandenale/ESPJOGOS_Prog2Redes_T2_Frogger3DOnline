using UnityEngine.Networking;

public class OpponentReady : NetworkBehaviour
{
    public void SetAsReady()
    {
        CmdOpponentReady();
    }

    [Command]
    private void CmdOpponentReady()
    {
        GameManager.localPlayerReady = true;
        RpcOpponentReady();
    }
    [ClientRpc]
    private void RpcOpponentReady()
    {
        if(!isLocalPlayer)
            GameManager.opponentReady = true;
    }
}

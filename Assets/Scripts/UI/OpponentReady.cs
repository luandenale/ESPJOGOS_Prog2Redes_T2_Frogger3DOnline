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
        GameManager.instance.localPlayerReady = true;
        RpcOpponentReady();
    }
    [ClientRpc]
    private void RpcOpponentReady()
    {
        if(!isLocalPlayer)
            GameManager.instance.opponentReady = true;
    }
}

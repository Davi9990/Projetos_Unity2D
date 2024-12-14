using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Sincronizar_posicao : MonoBehaviourPun, IPunObservable
{
    private Vector3 networkPosition;

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else
        {
            networkPosition = (Vector3)stream.ReceiveNext();
        }
    }

    void Update()
    {
        if (!photonView.IsMine)
        {
            transform.position = Vector3.Lerp(transform.position, networkPosition, Time.deltaTime * 10);
        }
    }
}

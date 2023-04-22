using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    //Level1 Yellow Gate
    public bool _isDoorOpen = false;
    public bool _isDoorClose = false;

    //Level1 Tutorial Level
    public bool _isTutorialLevel = false; //Tunnel Yellow Gate close.
    public bool _isTutorialLevleYellowDoor = false;

    //Level1 Green gate
    public bool _isLevel1GreenDoorOpen = false;
    public bool _isLevel1GreenDoorClose = false;
    //Level1GreenTunnel
    public bool _isLevel1GreenTunnel = false;
    //Level1YellowTunnel
    public bool _isLevel1YellowTunnel = false;

    //-----Level2---//
    //YellowGate
    public bool _isLevel2YelllowDoorOpen = false;
    //GreenGate
    public bool _isLevel2GreenDoorOpen = false;
    public bool _isLevel2GreenDoorClose = false;

    //-----Level3---//
    //Green Gate
    public bool _isLevel3GreenDoorOpen = false;
    public bool _isLevel3GreenDoorClose = false;

    //Yellow Gate
    public bool _isLevel3YellowDoorOpen = false;
    public bool _isLevel3YellowDoorClose = false;

    //-----Level4------//
    //Green Gate
    public bool _isLevel4GreenDoorOpen = false;
    public bool _isLevel4GreenDoorClose = false;

    //Yellow Gate
    public bool _isLevel4YellowDoorOpen = false;
    public bool _isLevel4YellowDoorClose = false;

    //-----Level5------//
    public bool _isLevel5GreenDoorOpen = false;
    public bool _isLevel5YellowDoorOpen = false;

    //Open/Close sounds
    public bool _isOpenSoundPlayed = false;
    public bool _isClosedSoundPlayed = false;

    Vector3 _doorClosedPos;// Level1 Yellow Door
    Vector3 _doorLevel1GreenClosedPos;// Level1 Green Door
    Vector3 _doorTunnelHorizontalClosePos;
    Vector3 _doorOpenPos;

    Vector3 _doorTunnelOpenPos;
    Vector3 _doorTunnelClosePos;
    Vector3 _doorTunnelHorizontalOpenPos;

    float _doorSpeed = 5.4f;

    public AudioSource AudioSource;
    public AudioClip shutterOpenSound;
    public AudioClip shutterCloseSound;

    private void Awake()
    {
        _doorClosedPos = transform.position;
        _doorOpenPos = new Vector3(transform.position.x, transform.position.y + 10f, transform.position.z);

        _doorTunnelOpenPos = transform.position;
        _doorTunnelClosePos = new Vector3(transform.position.x, transform.position.y - 2f, transform.position.z);

        _doorTunnelHorizontalClosePos = transform.position;
        _doorTunnelHorizontalOpenPos = new Vector3(transform.position.x - 10f, transform.position.y, transform.position.z);

    }

    // Update is called once per frame
    private void Update()
    {
        //Debug.Log("DOOR STAT\n" + _isDoorOpen);
        //TutorialTunnel.
        if (_isTutorialLevel)
        {
            if (transform.position != _doorTunnelClosePos)
            {
                transform.position = Vector3.MoveTowards(transform.position, _doorTunnelClosePos, _doorSpeed * Time.deltaTime);
            }
            //_isDoorOpen = true;
            if (!_isClosedSoundPlayed)
            {
                AudioSource.clip = shutterCloseSound;
                AudioSource.Play();
                _isClosedSoundPlayed = true;
            }
        }
        //Tutorial Level Yellow Level.
        if (_isTutorialLevleYellowDoor)
        {
            if (transform.position != _doorTunnelHorizontalOpenPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, _doorTunnelHorizontalOpenPos, _doorSpeed * Time.deltaTime);
            }
            if (!_isOpenSoundPlayed)
            {
                AudioSource.clip = shutterOpenSound;
                AudioSource.Play();
                _isOpenSoundPlayed = true;
            }
        }

        //Level1 Yellow Gate Open
        if (_isDoorOpen)
        {
            Debug.Log("DOOR IF\n" + _isDoorOpen);
            if (transform.position != _doorOpenPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, _doorOpenPos, _doorSpeed * Time.deltaTime);
            }
            if (transform.position == _doorOpenPos)
            {
                _isDoorOpen = false;
            }
            if (!_isOpenSoundPlayed)
            {
                AudioSource.clip = shutterOpenSound;
                AudioSource.Play();
                _isOpenSoundPlayed = true;
            }
        }
        //Level 1 green gate Open
        if (_isLevel1GreenDoorOpen)
        {
            if (transform.position != _doorOpenPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, _doorOpenPos, _doorSpeed * Time.deltaTime);
            }
            if (transform.position == _doorOpenPos)
            {
                _isLevel1GreenDoorOpen = false;
            }
            if (!_isOpenSoundPlayed)
            {
                AudioSource.clip = shutterOpenSound;
                AudioSource.Play();
                _isOpenSoundPlayed = true;
            }
        }
        //Level1 yellow gate close.
        if (_isDoorClose)
        {
            //Open Door
            Debug.Log("DOOR ELSE\n" + _isDoorOpen);
            if (transform.position != _doorClosedPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, _doorClosedPos, _doorSpeed * Time.deltaTime);
            }
            //_isDoorOpen = true;
            if (!_isClosedSoundPlayed)
            {
                AudioSource.clip = shutterCloseSound;
                AudioSource.Play();
                _isClosedSoundPlayed = true;
            }
        }
        //------------TUNNELS___________//
        // GREEN TUNNEL CLOSE
        if (_isLevel1GreenTunnel)
        {
            if (transform.position != _doorTunnelClosePos)
            {
                transform.position = Vector3.MoveTowards(transform.position, _doorTunnelClosePos, _doorSpeed * Time.deltaTime);
            }
            //_isDoorOpen = true;
            if (!_isClosedSoundPlayed)
            {
                AudioSource.clip = shutterCloseSound;
                AudioSource.Play();
                _isClosedSoundPlayed = true;
            }
        }
        // YELLOW TUNNEL CLOSE
        if (_isLevel1YellowTunnel)
        {
            if (transform.position != _doorTunnelClosePos)
            {
                transform.position = Vector3.MoveTowards(transform.position, _doorTunnelClosePos, _doorSpeed * Time.deltaTime);
            }
            //_isDoorOpen = true;
            if (!_isClosedSoundPlayed)
            {
                AudioSource.clip = shutterCloseSound;
                AudioSource.Play();
                _isClosedSoundPlayed = true;
            }
        }
        //Level1 CLOSE
        if (_isLevel1GreenDoorClose)
        {
            if (transform.position != _doorClosedPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, _doorClosedPos, _doorSpeed * Time.deltaTime);
            }
            //_isDoorOpen = true;
            if (!_isClosedSoundPlayed)
            {
                AudioSource.clip = shutterCloseSound;
                AudioSource.Play();
                _isClosedSoundPlayed = true;
            }
        }

        //----------Level2---------------//
        // Green Gate OPEN
        if (_isLevel2GreenDoorOpen)
        {
            if (transform.position != _doorOpenPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, _doorOpenPos, _doorSpeed * Time.deltaTime);
            }
            Debug.Log("GATE SOUND");
            if (!_isOpenSoundPlayed)
            {
                AudioSource.clip = shutterOpenSound;
                AudioSource.Play();
                _isOpenSoundPlayed = true;
            }

        }
        //Green Gate CLOSE
        if (_isLevel2GreenDoorClose)
        {
            if (transform.position != _doorClosedPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, _doorClosedPos, _doorSpeed * Time.deltaTime);
            }
            //_isDoorOpen = true;
            if (!_isClosedSoundPlayed)
            {
                AudioSource.clip = shutterCloseSound;
                AudioSource.Play();
                _isClosedSoundPlayed = true;
            }
        }
        //YELLOW GATE OPEN
        if (_isLevel2YelllowDoorOpen)
        {
            if (transform.position != _doorOpenPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, _doorOpenPos, _doorSpeed * Time.deltaTime);
            }
            if (transform.position == _doorOpenPos)
            {
                _isLevel2YelllowDoorOpen = false;
            }
            if (!_isOpenSoundPlayed)
            {
                AudioSource.clip = shutterOpenSound;
                AudioSource.Play();
                _isOpenSoundPlayed = true;
            }
        }


        //----------Level3---------------//
        // Green Gate OPEN
        if (_isLevel3GreenDoorOpen)
        {
            if (transform.position != _doorOpenPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, _doorOpenPos, _doorSpeed * Time.deltaTime);
            }
            if (transform.position == _doorOpenPos)
            {
                _isLevel2YelllowDoorOpen = false;
            }
            if (!_isOpenSoundPlayed)
            {
                AudioSource.clip = shutterOpenSound;
                AudioSource.Play();
                _isOpenSoundPlayed = true;
            }
        }

        if (_isLevel3GreenDoorClose)
        {
            if (transform.position != _doorClosedPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, _doorClosedPos, _doorSpeed * Time.deltaTime);
            }
            //_isDoorOpen = true;
            if (!_isClosedSoundPlayed)
            {
                AudioSource.clip = shutterCloseSound;
                AudioSource.Play();
                _isClosedSoundPlayed = true;
            }
        }

        //------Level 4 ------//
        // Green Gate OPEN
        if (_isLevel4GreenDoorOpen)
        {
            if (transform.position != _doorOpenPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, _doorOpenPos, _doorSpeed * Time.deltaTime);
            }
            if (transform.position == _doorOpenPos)
            {
                _isLevel4GreenDoorOpen = false;
            }
            if (!_isOpenSoundPlayed)
            {
                AudioSource.clip = shutterOpenSound;
                AudioSource.Play();
                _isOpenSoundPlayed = true;
            }
        }

        if (_isLevel4YellowDoorOpen)
        {
            if (transform.position != _doorOpenPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, _doorOpenPos, _doorSpeed * Time.deltaTime);
            }
            if (transform.position == _doorOpenPos)
            {
                _isLevel4YellowDoorOpen = false;
            }
            if (!_isOpenSoundPlayed)
            {
                AudioSource.clip = shutterOpenSound;
                AudioSource.Play();
                _isOpenSoundPlayed = true;
            }
        }

        if (_isLevel4GreenDoorClose)
        {
            if (transform.position != _doorClosedPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, _doorClosedPos, _doorSpeed * Time.deltaTime);
            }
            //_isDoorOpen = true;
            if (!_isClosedSoundPlayed)
            {
                AudioSource.clip = shutterCloseSound;
                AudioSource.Play();
                _isClosedSoundPlayed = true;
            }
        }



        //Yellow
        if (_isLevel3YellowDoorOpen)
        {
            if (transform.position != _doorTunnelHorizontalOpenPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, _doorTunnelHorizontalOpenPos, _doorSpeed * Time.deltaTime);
            }
            if (transform.position == _doorTunnelHorizontalOpenPos)
            {
                _isLevel3YellowDoorOpen = false;
            }
            if (!_isOpenSoundPlayed)
            {
                AudioSource.clip = shutterOpenSound;
                AudioSource.Play();
                _isOpenSoundPlayed = true;
            }
        }

        if (_isLevel3YellowDoorClose)
        {
            if (transform.position != _doorClosedPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, _doorClosedPos, _doorSpeed * Time.deltaTime);
            }
            //_isDoorOpen = true;
            if (!_isClosedSoundPlayed)
            {
                AudioSource.clip = shutterCloseSound;
                AudioSource.Play();
                _isClosedSoundPlayed = true;
            }
        }

        //------Level 5 ------//
        //Green Gate
        if (_isLevel5GreenDoorOpen)
        {

            if (transform.position != _doorTunnelHorizontalOpenPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, _doorTunnelHorizontalOpenPos, _doorSpeed * Time.deltaTime);
            }
            if (!_isOpenSoundPlayed)
            {
                AudioSource.clip = shutterOpenSound;
                AudioSource.Play();
                _isOpenSoundPlayed = true;
            }
        }

        if (_isLevel5YellowDoorOpen)
        {
            if (transform.position != _doorOpenPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, _doorOpenPos, _doorSpeed * Time.deltaTime);
            }
            if (!_isOpenSoundPlayed)
            {
                AudioSource.clip = shutterOpenSound;
                AudioSource.Play();
                _isOpenSoundPlayed = true;
            }
        }

    }
}

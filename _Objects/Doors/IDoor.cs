using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDoor
{
    bool canBeOpened();
    bool canBeClosed();
    bool isOpen();
    bool isClosed();

    void Open();
    void Close();
}
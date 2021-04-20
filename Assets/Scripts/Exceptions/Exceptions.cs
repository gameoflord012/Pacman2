using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InvalidMapFormat : Exception
{
    public InvalidMapFormat() : base() { }
    public InvalidMapFormat(string message) : base(message) { }
    public InvalidMapFormat(string message, Exception inner) : base(message, inner) { }

    // A constructor is needed for serialization when an
    // exception propagates from a remoting server to the client.
    protected InvalidMapFormat(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
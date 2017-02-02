using UnityEngine;
using System.Collections;


public delegate void Callback();

public delegate void Callback<A>(A parameterOne);

public delegate void Callback<A, B>(A parameterOne, B parameterTwo);

public delegate void Callback<A, B, C>(A parameterOne, B parameterTwo, C parameterThree);

public delegate void Callback<A, B, C, D>(A parameterOne, B parameterTwo, C parameterThree, D parameterFour);
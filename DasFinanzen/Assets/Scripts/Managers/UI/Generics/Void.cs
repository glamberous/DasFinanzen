
using System;

public sealed class Void {
    Void() {
        throw new InvalidOperationException("Don't instantiate Void.");
    }
}

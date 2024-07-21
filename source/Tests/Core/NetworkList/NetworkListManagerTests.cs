// Copyright (c) Microsoft Corporation.  All rights reserved.

using Microsoft.WindowsAPICodePack.Net;
using Xunit;

namespace Tests;

public class NetworkListManagerTests
{
    [Fact]
    public void NetworkCollectionContainsAllNetworkConnections()
    {
        var isConnected = NetworkListManager.IsConnected;
        var connectivity = NetworkListManager.Connectivity;
        var isConnectedToInternet = NetworkListManager.IsConnectedToInternet;

        var networks = NetworkListManager.GetNetworks(NetworkConnectivityLevels.All);
        var connections = NetworkListManager.GetNetworkConnections();

        // BUG: Both GetNetworks and GetNetworkConnections create new network objects, so 
        // you can't do a reference comparison.
        // By inspection, the connections are contained in the NetworkCollection, just a different instance.
        foreach (var c in connections) Assert.Contains(c.Network, networks);
    }

    [Fact]
    public void IsConnectedIsConsistentWithGetNetworkConnections()
    {
        var isConnected = NetworkListManager.IsConnected;
        var moreThanZeroConnections = false;

        foreach (var c in NetworkListManager.GetNetworkConnections())
        {
            moreThanZeroConnections = true;
            break;
        }

        Assert.Equal(isConnected, moreThanZeroConnections);
    }
}

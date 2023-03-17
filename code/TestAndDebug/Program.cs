using ServerCommunication.Api;

TokenManager tokenManager = new TokenManager();

tokenManager.Write();

Console.WriteLine(tokenManager.Read());

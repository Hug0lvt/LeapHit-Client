using ServerCommunication.Api;
using ServerCommunication.Api.HttpMangers;
using ServerCommunication.Api.WriteReadFiles;


IdManager tokenManager = new IdManager();

Console.WriteLine(tokenManager.ManageId());

using ServerCommunication.Api.WriteReadFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCommunication.Api
{


    public class TokenManager
    {
        private string _token= "K02q7naLzjmodzAFfoSO4mPydr7W5hydPMrHtA6D";
        private string _fileName = "..\\..\\..\\..\\Communication\\token.bin";

        public TokenManager(){        }
        public TokenManager(string token, string fileName)
        {
            _token = token;
            _fileName = fileName;
        }

        public void Write()
        {
            FilePongManger fileManager=new FilePongManger(_token, _fileName);
            fileManager.Write();

        }

        public string Read()
        {
            FilePongManger fileManager = new FilePongManger(_token, _fileName);
            return fileManager.Read();
        }





    }

   

}

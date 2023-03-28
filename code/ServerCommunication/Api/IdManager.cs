using ServerCommunication.Api.HttpMangers;
using ServerCommunication.Api.WriteReadFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCommunication.Api
{


    public class IdManager
    {
        private string _fileName = "..\\..\\..\\..\\Communication\\id.bin";

        public string Write()
        {
            HttpClient client = new HttpClient();
            FilePongManger fileManager=new FilePongManger( _fileName);
            HttpId idProvider = new HttpId(client);
            string idUser =  idProvider.GetId().Result;
            fileManager.Write(idUser);
            return idUser;

        }

        public string ManageId()
        {
            FilePongManger fileManager = new FilePongManger( _fileName);
            string value="";
            try
            {
                value = fileManager.Read();

            }
            catch (ExceptionFileDosentExist)
            {
                using (FileStream fs = File.Create(_fileName)) {}
            }
            if (string.IsNullOrEmpty(value))
            {
                value= Write();
                
            }
            return value;
        }

    }

   

}

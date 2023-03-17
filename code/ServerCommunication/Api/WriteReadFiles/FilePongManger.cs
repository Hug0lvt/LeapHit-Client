using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCommunication.Api.WriteReadFiles
{
    public class ExceptionFileDosentExist : Exception { }

    public class FilePongManger
    {
        private string _token = "K02q7naLzjmodzAFfoSO4mPydr7W5hydPMrHtA6D";
        private string _fileName = "..\\..\\..\\..\\token.bin";

        public FilePongManger(string token, string fileName)
        {
            _token = token;
            _fileName = fileName;
        }

        public void Write()
        {
            using (var stream = File.Open(_fileName, FileMode.Create))
            {
                using (var writer = new BinaryWriter(stream, Encoding.UTF8, false))
                {
                    writer.Write(_token);

                }
            }
        }

        public string Read()
        {
            string token;
            if (File.Exists(_fileName))
            {
                using (var stream = File.Open(_fileName, FileMode.Open))
                {
                    using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                    {
                        token = reader.ReadString();
                    }
                }
                return token;
            }
            throw new ExceptionFileDosentExist();
        }





    }
}

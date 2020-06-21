using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Configuration;

namespace CodingHood
{
    public class PswdFieldManager
    {
        public static string PswdStartTag { get; set; } = "#p*";
        public static string PswdEndTag { get; set; } = "*p#";

        private static PswdFieldManager instance;
        public static PswdFieldManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PswdFieldManager();
                }
                return instance;
            }
        }

        public string SettingsEncryptinKey
        {
            get
            {
                return ConfigurationManager.AppSettings["EncKey"];
            }
        }

        private PswdFieldManager() {
        }

        public string EncryptPassWordFields(string str)
        {
            List<int> starts;  List<int> ends;  List<string> txts;
            GetPswdTxts(str, out starts, out ends, out txts);
            var encs = txts.Select(txt => EncryptString(txt)).ToList();
            var res = WriteFieldsBack(str, starts, ends, encs);
            return res;
        }

        public string DecryptPassWordFields(string str)
        {
            List<int> starts; List<int> ends; List<string> txts;
            GetPswdTxts(str, out starts, out ends, out txts);
            var decs = txts.Select(txt => DecryptString(txt)).ToList();
            var res = WriteFieldsBack(str, starts, ends, decs);
            return res;
        }

        private string WriteFieldsBack(string str, List<int> starts, List<int> ends, List<string> newStrings)
        {
            var zipped = starts.Zip(ends, (fst, scnd) => new { fst, scnd });
            for (int i = zipped.Count() - 1; i >= 0; i--)
            {
                var pair = zipped.ElementAt(i);
                var start = pair.fst + PswdStartTag.Length;
                var end = pair.scnd;
                str = str.Remove(start, end - start);
                str = str.Insert(start, newStrings[0]);
            }

            return str;
        }

        private void GetPswdTxts(string str, out List<int> starts, out List<int> ends, out List<string> txts) {
            starts = str.AllIndexesOf(PswdStartTag);
            ends = str.AllIndexesOf(PswdEndTag);

            List<string> cuts = new List<string>();
            for (int i = 0; i < starts.Count; i++)
            {
                int start = starts[i] + PswdStartTag.Length;
                int len = ends[i] - start;
                cuts.Add(str.Substring(start, len));
            }
            txts = cuts;
        }


        public string EncryptString(string plainText)
        {
            string key = SettingsEncryptinKey;
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public string DecryptString(string cipherText)
        {
            string key = SettingsEncryptinKey;
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        internal string CleanPswdTags(string txt)
        {
            txt = txt.Replace(PswdStartTag, "");
            txt = txt.Replace(PswdEndTag, "");
            return txt;
        }

        internal bool ContainsPswds(string txt)
        {
            if (txt.AllIndexesOf(PswdStartTag).Count > 0 && txt.AllIndexesOf(PswdStartTag).Count > 0) {
                return true;
            }
            return false;
        }

        internal bool PinSet()
        {
            return ConfigurationManager.AppSettings["Pin"] != null;
        }

        internal bool CheckPin(string result)
        {
            var pin = ConfigurationManager.AppSettings["Pin"];
            System.Diagnostics.Debug.WriteLine(pin);
            return ConfigurationManager.AppSettings["Pin"]== result;
            //throw new NotImplementedException();
        }
    }
}

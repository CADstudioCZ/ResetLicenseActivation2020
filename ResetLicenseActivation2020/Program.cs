using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace ResetLicense
{
    class Program
    {
        private static string LicHelperExe => Environment.ExpandEnvironmentVariables(
            @"%CommonProgramFiles(x86)%\Autodesk Shared\AdskLicensing\Current\helper\AdskLicensingInstHelper.exe");

        private static string LoginStateXml =>
            Environment.ExpandEnvironmentVariables(@"%LOCALAPPDATA%\Autodesk\Web Services\LoginState.xml");

        static void Main(string[] args)
        {
            Console.WriteLine(@"
Automatized action described here:
https://knowledge.autodesk.com/support/autocad/troubleshooting/caas/sfdcarticles/sfdcarticles/Forcing-re-activation-of-product.html#subscription
");
            ResetAllLicenses();
        }

        private static void ResetAllLicenses()
        {
            //Read license content
            string outputContent = ReadLicenseContent();

            //Parse productInfos
            var productInfos = ParseProductInfos(outputContent);
            PrintFoundLicenses(productInfos);

            //Unregister
            UnregisterLicenses(productInfos);

            //Delete LoginState.xml
            DeleteLoginStateXml();

            //Kill process AdSSO.exe
            KillAdSSOExe();

            Console.WriteLine("GAME OVER");
        }

        private static ProductInfo[] ParseProductInfos(string outputContent)
        {
            /* Newtonsoft.JSON version
            var productInfos = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductInfo[]>(outputContent);
            return productInfos;
            */

            // Deserialize a JSON stream to a User object.
            var productInfos = new[] { new ProductInfo() };

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(outputContent));
            var ser = new DataContractJsonSerializer(productInfos.GetType());
            productInfos = ser.ReadObject(ms) as ProductInfo[];
            ms.Close();
            return productInfos;
        
        }

        private static void UnregisterLicenses(ProductInfo[] productInfos)
        {
            foreach (var productInfo in productInfos)
            {
                UnregisterLicense(productInfo);
            }
        }

        private static void PrintFoundLicenses(ProductInfo[] productInfos)
        {
            Console.WriteLine();
            Console.WriteLine("Products");
            foreach (var productInfo in productInfos)
            {
                Console.WriteLine($"{productInfo.def_prod_ver}\t{productInfo.def_prod_code}");
            }
        }

        private static void DeleteLoginStateXml()
        {
            if (File.Exists(LoginStateXml))
            {
                File.Delete(LoginStateXml);
                Console.WriteLine("Successfuly deleted");
            }
            else
                Console.WriteLine($"File not found\r\n{LoginStateXml}");
        }

        private static string ReadLicenseContent()
        {
            ProcessStartInfo psi = new ProcessStartInfo(LicHelperExe, "list")
            {
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            var standardOutput = Process.Start(psi)?.StandardOutput;
            string outputContent = standardOutput?.ReadToEnd();
            Console.WriteLine(outputContent);
            return outputContent;
        }

        private static void UnregisterLicense(ProductInfo productInfo)
        {
            string prod_key = productInfo.def_prod_key;
            string prod_ver = productInfo.def_prod_ver;
            string arg = $"change --prod_key {prod_key} --prod_ver {prod_ver} --lic_method \"\"";

            ProcessStartInfo unLicStartinfo = new ProcessStartInfo(LicHelperExe)
            {
                Arguments = arg,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            var unLicProcess = Process.Start(unLicStartinfo);
            unLicProcess?.WaitForExit();
            Console.WriteLine($"DONE AdskLicensingInstHelper.exe {arg}");
        }

        private const string KillInventorFile = "Kill_AdSSOexe.bat";
        private const string KillInventorContent = @"TASKKILL /F /IM AdSSO.exe /T";

        public static void KillAdSSOExe()
        {
            EnsureBatchFile(KillInventorFile, KillInventorContent);
            ExecuteBatchFile(KillInventorFile);
            Console.WriteLine("Process AdSSO.exe killed.");
        }

        private static void EnsureBatchFile(string batFileName, string batFileContent)
        {
            if (!File.Exists(batFileName))
                File.WriteAllText(batFileName, batFileContent, Encoding.GetEncoding(852));
        }

        private static void ExecuteBatchFile(string batFileName)
        {
            var process = Process.Start(batFileName);
            process?.WaitForExit(1000);
        }
    }
}
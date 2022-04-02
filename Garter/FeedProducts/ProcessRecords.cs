using Garter.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Garter.FeedProducts
{
    public class ProcessRecords
    {
        public bool ProcessAllRecords()
        {
            bool result = false;

            string basePath = AppDomain.CurrentDomain.BaseDirectory + "AppData/";

            var directoryFiles = Directory.GetFiles(basePath, "*.*", SearchOption.AllDirectories);


            if (!directoryFiles.Any())
                return false;

            var files = directoryFiles.Where(s => s.EndsWith(".json", StringComparison.OrdinalIgnoreCase) || s.EndsWith(".yaml", StringComparison.OrdinalIgnoreCase));

            foreach (string file in files)
            {
                string ext = GetFileExtension(file);


                result  = ProcessFileOnBasisOfExtension(ext, file);

                if (!result)
                    continue;

                result = true;
            }

            return result;
        }


        private string GetFileExtension(string file) => Path.GetExtension(file);

        private bool ProcessFileOnBasisOfExtension(string ext, string file)
        {
            switch (ext)
            {
                case ".json":
                    return ProcessSoftwareAdviceJson(file);
                case ".yaml":
                    return ProcessCapteraaYamlFile(file);

                default:
                    throw new NotImplementedException("Processor not implemented");
            }
        }

        private bool ProcessSoftwareAdviceJson(string file)
        {
            SoftwareAdviceModel softwareAdviceModel = new SoftwareAdviceModel();

            using (StreamReader r = new StreamReader(file))
            {
                string text = r.ReadToEnd();

                try
                {
                    softwareAdviceModel = JsonConvert.DeserializeObject<SoftwareAdviceModel>(text);
                }
                catch (Exception e)
                {

                    Console.WriteLine("Error while processing softwareAdvice file!!!!");
                    return false;
                }
            }

            if (softwareAdviceModel == null)
            {
                Console.WriteLine("Error while processing softwareAdvice file!!!!");
                return false;
            }

            //Once you get json file then you can process any operation on it
            // save in structured table 
            Console.WriteLine("Software Advice File processed successfully");
            return true;
        }

        private bool ProcessCapteraaYamlFile(string file)
        {
            List<CapterraModel> capterraModel = new List<CapterraModel>();

            var yamlDeserailizer = new DeserializerBuilder()
                                    .WithNamingConvention(CamelCaseNamingConvention.Instance).IgnoreUnmatchedProperties()
                                    .Build();
            using (StreamReader r = new StreamReader(file))
            {
                string text = r.ReadToEnd();

                try
                {
                    capterraModel = yamlDeserailizer.Deserialize<List<CapterraModel>>(text);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error while processing capteraa file!!!!");
                    return false;
                }
            }
                

            if (capterraModel == null || !capterraModel.Any())
            {
                Console.WriteLine("Error while processing capteraa file!!!!");
                return false;
            }

            Console.WriteLine("Capteraa file processed successfully");
            return true;
        }
    }
}

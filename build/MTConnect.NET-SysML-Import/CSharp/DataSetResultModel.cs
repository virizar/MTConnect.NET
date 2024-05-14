﻿using MTConnect.NET_SysML_Import.CSharp;
using MTConnect.SysML.Models.Assets;
using MTConnect.SysML.Models.Observations;
using Scriban;
using System;
using System.IO;
using System.Linq;

namespace MTConnect.SysML.CSharp
{
    public class DataSetResultModel : MTConnectClassModel, ITemplateModel
    {
        public string Namespace => NamespaceHelper.GetNamespace(Id);

        public string XmlDescription { get; set; }


        public static DataSetResultModel Create(MTConnectClassModel importModel)
        {
            if (importModel != null)
            {
                var type = typeof(MTConnectClassModel);

                var importProperties = importModel.GetType().GetProperties();
                var exportProperties = type.GetProperties();

                if (importProperties != null && exportProperties != null)
                {
                    var exportModel = new DataSetResultModel();

                    foreach (var importProperty in importProperties)
                    {
                        var propertyValue = importProperty.GetValue(importModel);

                        var exportProperty = exportProperties.FirstOrDefault(o => o.Name == importProperty.Name);
                        if (exportProperty != null && exportProperty.PropertyType == importProperty.PropertyType)
                        {
                            exportProperty.SetValue(exportModel, propertyValue);
                        }
                    }

                    foreach (var exportProperty in exportModel.Properties)
                    {
                        exportProperty.DataType = ModelHelper.RemoveEnumSuffix(exportProperty.DataType);
                    }

                    exportModel.Description = DescriptionHelper.GetTextDescription(importModel.Description);
                    exportModel.XmlDescription = DescriptionHelper.GetXmlDescription(importModel.Description);

                    return exportModel;
                }
            }

            return null;
        }

        public string RenderModel()
        {
            var templateFilename = $"Observations.DataSetResults.scriban";
            var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "csharp", "templates", templateFilename);
            if (File.Exists(templatePath))
            {
                try
                {
                    var templateContents = File.ReadAllText(templatePath);
                    if (templateContents != null)
                    {
                        var template = Template.Parse(templateContents);
                        return template.Render(this);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return null;
        }

        public string RenderInterface() => null;

        public string RenderDescriptions() => null;
    }
}

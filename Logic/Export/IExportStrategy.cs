using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Logic.Export
{
    internal interface IExportStrategy
    {
        void Export(List<object> courses, string filePath);

        string FileExtension {  get; }

        string FormatDescription {  get; }
    }
}

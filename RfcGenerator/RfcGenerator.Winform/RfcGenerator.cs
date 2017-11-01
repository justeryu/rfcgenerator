using RfcGenerator.Winform.Helper;
using SAP.Middleware.Connector;
using System;
using System.IO;

namespace RfcGenerator.Winform
{
  public class RfcGenerator
  {
    private const string Tab = "    ";

    private static string RfcName = "";

    private static int sName = 0;

    public static string Namespace
    {
      get;
      set;
    }

    public static string HelperNamespace
    {
      get;
      set;
    }

    private static string Indent(int level)
    {
      string text = "";
      for (int i = 0; i < level; i++)
      {
        text += "    ";
      }
      return text;
    }

    public static string GetGenericUsingNameSpaces()
    {
      return string.Format("using System;{0}using System.Collections.Generic;{0}using System.Linq;{0}using System.Text;{0}using SAP.Middleware.Connector;{0}", Environment.NewLine);
    }

    public static string GetUsingNameSpaces()
    {
      string text = "";
      if (RfcGenerator.HelperNamespace != "")
      {
        text += string.Format("using {1};{0}{0}", Environment.NewLine, RfcGenerator.HelperNamespace);
      }
      return text;
    }

    private static string GetSummaryDocumentation(string doc, int indent)
    {
      return string.Format("{1}/// <summary>{0}{1}/// {2} {0}{1}/// </summary>{0}", Environment.NewLine, RfcGenerator.Indent(indent), doc);
    }

    private static string GetParamDocumentation(string name, string doc, int indent)
    {
      return string.Format("{1}/// <param name=\"{2}\">{3}</param>{0}", new object[]
      {
        Environment.NewLine,
        RfcGenerator.Indent(indent),
        name,
        doc
      });
    }

    public static void GenerateFunction(string rfcName)
    {
      RfcGenerator.RfcName = rfcName;
      RfcRepository repository = SapDestination.Destination.Repository;
      RfcFunctionMetadata functionMetadata = repository.GetFunctionMetadata(rfcName);
      int num = 0;
      string text = RfcGenerator.GetGenericUsingNameSpaces() + RfcGenerator.GetUsingNameSpaces();
      text += string.Format("namespace {1}{0}{{{0}", Environment.NewLine, RfcGenerator.Namespace);
      num++;
      text += string.Format("{2}public class {1}{0}{2}{{{0}", Environment.NewLine, "F_" + rfcName, RfcGenerator.Indent(num));
      num++;
      text += RfcGenerator.GetSummaryDocumentation(functionMetadata.GetDescription(), num);
      for (int i = 0; i < functionMetadata.ParameterCount; i++)
      {
        RfcParameterMetadata rfcParameterMetadata = functionMetadata[i];
        text += RfcGenerator.GetParamDocumentation(rfcParameterMetadata.Name, rfcParameterMetadata.Documentation, num);
      }
      text = text + RfcGenerator.Indent(num) + "public static bool Invoke(";
      for (int i = 0; i < functionMetadata.ParameterCount; i++)
      {
        RfcParameterMetadata rfcParameterMetadata = functionMetadata[i];
        if (rfcParameterMetadata.Direction == RfcDirection.IMPORT)
        {
          text += ((i == 0) ? "" : ", ");
          text += RfcGenerator.GetElementString(rfcParameterMetadata);
        }
        else if (rfcParameterMetadata.Direction == RfcDirection.EXPORT)
        {
          text += ((i == 0) ? "" : ", ");
          text = text + "out " + RfcGenerator.GetElementString(rfcParameterMetadata);
        }
        else if (rfcParameterMetadata.Direction == RfcDirection.CHANGING)
        {
          text += ((i == 0) ? "" : ", ");
          text = text + "ref " + RfcGenerator.GetElementString(rfcParameterMetadata);
        }
        else if (rfcParameterMetadata.Direction == RfcDirection.TABLES)
        {
          text += ((i == 0) ? "" : ", ");
          text = text + "ref " + RfcGenerator.GetElementString(rfcParameterMetadata);
        }
      }
      text += string.Format("){0}{1}{{{0}", Environment.NewLine, RfcGenerator.Indent(num));
      num++;
      for (int i = 0; i < functionMetadata.ParameterCount; i++)
      {
        RfcParameterMetadata rfcParameterMetadata = functionMetadata[i];
        RfcElementMetadata rfcElementMetadata = rfcParameterMetadata;
        if (rfcParameterMetadata.Direction == RfcDirection.EXPORT)
        {
          text += string.Format("{1}{2} = default({3});{0}", new object[]
          {
            Environment.NewLine,
            RfcGenerator.Indent(num),
            rfcParameterMetadata.Name,
            RfcGenerator.GetElementType(rfcElementMetadata)
          });
        }
      }
      text += string.Format("{1}try{0}", Environment.NewLine, RfcGenerator.Indent(num));
      text += string.Format("{1}{{{0}", Environment.NewLine, RfcGenerator.Indent(num));
      num++;
      text += string.Format("{1}RfcRepository rfcRep = SapDestination.Destination.Repository;{0}", Environment.NewLine, RfcGenerator.Indent(num));
      text += string.Format("{1}IRfcFunction function = rfcRep.CreateFunction(\"{2}\");{0}", Environment.NewLine, RfcGenerator.Indent(num), rfcName);
      for (int i = 0; i < functionMetadata.ParameterCount; i++)
      {
        RfcParameterMetadata rfcParameterMetadata = functionMetadata[i];
        if (rfcParameterMetadata.Direction != RfcDirection.EXPORT)
        {
          if (rfcParameterMetadata.DataType == RfcDataType.TABLE || rfcParameterMetadata.DataType == RfcDataType.STRUCTURE || rfcParameterMetadata.DataType == RfcDataType.DATE || rfcParameterMetadata.DataType == RfcDataType.TIME)
          {
            text += string.Format("{1}if({2} != null){{{0}", Environment.NewLine, RfcGenerator.Indent(num), rfcParameterMetadata.Name);
            text += string.Format("{1}function.SetValue(\"{2}\", {2}.ToSapObject());{0}", Environment.NewLine, RfcGenerator.Indent(num + 1), rfcParameterMetadata.Name);
            text += string.Format("{1}}}{0}", Environment.NewLine, RfcGenerator.Indent(num));
          }
          else
          {
            text += string.Format("{1}function.SetValue(\"{2}\", {2});{0}", Environment.NewLine, RfcGenerator.Indent(num), rfcParameterMetadata.Name);
          }
        }
      }
      text += string.Format("{1}function.Invoke(SapDestination.Destination);{0}", Environment.NewLine, RfcGenerator.Indent(num));
      for (int i = 0; i < functionMetadata.ParameterCount; i++)
      {
        RfcParameterMetadata rfcParameterMetadata = functionMetadata[i];
        RfcElementMetadata rfcElementMetadata = rfcParameterMetadata;
        if (rfcParameterMetadata.Direction == RfcDirection.EXPORT || rfcParameterMetadata.Direction == RfcDirection.TABLES)
        {
          if (rfcParameterMetadata.DataType == RfcDataType.TABLE)
          {
            text += string.Format("{1}{2}.FromSapObject(function.GetTable(\"{2}\"));{0}", Environment.NewLine, RfcGenerator.Indent(num), rfcParameterMetadata.Name);
          }
          else if (rfcParameterMetadata.DataType == RfcDataType.STRUCTURE)
          {
            text += string.Format("{1}{2}.FromSapObject(function.GetStructure(\"{2}\"));{0}", Environment.NewLine, RfcGenerator.Indent(num), rfcParameterMetadata.Name);
          }
          else
          {
            text += string.Format("{1}{2} = {3};{0}", new object[]
            {
              Environment.NewLine,
              RfcGenerator.Indent(num),
              rfcElementMetadata.Name,
              RfcGenerator.GetElementConvertionType(rfcElementMetadata, "function")
            });
          }
        }
      }
      text += string.Format("{1}return true;{0}", Environment.NewLine, RfcGenerator.Indent(num));
      num--;
      text += string.Format("{1}}}{0}", Environment.NewLine, RfcGenerator.Indent(num));
      text += string.Format("{1}catch{0}", Environment.NewLine, RfcGenerator.Indent(num));
      text += string.Format("{1}{{{0}", Environment.NewLine, RfcGenerator.Indent(num));
      num++;
      text += string.Format("{1}return false;{0}", Environment.NewLine, RfcGenerator.Indent(num));
      num--;
      text += string.Format("{1}}}{0}", Environment.NewLine, RfcGenerator.Indent(num));
      num--;
      text += string.Format("{0}{1}}}", Environment.NewLine, RfcGenerator.Indent(num));
      num--;
      text += string.Format("{0}{1}}}", Environment.NewLine, RfcGenerator.Indent(num));
      num--;
      text += string.Format("{0}}}", Environment.NewLine);
      RfcGenerator.WriteToFile("F_" + rfcName, text);
    }

    private static string GetElementString(RfcElementMetadata el)
    {
      return RfcGenerator.GetElementType(el) + " " + el.Name;
    }

    private static string GetElementType(RfcElementMetadata el)
    {
      string result;
      if (el.DataType == RfcDataType.CHAR || el.DataType == RfcDataType.STRING || el.DataType == RfcDataType.XSTRING)
      {
        result = "string";
      }
      else if (el.DataType == RfcDataType.DATE || el.DataType == RfcDataType.TIME)
      {
        result = "SapDateTime";
      }
      else if (el.DataType == RfcDataType.BCD)
      {
        result = "decimal";
      }
      else if (el.DataType == RfcDataType.FLOAT)
      {
        result = "float";
      }
      else if (el.DataType == RfcDataType.NUM || el.DataType == RfcDataType.INT1 || el.DataType == RfcDataType.INT2 || el.DataType == RfcDataType.INT4 || el.DataType == RfcDataType.INT8)
      {
        result = "int";
      }
      else if (el.DataType == RfcDataType.STRUCTURE)
      {
        RfcStructureMetadata rfcStructureMetadata = el.ValueMetadataAsStructureMetadata;
        RfcContainerMetadata<RfcFieldMetadata> rfcContainerMetadata = rfcStructureMetadata;
        RfcGenerator.GenerateStructure(rfcStructureMetadata);
        result = rfcContainerMetadata.Name;
      }
      else
      {
        if (el.DataType != RfcDataType.TABLE)
        {
          throw new Exception("Unknown type");
        }
        RfcTableMetadata valueMetadataAsTableMetadata = el.ValueMetadataAsTableMetadata;
        RfcStructureMetadata rfcStructureMetadata = valueMetadataAsTableMetadata.LineType;
        if (valueMetadataAsTableMetadata.LineType.Name != "")
        {
          RfcGenerator.GenerateStructure(valueMetadataAsTableMetadata.LineType);
          result = string.Format("SapTable<{0}>", valueMetadataAsTableMetadata.LineType.Name);
        }
        else
        {
          if (valueMetadataAsTableMetadata.LineType.FieldCount != 1)
          {
            throw new Exception("Can not generate type for structure!");
          }
          result = string.Format("SapTable<{0}>", RfcGenerator.GetElementType(valueMetadataAsTableMetadata.LineType[0]));
        }
      }
      return result;
    }

    private static void GenerateStructure(RfcStructureMetadata st)
    {
      int num = 0;
      string text = RfcGenerator.GetGenericUsingNameSpaces() + RfcGenerator.GetUsingNameSpaces();
      text += string.Format("namespace {1}{0}{{{0}", Environment.NewLine, RfcGenerator.Namespace);
      num++;
      text += string.Format("{2}public class {1} : ISapStructure{0}{2}{{{0}", Environment.NewLine, st.Name, RfcGenerator.Indent(num));
      num++;
      for (int i = 0; i < st.FieldCount; i++)
      {
        RfcElementMetadata rfcElementMetadata = st[i];
        text += RfcGenerator.GetSummaryDocumentation(rfcElementMetadata.Documentation, num);
        text += string.Format("{2}public {1} {{ get; set; }}{0}{0}", Environment.NewLine, RfcGenerator.GetElementString(rfcElementMetadata), RfcGenerator.Indent(num));
      }
      text += string.Format("{0}{1}public IRfcStructure ToSapObject(){0}{1}{{{0}", Environment.NewLine, RfcGenerator.Indent(num));
      num++;
      text += string.Format("{1}IRfcStructure s = SapDestination.Destination.Repository.GetStructureMetadata(\"{2}\").CreateStructure();{0}", Environment.NewLine, RfcGenerator.Indent(num), st.Name);
      for (int i = 0; i < st.FieldCount; i++)
      {
        RfcElementMetadata rfcElementMetadata = st[i];
        if (rfcElementMetadata.DataType == RfcDataType.TABLE || rfcElementMetadata.DataType == RfcDataType.STRUCTURE || rfcElementMetadata.DataType == RfcDataType.DATE || rfcElementMetadata.DataType == RfcDataType.TIME)
        {
          text += string.Format("{1}s[\"{2}\"].SetValue({2}.ToSapObject());{0}", Environment.NewLine, RfcGenerator.Indent(num), rfcElementMetadata.Name);
        }
        else
        {
          text += string.Format("{1}s[\"{2}\"].SetValue({2});{0}", Environment.NewLine, RfcGenerator.Indent(num), rfcElementMetadata.Name);
        }
      }
      text += string.Format("{1}return s;", Environment.NewLine, RfcGenerator.Indent(num));
      num--;
      text += string.Format("{0}{1}}}", Environment.NewLine, RfcGenerator.Indent(num));
      text += string.Format("{0}{1}public ISapStructure FromSapObject(IRfcStructure s){0}{1}{{{0}", Environment.NewLine, RfcGenerator.Indent(num));
      num++;
      for (int i = 0; i < st.FieldCount; i++)
      {
        RfcElementMetadata rfcElementMetadata = st[i];
        if (rfcElementMetadata.DataType == RfcDataType.TABLE)
        {
          text += string.Format("{1}this.{2} = new {3}().FromSapObject(s.GetTable(\"{2}\"));{0}", new object[]
          {
            Environment.NewLine,
            RfcGenerator.Indent(num),
            rfcElementMetadata.Name,
            RfcGenerator.GetElementType(rfcElementMetadata)
          });
        }
        else if (rfcElementMetadata.DataType == RfcDataType.STRUCTURE)
        {
          text += string.Format("{1}this.{2} = new {3}().FromSapObject(s.GetStructure(\"{2}\"));{0}", new object[]
          {
            Environment.NewLine,
            RfcGenerator.Indent(num),
            rfcElementMetadata.Name,
            RfcGenerator.GetElementType(rfcElementMetadata)
          });
        }
        else
        {
          text += string.Format("{1}this.{2} = {3};{0}", new object[]
          {
            Environment.NewLine,
            RfcGenerator.Indent(num),
            rfcElementMetadata.Name,
            RfcGenerator.GetElementConvertionType(rfcElementMetadata, "s")
          });
        }
      }
      text += string.Format("{1}return this;", Environment.NewLine, RfcGenerator.Indent(num));
      num--;
      text += string.Format("{0}{1}}}", Environment.NewLine, RfcGenerator.Indent(num));
      num--;
      text += string.Format("{0}{1}}}", Environment.NewLine, RfcGenerator.Indent(num));
      num--;
      text += string.Format("{0}{1}}}", Environment.NewLine, RfcGenerator.Indent(num));
      RfcGenerator.WriteToFile(st.Name, text);
    }

    public static string GetElementConvertionType(RfcElementMetadata el, string var)
    {
      string result;
      if (el.DataType == RfcDataType.DATE)
      {
        result = string.Format("new SapDateTime({1}.GetString(\"{0}\"), SapDateTime.DataType.Date)", el.Name, var);
      }
      else if (el.DataType == RfcDataType.TIME)
      {
        result = string.Format("new SapDateTime({1}.GetString(\"{0}\"), SapDateTime.DataType.Time)", el.Name, var);
      }
      else if (el.DataType == RfcDataType.CHAR || el.DataType == RfcDataType.STRING || el.DataType == RfcDataType.XSTRING)
      {
        result = string.Format("{1}.GetString(\"{0}\")", el.Name, var);
      }
      else if (el.DataType == RfcDataType.BCD)
      {
        result = string.Format("{1}.GetDecimal(\"{0}\")", el.Name, var);
      }
      else if (el.DataType == RfcDataType.FLOAT)
      {
        result = string.Format("{1}.GetFloat(\"{0}\")", el.Name, var);
      }
      else
      {
        if (el.DataType != RfcDataType.NUM && el.DataType != RfcDataType.INT1 && el.DataType != RfcDataType.INT2 && el.DataType != RfcDataType.INT4 && el.DataType != RfcDataType.INT8)
        {
          throw new Exception("Unknown type.");
        }
        result = string.Format("{1}.GetInt(\"{0}\")", el.Name, var);
      }
      return result;
    }

    private static void WriteToFile(string fileName, string content)
    {
      if (fileName == "")
      {
        fileName = "UNKWN_" + RfcGenerator.sName++;
      }
      string text = RfcGenerator.Namespace + "\\" + RfcGenerator.RfcName;
      Directory.CreateDirectory(text);
      File.WriteAllText(text + "\\" + fileName + ".cs", content);
    }
  }
}

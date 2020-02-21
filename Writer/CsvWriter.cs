using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using LossDataExtractor.Data;
using LossDataExtractor.MetaModel;

namespace LossDataExtractor.Writer
{
    public class CSVWriter : IWrite
    {
        public CSVWriter()
        {
        }
        

        public void WriteToFile<T>(IEnumerable<T> results, Header model)
        {
            using (var writer = new StreamWriter(model.FileName))
            {
                WriteHeadersToFile(model, writer);
                foreach (var result in results)
                {
                    WriteEntry(result, model, writer);
                }
            }
        }

        private void WriteEntry<T>(T result, Header model, StreamWriter writer)
        {
            var root = model.RootObject;
            var entries = new List<string>();
            foreach (var rootEntityField in root.EntityFields)
            {
                if (rootEntityField is EntityNumberField || rootEntityField is EntityStringField)
                {
                    Type dataType = typeof(T);
                    entries.Add(dataType.GetProperty(rootEntityField.FieldName)?.GetValue(result).ToString());
                } else if (rootEntityField is EntityObject)
                {
                    Type dataType = typeof(T);
                    dynamic nestedObj = dataType.GetProperty(rootEntityField.FieldName)?.GetValue(result);
                    entries.AddRange(GetObjectEntries((EntityObject) rootEntityField, nestedObj));

                } else if (rootEntityField is EntityList)
                {
                    Type dataType = typeof(T);
                    var list = dataType.GetProperty(rootEntityField.FieldName);
                    entries.Add(WriteListEntry(entries,list,result, (EntityList) rootEntityField));
                }
            }
            writer.WriteLine(string.Join(";", entries.Where(s => !String.IsNullOrEmpty(s))));
        }

        private string WriteListEntry<T>(List<string> rootEntries, PropertyInfo list, T obj, EntityList entityList)
        {
            var listEntries = new List<string>();
            foreach (var item in (IEnumerable) list.GetValue(obj, null))
            { 
                var entries = new List<string>();
                //Add Whitespace to the list based on the root entries
                for (int i = 0; i < rootEntries.Count; i++)
                {
                  entries.Add(" ");  
                }
                foreach (var field in entityList.EntityFields)
                {
                    if (field is EntityNumberField || field is EntityStringField)
                    {
                        Type dataType = item.GetType();
                        entries.Add(dataType.GetProperty(field.FieldName)?.GetValue(item).ToString());
                    } else if (field is EntityObject)
                    {
                        Type dataType = item.GetType();
                        dynamic nestedObj = dataType.GetProperty(field.FieldName)?.GetValue(item);
                        entries.AddRange(GetObjectEntries((EntityObject) field, nestedObj));
                    } else if (field is EntityList)
                    {
                        Type dataType = item.GetType();
                        var listProp = dataType.GetProperty(field.FieldName);
                        entries.Add(WriteListEntry(entries,listProp,item, (EntityList) field));
                    }
                }

                listEntries.Add(string.Join(";", entries.Where(s => !String.IsNullOrEmpty(s))));
            }

            return "\n" + string.Join("\n", listEntries.Where(s => !String.IsNullOrEmpty(s)));
        }

        private IEnumerable<string> GetObjectEntries<T>(EntityObject entityObject, T obj)
        {
            var entries = new List<string>();
            foreach (var field in entityObject.EntityFields)
            {
                if (field is EntityNumberField || field is EntityStringField)
                {
                    Type dataType = typeof(T);
                    entries.Add(dataType.GetProperty(field.FieldName)?.GetValue(obj).ToString());
                } else if (field is EntityObject)
                {
                    Type dataType = typeof(T);
                    dynamic nestedObj = dataType.GetProperty(field.FieldName)?.GetValue(obj);
                    entries.AddRange(GetObjectEntries((EntityObject) field, nestedObj));
                } else if (field is EntityList)
                {
                    Type dataType = typeof(T);
                    var list = dataType.GetProperty(field.FieldName);
                    entries.Add(WriteListEntry(entries,list,obj, (EntityList) field));
                }
            }

            return entries;
        }

        private void WriteHeadersToFile(Header model, StreamWriter writer)
        {
            var root = model.RootObject;
            List<string> headers = new List<string>();
            foreach (var rootEntityField in root.EntityFields)
            {
                if (rootEntityField is EntityNumberField || rootEntityField is EntityStringField)
                {
                    headers.Add(rootEntityField.FieldName); 
                } else if (rootEntityField is EntityObject)
                {
                    headers.AddRange(GetHeaders((EntityObject)rootEntityField));
                } else if (rootEntityField is EntityList)
                {
                    headers.AddRange(GetHeaders((EntityList)rootEntityField));
                }
            }

            writer.WriteLine(string.Join(";", headers.Where(s => !String.IsNullOrEmpty(s))));
        }

        private IEnumerable<string> GetHeaders(EntityObject entityObject)
        {
            var headers = new List<string>();
            foreach (var field in entityObject.EntityFields)
            {
                if (field is EntityNumberField || field is EntityStringField)
                {
                    headers.Add(field.FieldName); 
                } else if (field is EntityObject)
                {
                    headers.AddRange(GetHeaders((EntityObject)field));
                } else if (field is EntityList)
                {
                    headers.AddRange(GetHeaders((EntityList)field));
                }
            }

            return headers;
        }

        private IEnumerable<string> GetHeaders(EntityList entityList)
        {
            var headers = new List<string>();
            foreach (var field in entityList.EntityFields)
            {
                if (field is EntityNumberField || field is EntityStringField)
                {
                    headers.Add(field.FieldName); 
                } else if (field is EntityObject)
                {
                    headers.AddRange(GetHeaders((EntityObject)field));
                } else if (field is EntityList)
                {
                    headers.AddRange(GetHeaders((EntityList)field));
                }
            }

            return headers;
        }
       
        private static bool IsSimple(Type type)
        {
            return type.IsPrimitive || type.Equals(typeof(string));
        }
   
    }
    
}
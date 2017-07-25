using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using OfficeOpenXml;
using System.IO;
using System.Reflection;
using System;

public class DataExportor {
    [MenuItem("Assets/Excel/All", priority = 0)]
    public static void ExportAll() {
        //ExportBuild();
        Export<SO_Build>();
    }
    static Type exlType = typeof(ExlAttribute);
    public static void Export<T>() where T : ScriptableObject {
        // 1. get ExlFileAttributes 
        var filename = "";
        var type = typeof(T);

        foreach (var attr in type.GetCustomAttributes(true)) {
            var exlFile = attr as ExlFileAttribute;
            if(exlFile!=null) {
                filename = exlFile.file;
                break;
            }
        }

        if (string.IsNullOrEmpty(filename)) {
            Debug.LogErrorFormat("Cannot find ExlFileAttribute in type '{0}'", typeof(T).Name);
            return;
        }

        // 2. open Excel and read
        var filePath = string.Format("{0}/Data/Excels/{1}", Application.dataPath, filename);
        FileInfo file = new FileInfo(filePath);
        if (file.Exists) {
            using (ExcelPackage package = new ExcelPackage(file)) {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                var c = worksheet.Cells;

                var idNum = -1;

                // 2a. get filed map;
                Dictionary<string, int> info = new Dictionary<string, int>();
                for (int i = 1; i < 12; i++) {
                    var nn = c[4, i].Value.ToString().Trim().ToLower();
                    if (!info.ContainsKey(nn)) {
                        info[nn] = i;
                    } else {
                        Debug.LogError("Already exists a key named: " + nn);
                    }
                    if (nn.Equals("id")) {
                        idNum = i;
                    }
                }
                if (idNum < 0) {
                    Debug.LogError("Cannot find ID field in the excel, please check!!");
                    return;
                }
                // 2b export data 
                for (int row = 5; row < 8; row++) {
                    var id = c[row, idNum].Value.ToString();
                    makeAssets<T>(string.Format("build/build_{0}.asset", id), b => {

                        // 2. Achieve type info;
                        //foreach (var p in type.GetFields()) {
                        //    var aa = p.GetCustomAttributes(exlType, false);
                        //    if (aa.Length == 1) {
                        //        Debug.LogErrorFormat("f> {0}: {1}", p.Name, p.FieldType);
                        //    }
                        //}

                        //b.ID = int.Parse(id);
                        //b.Name = c[row, 2].Value.ToString();
                        //b.Lev = int.Parse(c[row, 3].Value.ToString());
                        //b.MaxLev = int.Parse(c[row, 4].Value.ToString());
                        //b.BuildType = int.Parse(c[row, 5].Value.ToString());
                        //b.Precondition_Craft = c[row, 6].Value.ToString();
                        //b.Consume_Craft = c[row, 7].Value.ToString();
                        //b.Time_Craft = float.Parse(c[row, 8].Value.ToString());
                        //b.FuncitonOfBuild = c[row, 9].Value.ToString();
                        //b.Precondition_LevUp = c[row, 10].Value.ToString();
                        //b.Consume_LevUp = c[row, 11].Value.ToString();
                        //b.LevUpId = int.Parse(c[row, 12].Value.ToString());
                        // ....
                    });
                }
            }

            

        } else {
            Debug.LogErrorFormat("File \'{0}\' is not exists, please check!!", filePath);
        }
    }

    [MenuItem("Assets/Excel/Build", priority = 1)]
    public static void ExportBuild() {
        var filePath = Application.dataPath + "/Data/Excels/Build.xlsx";
        FileInfo existingFile = new FileInfo(filePath);
        if (existingFile.Exists) {
            using (ExcelPackage package = new ExcelPackage(existingFile)) {
                // get the first worksheet in the workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                for (var row = 5; row < 8; row++) {
                    var c = worksheet.Cells;
                    var id = c[row, 1].Value.ToString();
                    makeAssets<SO_Build>(string.Format("build/build_{0}.asset", id), b => {
                        b.ID = int.Parse(id);
                        b.Name = c[row, 2].Value.ToString();
                        b.Lev = int.Parse(c[row, 3].Value.ToString());
                        b.MaxLev = int.Parse(c[row, 4].Value.ToString());
                        b.BuildType = int.Parse(c[row, 5].Value.ToString());
                        b.Precondition_Craft = c[row, 6].Value.ToString();
                        b.Consume_Craft = c[row, 7].Value.ToString();
                        b.Time_Craft = float.Parse(c[row, 8].Value.ToString());
                        b.FuncitonOfBuild = c[row, 9].Value.ToString();
                        b.Precondition_LevUp = c[row, 10].Value.ToString();
                        b.Consume_LevUp = c[row, 11].Value.ToString();
                        b.LevUpId = int.Parse(c[row, 12].Value.ToString());
                        // ....
                    });
                }

            }
        } else {
            Debug.LogErrorFormat("File \'{0}\' is not exists, please check!!", filePath);
        }
        

    }

    private static void makeAssets<T>(string name, System.Action<T> process) where T : ScriptableObject {
        T asset = ScriptableObject.CreateInstance<T>();

        if (process != null) {
            process(asset);
        }

        AssetDatabase.CreateAsset(asset, "Assets/Data/Datas/" + name);
        AssetDatabase.SaveAssets();
    }
}

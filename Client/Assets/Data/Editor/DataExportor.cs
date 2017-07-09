using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using OfficeOpenXml;
using System.IO;

public class DataExportor {
    [MenuItem("Assets/Excel/All", priority = 0)]
    public static void ExportAll() {
        ExportBuild();
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

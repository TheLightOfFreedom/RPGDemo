using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.IO;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Text;

public class PropAnalysis {
    Dictionary<string, TC> dict;

    [Test]
    public void PropAnalysisSimplePasses() {
        dict = new Dictionary<string, TC>();
        var filePath = Application.dataPath + "/Data/Excels/TC.xlsx";
        FileInfo existingFile = new FileInfo(filePath);
        if (existingFile.Exists) {
            using (ExcelPackage package = new ExcelPackage(existingFile)) {
                // get the first worksheet in the workbook
                var cells = package.Workbook.Worksheets[1].Cells;

                for (var row = 3; row < 710; row++) {
                    var tc = new TC(row, cells);
                    dict.Add(tc.name, tc);
                }
            }
        }

        string name = "Act 3 Cast A";
        List<Item> picked = new List<Item>();
        if (dict.ContainsKey(name)) {
            startPick(dict[name], 1f, 8, picked);
        }
        Debug.LogError(name);
        
       picked.ForEach(p => { if(p.fProp > 0.00001f) Debug.LogError(p.ToString()); });
        Debug.LogError("===========================");
        Dictionary<string, Item> pp = new Dictionary<string, Item>();
        foreach (var p in picked) {
            Item itm = null;
            if (pp.TryGetValue(p.name, out itm)) {
                itm.fProp += p.fProp;
            } else {
                pp.Add(p.name, p);
            }
        }

        foreach (var itm in pp.Values) {
            Debug.LogError(itm.ToString());
        }

    }

    private void startPick(TC root, float prop, int maxNum, List<Item> picked) {
        if (maxNum <= 0 || root == null)
            return;

        foreach (var itm in root.items) {
            var curProp = itm.prop / root.totalProp * prop;
            TC tc = null;
            if (dict.TryGetValue(itm.name, out tc)) { // 包含子TC
                startPick(root, curProp, maxNum - 1, picked);
            } else { // 是一个物品
                picked.Add(new Item(itm.name, curProp));
            }
        }
    }

    class TC : Item {
        public List<Item> items = new List<Item>();
        private ExcelRange range;
        private int row;
        public float totalProp = 0;
        public TC(int row, ExcelRange range) {
            this.range = range;
            this.row = row;
            name = getStr(1);
            picks = getInt(2);
            items.Add(new Item("NoDrop", getInt(7)));
            for (int i = 0; i < 10; i++) {
                var name = getStr(8+i*2);
                if (!string.IsNullOrEmpty(name)) {
                    var prop = getInt(8 + i * 2 + 1);
                    items.Add(new Item(name, prop));
                }
            }
            totalProp = 0;
            items.ForEach(i => { totalProp += i.prop; });
        }
        private string getStr( int col) {
            var c = range[row, col];
            return c == null || c.Value == null ? "" : c.Value.ToString().Trim();
        }

        private int getInt(int col) {
            var c = range[row, col];
            return c == null || c.Value == null ? 0 : int.Parse(c.Value.ToString().Trim());
        }
        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            foreach (var i in items) {
                sb.Append(string.Format("({0}:{1}),", i.name, i.prop));
            }
            return string.Format("{0}_{1} : [{2}]", name, picks, sb.ToString());
        }
    }

    class Item {
        public string name;
        public int prop;
        public float fProp;
        public int picks;
        public Item() { }

        public Item(string _name, int _prop, int _picks = 1) {
            name = _name;
            prop = _prop;
        }

        public Item(string _name, float prop) {
            name = _name;
            fProp = prop;
        }

        public override string ToString() {
            return string.Format("{0}:{1:0.000000}", name, fProp);
        }
    }

	
}

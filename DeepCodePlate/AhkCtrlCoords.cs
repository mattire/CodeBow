//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using static CodingHood.CodeBow;

//namespace CodingHood
//{
//    class AhkCtrlCoords
//    {
//        List<(int, int)> RowAndCTInd = new List<(int, int)>();

//        char[] searchChars = new char[] { ' ', '(', ')', '{', '}', '[', ']' };
//        char[] brackets = new char[] { '(', ')', '{', '}', '[', ']' };

//        //public void Process(string script) {
//        //    var starts = script.AllIndexesOf(FieldPlace.StartTag);
//        //    var ends = script.AllIndexesOf(FieldPlace.EndTag);
//        //    //var pairs = starts.Zip(ends, (i, s, e) => (s, e));
//        //    var pairs = starts.Zip(ends, (s, e) => (s, e));

//        //    var newLines = script.AllIndexesOf("\n");
//        //}

//        public void Process(   string script, 
//                        string processed, 
//                        List<FieldPlace> fieldPlaces)
//        {
//            var newLinesInds = processed.AllIndexesOf("\n");
//            var lines = processed.Split(
//                            new string[] { "\n" }, 
//                            StringSplitOptions.None);


//            #region Stuff
//            //var lineWords = lines.Select((ln, i) => ln.Split(
//            //                            searchChars,
//            //                            StringSplitOptions.None).ToList());

//            //foreach (var lwLst in lineWords)
//            //{
//            //    lwLst.Select((i, ind) => ind).ToList().ForEach(i => System.Diagnostics.Debug.Write(i + ", "));
//            //    System.Diagnostics.Debug.WriteLine("");
//            //}

//            #endregion

//            var speInds = lines.Select(l => new { line = l, idxs = l.AllIndexesOf(searchChars).ToList() }).ToList();
//            var wordStartEndsPerLine = speInds.Select(t =>
//            {
//                t.idxs.Insert(0, -1); // imagine first space to position "-1"
//                t.idxs.Add(t.line.Length);
//                var startEnds = new List<(int, int, int)>();
//                int count = 0;
//                t.idxs.Aggregate((i, j) => { startEnds.Add((i + 1, j, count)); count++; return j; });
//                return startEnds;
//            }).ToList();

//            LstLstTuplePrint(wordStartEndsPerLine);

//            //newLinesInds.Insert(0, -1);
//            newLinesInds.Insert(0, 0);
//            newLinesInds.Add(processed.Length);

//            var bounds = newLinesInds;

//            var fpGroups = new List<(int, List<FieldPlace>)>();
//            bounds.Aggregate((i, j) => 
//            {
//                var fps = fieldPlaces.Where(fp => fp.OutPutTextStart >= i && fp.OutPutTextEnd <= j).ToList();
//                fpGroups.Add((i+1,fps));
//                return j;
//            });
//            System.Diagnostics.Debug.WriteLine("");
//            PrintFPs(fpGroups);

//            var zipped = fpGroups.Zip(wordStartEndsPerLine, (group, startEnds) => { return (g: group, se:startEnds); });

//            List<(int, int)> coordResult1 = new List<(int, int)>();
//            int count1 = 0;
//            foreach (var z in zipped)
//            {
//                count1++;
//                foreach (var fp in z.g.Item2)
//                {
//                    var fpStart = fp.OutPutTextStart; //- z.g.Item1;
//                    var fpEnd = fp.OutPutTextEnd    ; //- z.g.Item1;

//                    if (count1 != 1) {
//                        fpStart = fpStart - z.g.Item1;
//                        fpEnd = fpEnd - z.g.Item1;
//                    }

//                    var match = z.se.FirstOrDefault(startEnd =>
//                                        startEnd.Item1 == fpStart &&
//                                        startEnd.Item2 == fpEnd);

//                    coordResult1.Add((count1, match.Item3));
//                }
//            }
//            coordResult1.ForEach(r => { System.Diagnostics.Debug.WriteLine($"{r.Item1}, {r.Item2}"); });

//            // TODO: spaces around (){}[]
//            string processed2 = Process(processed);
//            System.Diagnostics.Debug.WriteLine(processed2);

//            System.Diagnostics.Debug.WriteLine("");

//            // if(sdfa) { adfafd; } 

//            //fieldPlaces.First().OutPutTextStart
//            //fieldPlaces.First().OutPutTextEnd
//        }

//        private string Process(string processed)
//        {
//            foreach (var br in brackets)
//            {
//                var str1 = " " + br + " ";
//                processed = processed.Replace(br.ToString(), str1);
//            }
//            for (int i = 0; i < brackets.Length; i++)
//            {
//                processed = processed.Replace("  ", " ");
//            }
//            return processed;
//        }

//        private void LstLstTuplePrint<T>(List<List<T>> lstLst)
//        {
//            foreach (var lst in lstLst)
//            {
//                System.Diagnostics.Debug.WriteLine("");
//                foreach (var item in lst)
//                {
//                    item.GetType().GetFields().ToList().ForEach(f => System.Diagnostics.Debug.Write(f.GetValue(item)+","));
//                    System.Diagnostics.Debug.Write("  ");
//                }
//            }
//        }

//        private void PrintFPs(List<(int, List<FieldPlace>)> lstLst) {
//            foreach (var tpl in lstLst)
//            {
//                foreach (var fp in tpl.Item2)
//                {
//                    System.Diagnostics.Debug.Write($"{fp.OutPutTextStart},{fp.OutPutTextEnd},  ");
//                }
//                System.Diagnostics.Debug.WriteLine(tpl.Item1);
//            }
//        }

//    }
//}

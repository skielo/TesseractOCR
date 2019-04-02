using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using Tesseract;

namespace TesseractOCR
{
    class Program
    {
        public static void Main(string[] args)
        {
            var testImagePath = "./closeup_scan.png";//"./phototest.tif";
            if (args.Length > 0)
            {
                testImagePath = args[0];
            }

            try
            {
                using (IResultRenderer renderer = ResultRenderer.CreatePdfRenderer(@"./output", @"./tessdata"))
                {
                    using (renderer.BeginDocument("PDF Test"))
                    {
                        using (TesseractEngine engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.TesseractAndCube))
                        {
                            var list = Directory.GetFiles(@"images");
                            foreach (var item in list)
                            {
                                using (var tifFile = new Bitmap(item))
                                {
                                    //using (var img = PixConverter.ToPix(tifFile))
                                    //{
                                    using (var page = engine.Process(tifFile, "test"))
                                    {
                                        renderer.AddPage(page);
                                    }
                                    //}
                                }
                            }
                        }
                    }
                }
                //using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
                //{
                //    using (var img = Pix.LoadFromFile(testImagePath))
                //    {
                //        using (var page = engine.Process(img))
                //        {
                //            var text = page.GetText();
                //            Console.WriteLine("Mean confidence: {0}", page.GetMeanConfidence());

                //            Console.WriteLine("Text (GetText): \r\n{0}", text);
                //            Console.WriteLine("Text (iterator):");
                //            using (var iter = page.GetIterator())
                //            {
                //                iter.Begin();

                //                do
                //                {
                //                    do
                //                    {
                //                        do
                //                        {
                //                            do
                //                            {
                //                                if (iter.IsAtBeginningOf(PageIteratorLevel.Block))
                //                                {
                //                                    Console.WriteLine("<BLOCK>");
                //                                }

                //                                Console.Write(iter.GetText(PageIteratorLevel.Word));
                //                                Console.Write(" ");

                //                                if (iter.IsAtFinalOf(PageIteratorLevel.TextLine, PageIteratorLevel.Word))
                //                                {
                //                                    Console.WriteLine();
                //                                }
                //                            } while (iter.Next(PageIteratorLevel.TextLine, PageIteratorLevel.Word));

                //                            if (iter.IsAtFinalOf(PageIteratorLevel.Para, PageIteratorLevel.TextLine))
                //                            {
                //                                Console.WriteLine();
                //                            }
                //                        } while (iter.Next(PageIteratorLevel.Para, PageIteratorLevel.TextLine));
                //                    } while (iter.Next(PageIteratorLevel.Block, PageIteratorLevel.Para));
                //                } while (iter.Next(PageIteratorLevel.Block));
                //            }
                //        }
                //    }
                //}
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
                Console.WriteLine("Unexpected Error: " + e.Message);
                Console.WriteLine("Details: ");
                Console.WriteLine(e.ToString());
            }
            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }
    }
}

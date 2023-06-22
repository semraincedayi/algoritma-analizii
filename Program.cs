using System;
using System.IO;
using System.Linq;
using System.Windows.Markup;
using System.Diagnostics;

class Program
{
 static void Main(string[] args)
 {
  var sw = new Stopwatch();
  sw.Start();
  string filename = "tsp_1000_1.txt";
  float[][] koordinatlar = ReadKoordinatlar(filename);
  int[] optimalCozum = SolveTCP(koordinatlar);
  float optimalMaliyet = CalculateMaliyet(koordinatlar, optimalCozum);
  sw.Stop();
  Console.WriteLine("Optimal maliyet: " + optimalMaliyet);
  Console.Write("Optimal cozum: ");
  Console.WriteLine("Programin çalisma süresi: " + sw.ElapsedMilliseconds + " ms");
 for (int i = 0; i < optimalCozum.Length; i++)
  {
   Console.Write(optimalCozum[i] + " ");
  }
  Console.WriteLine();
 }

 static float[][] ReadKoordinatlar(string filename)
 {
  string[] lines = File.ReadAllLines(filename);
  float[][] koordinatlar = new float[lines.Length][];
        for (int i = 0; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(' ');
            koordinatlar[i] = new float[] { float.Parse(values[0]), float.Parse(values[1]) };
           // Console.Write("sayi "+ i +" x "+values[0]+" y " +values[1]);
        }
        
        return koordinatlar;

 }

 static int[] SolveTCP(float[][] koordinatlar)
 {
  int n = koordinatlar.Length;
  int[] cozum = Enumerable.Range(0, n).ToArray();
  float maliyet = CalculateMaliyet(koordinatlar, cozum);
  for (int i = 0; i < n - 1; i++)
  {
   for (int j = i + 1; j < n; j++)
   {
    Swap(cozum, i, j);
    float yeniMaliyet = CalculateMaliyet(koordinatlar, cozum);
    if (yeniMaliyet < maliyet)
    {
     maliyet = yeniMaliyet;
    }
    else
    {
     Swap(cozum, i, j);
    }
   }
  }
  return cozum;
 }

 static float CalculateMaliyet(float[][] koordinatlar, int[] cozum)
 {
  float maliyet = 0;
  for (int i = 0; i < koordinatlar.Length - 1; i++)
  {
   float dx = koordinatlar[cozum[i + 1]][0] - koordinatlar[cozum[i]][0];
   float dy = koordinatlar[cozum[i + 1]][1] - koordinatlar[cozum[i]][1];
   maliyet += (float)Math.Sqrt(dx * dx + dy * dy);
  }
  float dx0 = koordinatlar[cozum[0]][0] - koordinatlar[cozum[koordinatlar.Length - 1]][0];
  float dy0 = koordinatlar[cozum[0]][1] - koordinatlar[cozum[koordinatlar.Length - 1]][1];
  maliyet += (float)Math.Sqrt(dx0 * dx0 + dy0 * dy0);
  return maliyet;
 }
 static void Swap(int[] arr, int i, int j)
 {
  int temp = arr[i];
  arr[i] = arr[j];
  arr[j] = temp;
 }
}
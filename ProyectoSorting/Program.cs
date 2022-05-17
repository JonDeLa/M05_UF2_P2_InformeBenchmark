using System;
using System.Diagnostics;

namespace ProyectoSorting
{
    public class SortingArray
    {
        public int[] array;
        public int[] arrayCreciente;
        public int[] arrayDecreciente;

        public SortingArray(int elements, Random random)
        {
            array = new int[elements];
            arrayCreciente = new int[elements];
            arrayDecreciente = new int[elements];
            for (int i = 0; i < elements; i++)
            {
                array[i] = random.Next(100);
            }
            Array.Copy(array, arrayCreciente, elements);
            Array.Sort(arrayCreciente);
            Array.Copy(arrayCreciente, arrayDecreciente, elements);
            Array.Reverse(arrayDecreciente);
        }

        public void Sort(Action<int[]> func)
        {
            Stopwatch time = new Stopwatch();
            int[] temp = new int[array.Length];
            Array.Copy(array, temp, array.Length);

            Console.WriteLine(func.Method.Name);

            time.Start();

            func(temp);

            time.Stop();

            Console.WriteLine("Initial: " + time.ElapsedMilliseconds + "ms " + time.ElapsedTicks + "ticks");

            time.Reset();
            
            time.Start();

            func(temp);

            time.Stop();

            Console.WriteLine("Increasing: " + time.ElapsedMilliseconds + "ms " + time.ElapsedTicks + "ticks");

            time.Reset();

            Array.Reverse(temp);

            time.Start();

            func(temp);

            time.Stop();

            Console.WriteLine("Decreasing: " + time.ElapsedMilliseconds + "ms " + time.ElapsedTicks + "ticks");
        }
        public void BubbleSort(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - 1; j++)
                {
                    if(array[j] > array[j + 1])
                    {
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }
        public void BubbleSortEarlyExit(int[] array)
        {
            bool ordered = true;
            for (int i = 0; i < array.Length - 1; i++)
            {
                ordered = true;
                for (int j = 0; j < array.Length - 1; j++)
                {
                    if(array[j] > array[j + 1])
                    {
                        ordered = false;
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
                if (ordered)
                    return;
            }
        }
        public void QuickSort(int[] array)
        {
            QuickSort(array, 0, array.Length - 1);
        }
        public void QuickSort(int[] array, int left, int right)
        {
            if(left < right)
            {
                int pivot = QuickSortPivot(array, left, right);
                QuickSort(array, left, pivot);
                QuickSort(array, pivot + 1, right);
            }
        }
        public int QuickSortPivot(int[] array, int left, int right)
        {
            int pivot = array[(left + right) / 2];
            while (true)
            {
                while (array[left] < pivot)
                {
                    left++;
                }
                while (array[right] > pivot)
                {
                    right--;
                }
                if(left >= right)
                {
                    return right;
                }
                else
                {
                    int temp = array[left];
                    array[left] = array[right];
                    array[right] = temp;
                    right--; left++;
                }
            }
        }
        public void InsertionSort(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                //Guardamos posición y valor de la array
                int pos = i - 1;
                int aux = array[i];
                //while para que siga el proceso siempre y cuando se cumpla la condicion escrita

                while ((pos >= 0) && (array[pos] > aux))
                {
                    //Cambio
                    array[pos+1] = array[pos];
                    pos--;
                }
                array[pos+1] = aux;
            }
           
        }
        public void MergeSort(int[] array)
        {
            MergeSort(array, 0, array.Length - 1);
        }
        public void MergeSort(int[] array, int left, int right)
        {
            if (left == right)
            {
                return;
            }
            int half = (left + right) / 2;
            MergeSort(array, left, half);//Primera mitad
            MergeSort(array, half + 1, right);//segundaMitad
            Merge(array, left, half, right);//Juntamos
        }

        private void Merge(int[] array, int left, int half, int right)
        {
           
            int[] output = new int[right - left + 1];
            int i = left, j = half + 1, k = 0;
            //añadimos el numero mas pequeño de ambos.
            while (i <= half && j <= right)
            {
                if (array[i] <= array[j])
                {
                    output[k] = array[i];
                    k += 1; i += 1;
                }
                else
                {
                    output[k] = array[j];
                    k += 1; j += 1;
                }
            }
            //seguimos con los elementos restantes de la primera array
            while(i<= half)
            {
                output[k] = array[i];
                k += 1; i += 1;
            }
            //Lo mismo que arriba pero con la segunda array
            while (j <= right)
            {
                output[k] = array[j];
                k += 1; j += 1;
            }
            //Ahora pasamos el output a la array original.
            for (i = left; i <= right; i+=1)
            {
                array[i] = output[i - left];
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("How many elements do you want?");
            int elements = int.Parse(Console.ReadLine());

            Console.WriteLine("What seed do you want to use?");
            int seed = int.Parse(Console.ReadLine());

            Random random = new Random(seed);
            SortingArray array = new SortingArray(elements, random);
            array.Sort(array.BubbleSort);
            array.Sort(array.BubbleSortEarlyExit);
            array.Sort(array.QuickSort);
            array.Sort(array.InsertionSort);
            array.Sort(array.MergeSort);

        }
    }
}

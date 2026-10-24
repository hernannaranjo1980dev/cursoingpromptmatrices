using System;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

namespace Ejemplo1;

public static class Pregunta3
{
    public static void Responder()
    {
         // Cantidad exacta de notas que se van a solicitar.
            const int cantidadNotas = 5;

            // Arreglo donde se guardarán las cinco notas ya validadas.
            decimal[] notas = new decimal[cantidadNotas];

            // Recorrido del arreglo: se pide cada nota una por una.
            for (int i = 0; i < notas.Length; i++)
            {
                // Se guarda la nota validada en la posición i.
                notas[i] = PedirNota(i + 1);
            }

            // Se realizan los cálculos sobre el arreglo ya completo.
            decimal promedio = CalcularPromedio(notas);
            int aprobados = ContarAprobados(notas);
            int reprobados = notas.Length - aprobados;
            decimal mayor = ObtenerMayor(notas);
            decimal menor = ObtenerMenor(notas);

            // Se muestran los resultados con el formato pedido.
            MostrarResultados(promedio, aprobados, reprobados, mayor, menor);
        }

        // Pide una nota al usuario y no retorna hasta que sea válida.
        // Aquí vive TODA la validación: vacío, no numérico y fuera de rango.
        static decimal PedirNota(int numeroNota)
        {
            while (true)
            {
                Console.Write($"Ingrese la nota {numeroNota} (0 a 10): ");
                string? entrada = Console.ReadLine();

                // Validación 1: la entrada no debe ser nula ni vacía.
                if (string.IsNullOrWhiteSpace(entrada))
                {
                    Console.WriteLine("Error: la entrada está vacía. Intente de nuevo.");
                    continue;
                }

                // Validación 2: aceptar coma o punto decimal.
                // Se reemplaza la coma por punto para usar una sola cultura.
                string entradaNormalizada = entrada.Trim().Replace(',', '.');

                // Validación 3: comprobar que sea un número decimal.
                if (!decimal.TryParse(
                        entradaNormalizada,
                        NumberStyles.Number,
                        CultureInfo.InvariantCulture,
                        out decimal nota))
                {
                    Console.WriteLine("Error: la entrada no es un número válido. Intente de nuevo.");
                    continue;
                }

                // Validación 4: la nota debe estar entre 0 y 10, inclusive.
                if (nota < 0m || nota > 10m)
                {
                    Console.WriteLine("Error: la nota debe estar entre 0 y 10 inclusive. Intente de nuevo.");
                    continue;
                }

                // Si llegó hasta aquí, la nota es válida y se retorna.
                return nota;
            }
        }

        // Recorre el arreglo sumando cada nota y divide entre la cantidad.
        static decimal CalcularPromedio(decimal[] notas)
        {
            decimal suma = 0m;

            foreach (decimal nota in notas)
            {
                suma += nota;
            }

            return suma / notas.Length;
        }

        // Recorre el arreglo contando cuántas notas son >= 7.
        static int ContarAprobados(decimal[] notas)
        {
            int contador = 0;

            foreach (decimal nota in notas)
            {
                if (nota >= 7m)
                {
                    contador++;
                }
            }

            return contador;
        }

        // Recorre el arreglo comparando cada nota con la mayor encontrada.
        static decimal ObtenerMayor(decimal[] notas)
        {
            decimal mayor = notas[0];

            foreach (decimal nota in notas)
            {
                if (nota > mayor)
                {
                    mayor = nota;
                }
            }

            return mayor;
        }

        // Recorre el arreglo comparando cada nota con la menor encontrada.
        static decimal ObtenerMenor(decimal[] notas)
        {
            decimal menor = notas[0];

            foreach (decimal nota in notas)
            {
                if (nota < menor)
                {
                    menor = nota;
                }
            }

            return menor;
        }

        // Imprime las cinco líneas con las etiquetas pedidas.
        // Usa cultura invariante para forzar punto decimal y dos decimales.
        static void MostrarResultados(
            decimal promedio,
            int aprobados,
            int reprobados,
            decimal mayor,
            decimal menor)
        {
            Console.WriteLine();
            Console.WriteLine($"Promedio: {promedio.ToString("F2", CultureInfo.InvariantCulture)}");
            Console.WriteLine($"Aprobados: {aprobados}");
            Console.WriteLine($"Reprobados: {reprobados}");
            Console.WriteLine($"Mayor: {mayor.ToString("F2", CultureInfo.InvariantCulture)}");
            Console.WriteLine($"Menor: {menor.ToString("F2", CultureInfo.InvariantCulture)}");
    }
}
using System;
using System.Globalization;

namespace Ejemplo1;

public static class Pregunta2
{
    public static void Responder()
    {
        // Constante con la cantidad exacta de notas solicitadas.
            const int CantidadNotas = 5;

            // Arreglo de decimal donde se almacenarán las cinco notas válidas.
            decimal[] notas = new decimal[CantidadNotas];

            // Recorrido del arreglo: se pide cada nota una por una.
            // Cada iteración corresponde a una lectura independiente (no se divide una línea por comas).
            for (int i = 0; i < notas.Length; i++)
            {
                // Bucle interno de validación: repite la solicitud de la MISMA nota
                // hasta que la entrada sea numérica y esté dentro del rango [0, 10].
                while (true)
                {
                    Console.Write($"Ingrese la nota {i + 1} (0 a 10): ");
                    string? entrada = Console.ReadLine();

                    // Validación 1: la entrada no debe ser nula ni vacía.
                    if (string.IsNullOrWhiteSpace(entrada))
                    {
                        Console.WriteLine("Error: la entrada está vacía. Intente de nuevo.");
                        continue;
                    }

                    // Validación 2: aceptar coma o punto decimal.
                    // Se reemplaza la coma por punto para que decimal.TryParse
                    // funcione de forma uniforme con la cultura invariante.
                    string entradaNormalizada = entrada.Trim().Replace(',', '.');

                    // Validación 3: comprobar que sea un número decimal válido.
                    // Se usa NumberStyles.Number para permitir signos y separador decimal.
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

                    // Si pasa todas las validaciones, se guarda en el arreglo
                    // y se rompe el bucle interno para pasar a la siguiente nota.
                    notas[i] = nota;
                    break;
                }
            }

            // Cálculos sobre el arreglo ya lleno.
            // Se inicializan acumuladores y contadores.
            decimal suma = 0m;
            int aprobados = 0;
            int reprobados = 0;

            // Se toma la primera nota como referencia inicial para mayor y menor,
            // ya que el arreglo tiene al menos un elemento (5 en este caso).
            decimal mayor = notas[0];
            decimal menor = notas[0];

            // Recorrido del arreglo para acumular la suma, contar aprobados/reprobados
            // y determinar la nota mayor y menor.
            foreach (decimal nota in notas)
            {
                suma += nota;

                // Aprobado si la nota es mayor o igual a 7.
                if (nota >= 7m)
                {
                    aprobados++;
                }
                else
                {
                    reprobados++;
                }

                // Actualización de mayor y menor comparando cada nota.
                if (nota > mayor) mayor = nota;
                if (nota < menor) menor = nota;
            }

            // Cálculo del promedio: suma total dividida entre la cantidad de notas.
            decimal promedio = suma / notas.Length;

            // Formato con dos decimales y punto decimal, usando cultura invariante.
            // Se muestra cada resultado en su propia línea con la etiqueta indicada.
            Console.WriteLine();
            Console.WriteLine($"Promedio: {promedio.ToString("F2", CultureInfo.InvariantCulture)}");
            Console.WriteLine($"Aprobados: {aprobados}");
            Console.WriteLine($"Reprobados: {reprobados}");
            Console.WriteLine($"Mayor: {mayor.ToString("F2", CultureInfo.InvariantCulture)}");
            Console.WriteLine($"Menor: {menor.ToString("F2", CultureInfo.InvariantCulture)}");

    }
}

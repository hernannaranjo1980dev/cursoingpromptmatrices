using System;
using System.Globalization;

namespace Ejemplo1;

public class Pregunta5
{
    public static void Responder()
    {
        
       // La cantidad se define una sola vez para que arreglo y bucle coincidan.
            const int cantidadNotas = 5;
            decimal[] notas = new decimal[cantidadNotas];

            // El índice solo avanza cuando la nota es válida: una entrada
            // inválida no ocupa posición en el arreglo (requisito 5).
            for (int i = 0; i < notas.Length; i++)
            {
                notas[i] = PedirNota(i + 1);
            }

            // Cálculos separados en métodos para mantener cada recorrido con
            // un propósito único y facilitar la lectura.
            decimal promedio = CalcularPromedio(notas);
            int aprobados = ContarAprobados(notas);
            int reprobados = notas.Length - aprobados;
            decimal mayor = ObtenerMayor(notas);
            decimal menor = ObtenerMenor(notas);

            MostrarResultados(promedio, aprobados, reprobados, mayor, menor);
        }

        // Toda la validación vive aquí. Repite la solicitud de la MISMA nota
        // hasta obtener un decimal en [0, 10]. Solo retorna valores válidos,
        // por lo que el llamador nunca recibe datos incorrectos.
        static decimal PedirNota(int numeroNota)
        {
            while (true)
            {
                Console.Write($"Ingrese la nota {numeroNota} (0 a 10): ");
                string? entrada = Console.ReadLine();

                // Se descarta vacío o solo espacios antes de convertir.
                if (string.IsNullOrWhiteSpace(entrada))
                {
                    Console.WriteLine("Error: la entrada está vacía. Intente de nuevo.");
                    continue;
                }

                // Normalizar coma a punto permite aceptar ambas formas con una
                // sola cultura de análisis (InvariantCulture).
                string entradaNormalizada = entrada.Trim().Replace(',', '.');

                // TryParse evita excepciones ante entradas no numéricas.
                if (!decimal.TryParse(
                        entradaNormalizada,
                        NumberStyles.Number,
                        CultureInfo.InvariantCulture,
                        out decimal nota))
                {
                    Console.WriteLine("Error: la entrada no es un número válido. Intente de nuevo.");
                    continue;
                }

                // Comparaciones inclusivas: 0, 7 y 10 son válidas por requisito.
                if (nota < 0m || nota > 10m)
                {
                    Console.WriteLine("Error: la nota debe estar entre 0 y 10 inclusive. Intente de nuevo.");
                    continue;
                }

                return nota;
            }
        }

        // Recorrido del arreglo para acumular la suma; el promedio se calcula
        // al final dividiendo entre la cantidad real de elementos.
        static decimal CalcularPromedio(decimal[] notas)
        {
            decimal suma = 0m;
            foreach (decimal nota in notas)
            {
                suma += nota;
            }
            return suma / notas.Length;
        }

        // Recorrido del arreglo contando solo las notas que cumplen nota >= 7.
        // Los reprobados se derivan en Main como total - aprobados para no
        // duplicar la lógica de conteo.
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

        // Se inicializa con notas[0] en lugar de 0 para que el resultado sea
        // correcto sin importar el rango real de los datos.
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

        // Mismo criterio que ObtenerMayor: se parte del primer elemento para
        // no asumir que 0 es el mínimo posible.
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

        // La presentación se aísla aquí. Se usa InvariantCulture con "F2" para
        // garantizar dos decimales y punto decimal sin depender de la configuración
        // regional del equipo donde se ejecute el programa.
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


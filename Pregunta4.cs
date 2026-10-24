using System;
using System.Globalization;

namespace Ejemplo1;

public class Pregunta4
{
    public static void Responder()
    {
        // Arreglo de 5 posiciones para almacenar las notas válidas.
        // Se llena secuencialmente; una entrada inválida no avanza el índice.
        decimal[] notas = new decimal[5];

        // CultureInfo invariable para garantizar que el separador decimal sea el punto
        // al formatear la salida, independientemente de la configuración regional del sistema.
        CultureInfo culturaSalida = CultureInfo.InvariantCulture;

        int indice = 0;

        // Recorrido controlado por 'indice': solo se incrementa cuando se almacena
        // una nota válida, de modo que las entradas erróneas no consumen una posición.
        while (indice < notas.Length)
        {
            Console.Write($"Ingrese la nota #{indice + 1} (0-10): ");
            string? entrada = Console.ReadLine();

            // Normalización: se acepta tanto coma como punto como separador decimal.
            // Se reemplaza la coma por punto para unificar el formato antes de parsear.
            if (entrada is null)
            {
                Console.WriteLine("Entrada inválida. Intente nuevamente.");
                continue;
            }

            string normalizada = entrada.Trim().Replace(',', '.');

            // Validación numérica con TryParse en cultura invariable (espera punto).
            if (!decimal.TryParse(normalizada, NumberStyles.Number, culturaSalida, out decimal valor))
            {
                Console.WriteLine("Error: la entrada no es un número válido.");
                continue;
            }

            // Validación de rango inclusivo [0, 10]. Los extremos 0 y 10 son válidos.
            if (valor < 0m || valor > 10m)
            {
                Console.WriteLine("Error: la nota debe estar entre 0 y 10.");
                continue;
            }

            // Solo aquí se consume una posición del arreglo.
            notas[indice] = valor;
            indice++;
        }

        // Cálculos en un único recorrido posterior del arreglo.
        decimal suma = 0m;
        int aprobados = 0;
        int reprobados = 0;
        decimal mayor = notas[0];
        decimal menor = notas[0];

        foreach (decimal n in notas)
        {
            suma += n;

            // Umbral de aprobación: nota mayor o igual a 7.
            if (n >= 7m)
                aprobados++;
            else
                reprobados++;

            if (n > mayor) mayor = n;
            if (n < menor) menor = n;
        }

        // Promedio con dos decimales exactos.
        decimal promedio = suma / notas.Length;

        // Formato "0.00" con cultura invariable asegura punto decimal y dos dígitos
        // decimales, cumpliendo el requisito de salida.
        string fmt = "0.00";

        Console.WriteLine($"Promedio: {promedio.ToString(fmt, culturaSalida)}");
        Console.WriteLine($"Aprobados: {aprobados}");
        Console.WriteLine($"Reprobados: {reprobados}");
        Console.WriteLine($"Mayor: {mayor.ToString(fmt, culturaSalida)}");
        Console.WriteLine($"Menor: {menor.ToString(fmt, culturaSalida)}");
    }
}

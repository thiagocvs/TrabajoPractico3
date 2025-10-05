using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        // Ruta del archivo CSV
        string rutaArchivo = @"C:\Users\thiag\OneDrive\Desktop\Programacion\ConsoleApp1\trabajopractico3\f1_last5years.csv";

        string[] lineas = File.ReadAllLines(rutaArchivo);

        int filas = lineas.Length;
        int cols = lineas[0].Split(',').Length;

        // Cargar datos en matriz
        string[,] datos = new string[filas, cols];
        for (int i = 0; i < filas; i++)
        {
            string[] values = lineas[i].Split(',');
            for (int j = 0; j < cols; j++)
            {
                datos[i, j] = values[j];
            }
        }

        // Menú principal
        int opcion = 0;
        while (opcion != 6)
        {
            Console.WriteLine("\n===== MENÚ FÓRMULA 1 =====");
            Console.WriteLine("1. Buscar podios de un piloto");
            Console.WriteLine("2. Datos de campeonato de un equipo en un año");
            Console.WriteLine("3. Mayor remontada");
            Console.WriteLine("4. Listar equipos (orden alfabético)");
            Console.WriteLine("5. Mostrar todos los datos");
            Console.WriteLine("6. Salir");
            Console.WriteLine("==============================================================================================");
            Console.Write("Seleccione una opción: ");
            opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    BuscarPodios(datos, filas);
                    break;
                case 2:
                    DatosEquipo(datos, filas);
                    break;
                case 3:
                    MayorRemontada(datos, filas);
                    break;
                case 4:
                    ListarEquipos(datos, filas);
                    break;
                case 5:
                    MostrarDatos(datos, filas, cols);
                    break;
                case 6:
                    Console.WriteLine("Saliendo del programa...");
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }
    }

    static void BuscarPodios(string[,] datos, int filas)
    {
        Console.Write("Ingrese el nombre del piloto: ");
        string piloto = Console.ReadLine().ToLower();
        int podios = 0;

        for (int i = 1; i < filas; i++) // saltar encabezado
        {
            string nombre = datos[i, 2].ToLower();
            int posLlegada = int.Parse(datos[i, 5]);

            if (nombre.Contains(piloto) && (posLlegada == 1 || posLlegada == 2 || posLlegada == 3))
            {
                podios++;
            }
        }

        Console.WriteLine($"El piloto {piloto} obtuvo {podios} podios en total.");
    }

    static void DatosEquipo(string[,] datos, int filas)
    {
        

        // Console.WriteLine($"Puntos totales de {equipo} en {temporada}: {totalPuntos}");
    }

    static void MayorRemontada(string[,] datos, int filas)
    {
        

        // Console.WriteLine($"Mayor remontada: {pilotoMax} con el equipo de {equipoMax} en {carreraMax}, durante la temporada {temporadaMax}, ganó {remontadaMax} posiciones.");
    }

    static void ListarEquipos(string[,] datos, int filas)
    {
        List<string> equipos = new List<string>();
        // separar equipos
        for (int i = 1; i < filas; i++)
        {
            string equipo = datos[i, 1];

            // quitar duplicados
            if (!equipos.Contains(equipo))
            {
                equipos.Add(equipo);
            }
        }

    // odenar alfabeticamente
    for (int i = 0; i < equipos.Count - 1; i++)
    {
        for (int j = 0; j < equipos.Count - i - 1; j++)
        {
            if (string.Compare(equipos[j], equipos[j + 1]) > 0)
            {
                // intercambio
                string temp = equipos[j];
                equipos[j] = equipos[j + 1];
                equipos[j + 1] = temp;
            }
        }
    }

    // Mostrar
    Console.WriteLine("Equipos:");
    foreach (var eq in equipos)
    {
        Console.WriteLine(eq);
    }
    }

    static void MostrarDatos(string[,] datos, int filas, int cols)
    {
        for (int i = 0; i < filas; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(datos[i, j]);
                if (j < cols - 1)
                    Console.Write(" | ");
            }
            Console.WriteLine();
        }
    }
    
}
    

using System;
using System.IO;

class Program
{
    static void Main()
    {
        string rutaArchivo = @"C:\Users\thiag\OneDrive\Desktop\Programacion\ConsoleApp1\trabajopractico3\f1_last5years.csv";

        string[] lineas = File.ReadAllLines(rutaArchivo);

        int filas = lineas.Length;
        int cols = lineas[0].Split(',').Length;

        string[,] datos = new string[filas, cols];
        for (int i = 0; i < filas; i++)
        {
            string[] values = lineas[i].Split(',');
            for (int j = 0; j < cols; j++)
            {
                datos[i, j] = values[j];
            }
        }

        
        int opcion = 0;
        while (opcion != 7)
        {
            Console.WriteLine("\n===== MENÚ FÓRMULA 1 =====");
            Console.WriteLine("1. Buscar podios de un piloto");
            Console.WriteLine("2. Datos de campeonato de un equipo en un año");
            Console.WriteLine("3. Mayor remontada");
            Console.WriteLine("4. Listar equipos (orden alfabético)");
            Console.WriteLine("5. Mostrar todos los datos");
            Console.WriteLine("6. Calcular La Posioción Promedio De Un Piloto");
            Console.WriteLine("7. Salir");
            Console.WriteLine("==============================================================================================");
            Console.Write("Seleccione una opción: ");
            opcion = int.Parse(Console.ReadLine());
            //menu
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
                    PromedioPosicionPiloto(datos, filas, cols);
                    break;
                case 7:
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

        for (int i = 1; i < filas; i++) 
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

        {
            Console.Write("Ingrese el año de la temporada: ");
            string temporada = Console.ReadLine();

            Console.Write("Ingrese el nombre del equipo: ");
            string equipo = Console.ReadLine().ToLower();

            double totalPuntos = 0;
            bool hayDatos = false;

            for (int i = 1; i < filas; i++)
            {
                string año = datos[i, 0];
                string nomEquipo = datos[i, 1];
                nomEquipo = nomEquipo.ToLower();

                if (año == temporada && nomEquipo == equipo)
                {
                    hayDatos = true;

                    string piloto = datos[i, 2];
                    string carrera = datos[i, 3];
                    double puntos = 0;

                    double.TryParse(datos[i, 6], out puntos);

                    Console.WriteLine($"Carrera: {carrera} | Piloto: {piloto} | Puntos: {puntos}");
                    totalPuntos += puntos;
                }
            }

            if (hayDatos)
            {
                Console.WriteLine($"Puntos totales de {equipo} en {temporada}: {totalPuntos}");
            }
            else
            {
                Console.WriteLine("No se encontraron datos para ese equipo en ese año.");
            }
        }
    }

    static void MayorRemontada(string[,] datos, int filas)
    {
        int remontadaMax = -1;
        string pilotoMax = "";
        string equipoMax = "";
        string carreraMax = "";
        string temporadaMax = "";

        for (int i = 1; i < filas; i++)
        {
            int posInicio = int.Parse(datos[i, 4]);
            int posFinal = int.Parse(datos[i, 5]);
            int remontada = posInicio - posFinal; 

            if (remontada > remontadaMax)
            {
                remontadaMax = remontada;
                temporadaMax = datos[i, 0];
                equipoMax = datos[i, 1];
                pilotoMax = datos[i, 2];
                carreraMax = datos[i, 3];
            }
        }
        
        Console.WriteLine($"La mayor remontada fue de {pilotoMax} (Equipo: {equipoMax}) en {carreraMax} ({temporadaMax}). Ganó {remontadaMax} posiciones.");
       
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

        // ordenar alfabeticamente
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

        // mostrar
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

   // Función para calcular el promedio de posición final de un piloto(nueva indicador)
    static void PromedioPosicionPiloto (string[,] datos, int filas, int cols)
    {
        string pilotoBuscado = "";
        int posFinalSum = 0;
        int totalCarreras = 0;
        double promPosicion = 0.00;
        Console.WriteLine("Ingrese el nombre del piloto: ");
        pilotoBuscado = Console.ReadLine();

        for (int i = 1; i < filas; i++)
        {

            string piloto = datos[i, 2];
            int posFinal = int.Parse(datos[i, 5]);

            if (piloto == pilotoBuscado)
            {
                posFinalSum += posFinal;
                totalCarreras += 1;
            }

        }
        promPosicion = posFinalSum * 1.0 / totalCarreras;

        Console.WriteLine($"El Promedio de Posicion De {pilotoBuscado} Es De : {promPosicion} ");

    }
    
}

